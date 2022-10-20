using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapper
{
    public class MappingConfigurations
    {
        internal List<ITypeBinding> bindings;
        private bool useConventionalMapping = true;

        public MappingConfigurations()
        {
            bindings = new List<ITypeBinding>();
        }

        public ITypeBinding<TSource, TDestination> Map<TSource, TDestination>()
        {
            var settings = new TypeBinding<TSource, TDestination>();
            this.bindings.Add(settings);
            return settings;
        }

        public void UseConventionalMapping()
        {
            useConventionalMapping = true;
        }

        public void UseConfigurationalMapping()
        {
            useConventionalMapping = false;
        }

        public bool CanUseConventionalMapping => useConventionalMapping;



    }
}
