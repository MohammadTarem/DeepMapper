using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapper
{
    public interface ITypeBinding
    {
        public Type SourceType { get; }
        public Type DestinationType { get; }
        public List<Bind> Binds { get; }
    }

    public interface ITypeBinding<TSource, TDestination>
    {
        public ITypeBinding<TSource, TDestination> ToProperty<T1,T2>(Expression<Func<TDestination, T1>> destination, Expression<Func<TSource, T2>> source);
        public ITypeBinding<TSource, TDestination> ToConstant<T>(Expression<Func<TDestination, T>> destination, T value);
        public ITypeBinding<TSource, TDestination> ToDefault<T>(Expression<Func<TDestination, T>> destination);
        public ITypeBinding<TSource, TDestination> Ignore<T>(Expression<Func<TDestination, T>> destination);
        public void UseConventionalMapping();
    }
}
