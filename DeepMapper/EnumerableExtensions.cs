using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DeepMapper.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T?> Map<T>(this IEnumerable<object?> objects)
        {
            if(ServiceCollectionExtensions.DeepMapper is null )
            {
                throw new Exception("This method is available through dependency injectiontion only.");
            }
            
            foreach ( var item in objects )
            {
                yield return ServiceCollectionExtensions.DeepMapper!.Map<T>(item);
            }
            
        }
    }
}
