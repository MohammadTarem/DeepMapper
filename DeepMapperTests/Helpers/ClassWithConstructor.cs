using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperTests.Helpers
{
    public class ClassWithConstructor
    {
        protected string name;
        protected string manufacturer;
        protected Engine engine;
        protected int carId;

        public ClassWithConstructor(string name, string manufacturer, Engine engine, int carId)
        {
            this.name = name;
            this.manufacturer = manufacturer;
            this.engine = engine;
            this.carId = carId;
        }

        public string Name => name;
        public string Manufacturer => manufacturer;
        public Engine Engine => engine;
        public int CarId => carId;
    }
}
