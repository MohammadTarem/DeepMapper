using DeepMapperTests.Helpers;
using DeepMapper;
namespace DeepMapperTests
{
    public class TestConventionalMapping
    {
        private MappingConfigurations configurations;
        private IConventionalMapper cnvMapper;
        private IConfigurationalMapper cfgMapper;
        public TestConventionalMapping(IConfigurationalMapper cfgMapper, IConventionalMapper cnvMapper)
        {
            configurations = new MappingConfigurations();
            
            configurations.UseConventionalMapping();
            this.cfgMapper = cfgMapper;
            this.cnvMapper = cnvMapper;
        }

        [Fact]
        public void MapUsingProperties()
        {
            var car = new Car
            {
                Name = "i9",
                Manufacturer = "ClassWithoutConstructor",
                Engine = new Engine
                {
                    Volume = 3500,
                    Manufacturer = "ClassWithoutConstructor",
                    Cylinder = 8
                }
            };


            var ClassWithoutConstructor = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car);


            Assert.Equal(car.Name, ClassWithoutConstructor?.Name);
            Assert.Equal(car.Manufacturer, ClassWithoutConstructor?.Manufacturer);
            Assert.Equal(car.Engine.Manufacturer, ClassWithoutConstructor?.Engine.Manufacturer);
            Assert.Equal(car.Engine.Cylinder, ClassWithoutConstructor?.Engine.Cylinder);
            Assert.Equal(car.Engine.Volume, ClassWithoutConstructor?.Engine.Volume);
            Assert.Equal(0, ClassWithoutConstructor?.CarId);


        }

        [Fact]
        public void MapUsingConstructor()
        {
            var car = new Car
            {
                Name = "i9",
                Manufacturer = "ClassWithConstructor",
                Engine = new Engine
                {
                    Volume = 3500,
                    Manufacturer = "ClassWithConstructor",
                    Cylinder = 8
                }
            };


            var ClassWithConstructor = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithConstructor>(car);

            Assert.Equal(car.Name, ClassWithConstructor?.Name);
            Assert.Equal(car.Manufacturer, ClassWithConstructor?.Manufacturer);
            Assert.Equal(car.Engine.Manufacturer, ClassWithConstructor?.Engine.Manufacturer);
            Assert.Equal(car.Engine.Cylinder, ClassWithConstructor?.Engine.Cylinder);
            Assert.Equal(car.Engine.Volume, ClassWithConstructor?.Engine.Volume);
            Assert.Equal(0, ClassWithConstructor?.CarId);


        }

        [Fact]
        public void MapClassToItSelfMustResultNewInstance()
        {
            var ClassWithConstructor = new ClassWithConstructor("ClassWithConstructor", "ClassWithConstructor", new Engine { Manufacturer = "ClassWithConstructor", Cylinder = 12, Volume = 3500 }, 10);

            var mapped = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithConstructor>(ClassWithConstructor);

            Assert.Equal(mapped?.Name, ClassWithConstructor.Name);
            Assert.Equal(mapped?.Manufacturer, ClassWithConstructor.Manufacturer);
            Assert.Equal(mapped?.CarId, ClassWithConstructor.CarId);
            Assert.Equal(mapped?.Engine.Manufacturer, ClassWithConstructor.Engine.Manufacturer);
            Assert.Equal(mapped?.Engine.Volume, ClassWithConstructor.Engine.Volume);
            Assert.Equal(mapped?.Engine.Cylinder, ClassWithConstructor.Engine.Cylinder);

            Assert.False(Object.ReferenceEquals(mapped, ClassWithConstructor));
        }

        [Fact]
        public void NestedClassesMustBeMappedToNewInstance()
        {

            var car = new Car
            {
                Name = "i9",
                Manufacturer = "ClassWithConstructor",
                Engine = new Engine
                {
                    Volume = 3500,
                    Manufacturer = "ClassWithConstructor",
                    Cylinder = 8
                }
            };

            var ClassWithConstructor = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithConstructor>(car);
            var ClassWithoutConstructor = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car);


            Assert.False(object.ReferenceEquals(car.Engine, ClassWithConstructor?.Engine));
            Assert.False(object.ReferenceEquals(car.Engine, ClassWithoutConstructor?.Engine));

        }

        [Fact]
        public void MapToClassWithLowerCaseCharachters()
        {
            var car = new Car
            {
                Name = "i9",
                Manufacturer = "ClassWithoutConstructor",
                Engine = new Engine
                {
                    Volume = 3500,
                    Manufacturer = "ClassWithoutConstructor",
                    Cylinder = 8
                }
            };


            var b = new  Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithLowerCase>(car);


            Assert.Equal(car.Name, b?.name);
            Assert.Equal(car.Manufacturer, b?.manufacturer);
            Assert.Equal(0, b?.carId);


        }

        [Fact]
        public void MapToNullObjectMustBeNull()
        {

            var b = new  Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(null);

            Assert.Null(b);

        }

        [Fact]
        public void MapToObjectWithNullNestedObject()
        {
            var car = new Car
            {
                Name = "i9",
                Manufacturer = "ClassWithConstructor",
                Engine = null
            };


            var ClassWithConstructor = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithConstructor>(car);
            var ClassWithoutConstructor = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car);


            Assert.Null(ClassWithConstructor?.Engine);
            Assert.Null(ClassWithoutConstructor?.Engine);

        }

        [Fact]
        public void MapClassToValueMustFillPropertiesWithSameName()
        {
            var car = new Car
            {
                Name = "i9",
                Manufacturer = "ClassWithoutConstructor",
                Engine = new Engine
                {
                    Volume = 3500,
                    Manufacturer = "ClassWithoutConstructor",
                    Cylinder = 8
                }
            };


            var b = new Mapper(configurations, cnvMapper, cfgMapper).Map<CarValue>(car);


            Assert.Equal(car.Name, b.Name);
            


        }

        [Fact]
        public void TwoLevelNestedClassMustBeMappedToNewInstance()
        {

            var car = new ClassWithTwoLevelEngine
            {
                Name = "i9",
                Manufacturer = "ClassWithConstructor",
                TwoLevel = new TwoLevelEngine
                {
                     
                    Engine = new Engine
                    {
                        
                            Volume = 3500,
                            Manufacturer = "ClassWithConstructor",
                            Cylinder = 8
                        
                    },
                    Model = "A21"
                }
            };

            var twoLevel = new Mapper(configurations, cnvMapper, cfgMapper).Map<ClassWithTwoLevelEngine>(car);
            


            Assert.False(object.ReferenceEquals(car.TwoLevel, twoLevel?.TwoLevel));
            Assert.False(object.ReferenceEquals(car.TwoLevel.Engine, twoLevel?.TwoLevel.Engine));

        }




    }
}