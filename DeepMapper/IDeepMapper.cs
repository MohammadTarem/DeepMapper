using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapper
{
    public interface IDeepMapper
    {   
        T? Map<T>(object? obj);
    }
}
