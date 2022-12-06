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

        private  void SetProperty(PropertyInfo property, object? source, object destination)
        {
            var sourceProperty = source?.GetType().GetProperty(property.Name, true);
            if (property.PropertyType == sourceProperty?.PropertyType)
            {
                if (property.IsNestedProperty())
                {
                    var nested = Map(sourceProperty.PropertyType, sourceProperty.GetValue(source));
                    property.SetValue(destination, nested);
                }
                else
                {
                    property.SetValue(destination, sourceProperty?.GetValue(source));
                }

            }
            else
            {
                property.SetValue(destination, default);
            }
        }

        private  object MapUsingProperties(Type type, ConstructorInfo ctor, object? obj)
        {

            var mapped = ctor.Invoke(null);
            type.GetWritableProperties().ToList()
            .ForEach(property =>
            {
                SetProperty(property, obj, mapped);
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

        private  object? MapValues(Type type, object? obj)
        {

            var mapped = type.Assembly.CreateInstance(type.FullName ?? type.Name);
            mapped?.GetType()
                  .GetProperties()
                  .ToList()
                  .ForEach(property =>
                  {
                      SetProperty(property, obj, mapped);
                  });

            return mapped;

        }

        public object? Map(Type type, object? obj)
        {
            if (obj == null) return default;

            var ctor = type.GetConstroctorWithMaxParams();
            if (ctor == null && type.IsValueType)
            {
                return MapValues(type, obj);
            }
            else if (ctor!.GetParameters().Length == 0)
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
            if (mapped != null)
            {
                return (T)mapped;
            }
            else
            {
                return default;
            }
        }

    }
}
