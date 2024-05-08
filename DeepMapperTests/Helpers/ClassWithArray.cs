using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapperTests.Helpers
{
    public class ClassWithArray
    {
        public string Name { get; private set; }
        public ClassWithoutConstructor[] Cars { get; private set; }
    }
}
