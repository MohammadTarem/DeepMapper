using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DeepMapper
{

    public interface IConfigurationalMapper
    {
        public T? Map<T>(object? obj, ITypeBinding typeBinding);
    }

    internal sealed class ConfigurationalMapper  : IConfigurationalMapper
    {
        public ConfigurationalMapper()
        {
            
        }

        private  T CreateInstance<T>()
        {
            try
            {
                return (T)typeof(T).ConstructNewInstance()!;
            }
            catch
            {
                throw new Exception("No default constructor.");
            }

        }

        private  void SetProperty(Bind bind, object? source, object? destination)
        {
            var destProperty = destination?.GetType().GetProperty(bind.Destination, true)!;


            switch (bind)
            {
                case PropertyBind propertyBind:
                    var srcProperty = source?.GetType().GetProperty(propertyBind.Source, true);
                    destProperty?.SetValue(destination, srcProperty?.GetValue(source));
                    break;
                case IgnoreBind _:
                    break;
                case DefaultBind _:
                    destProperty.SetValue(destination, default);
                    break;
                case ConstantBind constantBind:
                    destProperty.SetValue(destination, constantBind.Constant);
                    break;
                default:
                    break;
            }

        }

        private  IEnumerable<Bind> RemoveIgnoredProperties(ITypeBinding typeBinding)
        {

            var ignoreBinds = typeBinding.Binds
                .Where(t => t is IgnoreBind)
                .Select(b => b.Destination);

            return typeBinding.Binds
                .ExceptBy(ignoreBinds, b => b.Destination);

        }

        public  T? Map<T>(object? obj, ITypeBinding typeBinding)
        {
            var newInstance = CreateInstance<T>();
            var propertyList = RemoveIgnoredProperties(typeBinding);

            propertyList
            .DistinctBy(b => b.Destination)
            .ToList()
            .ForEach(bind => SetProperty(bind, obj, newInstance));

            return newInstance;
        }

    }
}
