using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepMapper;
using DeepMapper.Extensions;
using DeepMapperTests.Helpers;

namespace DeepMapperTests
{
    public class TestEnumerationsMapping
    {
        IDeepMapper deepMapper;
        public TestEnumerationsMapping(IDeepMapper deepMapper)
        {
            this.deepMapper = deepMapper;
        }

        [Fact]
        public void MappingEnumerableMustWork()
        {
            var cars = new []
            {
                new Car 
                {
                    Name = "i9",
                    Manufacturer = "ClassWithoutConstructor",
                    Engine = new Engine
                    {
                        Volume = 3500,
                        Manufacturer = "ClassWithoutConstructor",
                        Cylinder = 8
                    }
                },
                new Car
                {
                    Name = "i8",
                    Manufacturer = "ClassWithoutConstructor",
                    Engine = new Engine
                    {
                        Volume = 3500,
                        Manufacturer = "ClassWithoutConstructor",
                        Cylinder = 8
                    }
                }
            };


            var newCars = cars.Map<ClassWithConstructor>();

            Assert.True(newCars.Count() == 2);
            Assert.NotNull(newCars.First(c => c?.Name == "i9"));
            Assert.NotNull(newCars.First(c => c?.Name == "i8"));
        }

        [Fact]
        public void MappingObjectUsingMapExtensionMethodMustThrows()
        {
            var cars = new[]
            {
                new Car
                {
                    Name = "i9",
                    Manufacturer = "ClassWithoutConstructor",
                    Engine = new Engine
                    {
                        Volume = 3500,
                        Manufacturer = "ClassWithoutConstructor",
                        Cylinder = 8
                    }
                },
                new Car
                {
                    Name = "i8",
                    Manufacturer = "ClassWithoutConstructor",
                    Engine = new Engine
                    {
                        Volume = 3500,
                        Manufacturer = "ClassWithoutConstructor",
                        Cylinder = 8
                    }
                }
            };


            Assert.ThrowsAny<Exception>(() => 
            {
                var newCars = deepMapper.Map<ClassWithConstructor>(cars);
            });


        }



    }
}
