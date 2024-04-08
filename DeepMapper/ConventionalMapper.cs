
using System.Reflection;

namespace DeepMapper
{
    public interface IConventionalMapper
    {
        public T? Map<T>(object? obj);
    }
    public sealed class ConventionalMapper : IConventionalMapper
    {
        
        public ConventionalMapper() { }

        private void SetPropertyUsingParent(PropertyInfo property, object? sourceObject, object destinationObject)
        {
            var sourceProperty = sourceObject?.GetType().GetProperty(property.Name, true);
            SetProperty(property, sourceProperty?.GetValue(sourceObject), destinationObject);
        }

        private void SetProperty(PropertyInfo property, object? sourceProperty, object destinationObject)
        {
            
            var sourcePropertyType = sourceProperty?.GetType();
            if (property.IsNestedProperty())
            {
                if(sourcePropertyType != null)
                {
                    var nested = Map(property.PropertyType, sourceProperty);
                    property.SetValue(destinationObject, nested);
                }
                else
                {
                    property.SetValue(destinationObject, default);
                }
                
            }
            else
            {
                property.SetValue(destinationObject, sourceProperty);
            }

        }

        private  object MapUsingProperties(Type type, ConstructorInfo ctor, object? obj)
        {

            var mapped = ctor.Invoke(null);
            type.GetWritableProperties().ToList()
            .ForEach(property =>
            {
                SetPropertyUsingParent(property, obj, mapped);
            });

            return mapped;

        }

        private  object? MapUsingConstructor(ConstructorInfo ctor, object? obj)
        {
            List<object?> parameters = new List<object?>();
            ctor.GetParameters().ToList()
            .ForEach(param =>
            {
                var property = obj?.GetType().GetProperty(param.Name ?? "", true);
                if (property?.PropertyType == param.ParameterType)
                {
                    if (property.IsNestedProperty())
                    {
                        parameters.Add(Map(property.PropertyType, property.GetValue(obj)));
                    }
                    else
                    {
                        parameters.Add(property.GetValue(obj) ?? default);
                    }
                }
                else
                {
                    parameters.Add(default);
                }
            });

            return ctor.Invoke(parameters.ToArray());
        }

        private  object? MapValues(Type type, ConstructorInfo? ctor, object? obj)
        {
            
            if(ctor != null)
            {
                return MapUsingConstructor(ctor, obj);
            }

            var mapped = type.Assembly.CreateInstance(type.FullName ?? type.Name);
            mapped?.GetType()
                  .GetProperties()
                  .ToList()
                  .ForEach(property =>
                  {
                      SetPropertyUsingParent(property, obj, mapped);
                  });

            return mapped;

        }

        private object? MapDictionary(Type type, IDictionary<string, object?> keyValues)
        {

            var mapped = type.InstantiateObjectWithDefaultConstructor();
            type.GetWritableProperties().ToList()
            .ForEach(property =>
            {
                object? obj = null;
                if (keyValues.TryGetValue(property.Name, out obj) ||
                   keyValues.TryGetValue(property.Name.ToLower(), out obj))
                {
                    SetProperty(property, obj, mapped);
                }

            });

            return mapped;

        }

        public object? Map(Type type, object? obj)
        {
            if (obj == null) return default;
            
            if(obj is IDictionary<string, object?> dictionary)
            {
                return MapDictionary(type, dictionary);
            }

            var ctor = type.GetConstroctorWithMaxParams();

            if (type.IsValueType)
            {
                return MapValues(type, ctor, obj);
            }
            
            if(ctor!.GetParameters().Length == 0)
            {
                return MapUsingProperties(type, ctor, obj);
            }
            else
            {
                return MapUsingConstructor(ctor, obj);
            }
            
        }

        public T? Map<T>(object? obj)
        {
            var mapped = Map(typeof(T), obj);
            return mapped != null ? (T)mapped : default;
        }

        //public T? MapFromJsonString<T>(string json )
        //{
        //    var obj = JsonSerializer.Deserialize<JsonObject>(json)
        //                            .ToDictionary()
        //    //var s = obj.to

        //    return Map<T>(obj);
        //}

        

    }
   
}
