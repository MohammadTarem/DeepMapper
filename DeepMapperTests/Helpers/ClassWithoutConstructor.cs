using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperTests.Helpers
{
    public class ClassWithoutConstructor
    {
        public string Name { get; private set; }
        public string Manufacturer { get; private set; }

        public Engine Engine { get;  private set; }

        public int CarId { get;  private set; }
    }
}
