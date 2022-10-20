using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapper
{
    public sealed class Mapper : IDeepMapper
    {
        
        private IConventionalMapper conventionalMapper;
        private IConfigurationalMapper configurationalMapper;
        private MappingConfigurations configurations;
        public Mapper(MappingConfigurations configuration, IConventionalMapper cnvMapper, IConfigurationalMapper cfgMapper)
        {
            conventionalMapper = cnvMapper;
            configurationalMapper = cfgMapper;
            configurations = configuration;
        }

        private ITypeBinding? FindTypeBinding(Type? source, Type destination) =>
            configurations.bindings.FirstOrDefault(t => t.SourceType == source && t.DestinationType == destination);


        public T? Map<T>(object? obj)
        {

            var binding = FindTypeBinding(obj?.GetType(), typeof(T));
            if (binding == null)
            {
                return configurations.CanUseConventionalMapping ?
                                conventionalMapper.Map<T>(obj) : default(T);
            } 
            else
            {
                return configurationalMapper!.Map<T>(obj, binding);
            }

        }
    }
}
