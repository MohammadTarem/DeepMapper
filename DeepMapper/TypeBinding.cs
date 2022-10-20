using System.Linq.Expressions;


namespace DeepMapper
{
    internal class TypeBinding<TSource, TDestination> : ITypeBinding<TSource, TDestination>, ITypeBinding
    {
        public TypeBinding()
        {
            SourceType = typeof(TSource);
            DestinationType = typeof(TDestination);
            Binds = new List<Bind>();
        }
        public Type SourceType { get; }
        public Type DestinationType { get; }
        public List<Bind> Binds { get; }
        public ITypeBinding<TSource, TDestination> ToProperty<T1,T2>(Expression<Func<TDestination, T1>> destination, Expression<Func<TSource, T2>> source)
        {
            var srcProperty = (source.Body as MemberExpression)?.Member.Name;
            var destProperty = (destination.Body as MemberExpression)?.Member.Name;
            if (srcProperty != null && destProperty != null)
            {
                Binds.Add(new PropertyBind(srcProperty, destProperty));
            }
            return this;
        }
        public ITypeBinding<TSource, TDestination> ToConstant<T>(Expression<Func<TDestination, T>> destination, T value)
        {

            var destProperty = (destination.Body as MemberExpression)?.Member.Name;
            if (destProperty != null)
            {
                Binds.Add(new ConstantBind(value, destProperty));
            }
            return this;
        }
        public ITypeBinding<TSource, TDestination> ToDefault<T>(Expression<Func<TDestination, T>> destination)
        {

            var destProperty = (destination.Body as MemberExpression)?.Member.Name;
            if (destProperty != null)
            {
                Binds.Add(new DefaultBind(destProperty));
            }
            return this;
        }
        
        public ITypeBinding<TSource, TDestination> Ignore<T>(Expression<Func<TDestination, T>> destination)
        {
            var destProperty = (destination.Body as MemberExpression)?.Member.Name;
            if (destProperty != null)
            {
                Binds.Add(new IgnoreBind(destProperty));
            }
            return this;
        }

        public void UseConventionalMapping()
        {
            var properties = DestinationType.GetWritableProperties();
            var remainingProp = properties.ExceptBy(Binds.Select(t => t.Destination), p => p.Name);
            remainingProp.ToList()
                .ForEach(p => Binds.Add(new PropertyBind(p.Name, p.Name)));

        }
    }
}
