using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperTests.Helpers
{
    public struct CarValue
    {
        public string Name { get; set; }
        public int Volume { get; set; }
    }

    public struct ValueWithoutConstructor
    {
        
        public string Name { get; private set; }
        public int Volume { get; private set; }
        public int Id { get; private set; }

    }

    public struct ValueWithConstructor
    {
        private string name;
        private int volume;
        private int id;
        public ValueWithConstructor(string name, int volume, int id)
        {
            this.name = name;
            this.volume = volume;
            this.id = id;

        }
        public string Name => name;
        public int Volume => volume;
        public int Id => id;

    }
}
