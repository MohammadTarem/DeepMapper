using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapperTests.Helpers
{
    public  class ClassWithTwoLevelEngine
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public TwoLevelEngine TwoLevel { get; set; }
    }
}
