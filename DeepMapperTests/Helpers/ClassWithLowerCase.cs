using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapperTests.Helpers
{
    public class ClassWithLowerCase
    {
        protected string _name;
        protected string _manufacturer;
        protected Engine _engine;
        protected int _carId;

        public ClassWithLowerCase(string name, string manufacturer, Engine engine, int carId)
        {
            _name = name;
            _manufacturer = manufacturer;
            _engine = engine;
            _carId = carId;
        }

        public string name => _name;
        public string manufacturer => _manufacturer;
        public Engine engine => _engine;
        public int carId => _carId;
    }
}
