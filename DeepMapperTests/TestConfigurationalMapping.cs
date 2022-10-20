using DeepMapper;
using DeepMapperTests.Helpers;

namespace DeepMapperTests
{

    public class TestConfigurationalMapping
    {
        private MappingConfigurations mappingConfigurations;
        private IConfigurationalMapper cfgMapper;
        private IConventionalMapper cnvMapper;

        public TestConfigurationalMapping(IConventionalMapper cnvMapper, IConfigurationalMapper cfgMapper)
        {
            mappingConfigurations = new MappingConfigurations();
            mappingConfigurations.Map<Car, ClassWithConstructor>()
                .ToProperty(d => d.Name, c => c.Name)
                .ToProperty(d => d.Manufacturer, c => c.Manufacturer)
                .ToConstant(d => d.CarId, -1)
                .ToDefault(d => d.Engine);

            mappingConfigurations.UseConfigurationalMapping();

            this.cnvMapper = cnvMapper;
            this.cfgMapper = cfgMapper;
        }

        
        [Fact]
        public void MapToProperty()
        {

            var config = new MappingConfigurations();
            config.Map<Car, ClassWithoutConstructor>()
                .ToProperty(d => d.Name, c => c.Name)
                .ToProperty(d => d.Manufacturer, c => c.Manufacturer);
            
            
            config.UseConfigurationalMapping();

            var car = new Car
            {
                Name = "BMW",
                Manufacturer = "BM",
                Engine = new Engine { Manufacturer = "BMW", Cylinder = 8, Volume = 3500 }
            };

            var bmw = new Mapper(config, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car)!;

            Assert.Equal(car.Name, bmw.Name);
            Assert.Equal(car.Manufacturer, bmw.Manufacturer);

        }

        [Fact]
        public void MapToDefaultMustWorkForValueAndRefrenceTypes()
        {

            var config = new MappingConfigurations();
            config.Map<Car, ClassWithoutConstructor>()
                .ToDefault(d => d.CarId)
                .ToDefault(d => d.Engine);

            config.UseConfigurationalMapping();

            var car = new Car
            {
                Name = "BMW",
                Manufacturer = "BM",
                Engine = new Engine { Manufacturer = "BMW", Cylinder = 8, Volume = 3500 }
            };

            var bmw = new Mapper(config, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car)!;


            Assert.Equal(0, bmw.CarId);
            Assert.Null(bmw.Engine);

        }

        [Fact]
        public void MapToConstantMustWorkForValueAndRefrenceTypes()
        {

            var config = new MappingConfigurations();
            var engine = new Engine { Manufacturer = "BMWWW", Cylinder = 8, Volume = 3500 };
            config.Map<Car, ClassWithoutConstructor>()
                .ToConstant(d => d.CarId, -1)
                .ToConstant(d => d.Engine, engine);

            config.UseConfigurationalMapping();

            var car = new Car
            {
                Name = "BMW",
                Manufacturer = "BM",
                Engine = new Engine { Manufacturer = "BMW", Cylinder = 8, Volume = 3500 }
            };

            var bmw = new Mapper(config, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car)!;


            Assert.Equal(-1, bmw.CarId);
            Assert.Equal(engine.Manufacturer, bmw.Engine.Manufacturer);
            Assert.Equal(engine.Volume, bmw.Engine.Volume);
            Assert.Equal(engine.Cylinder, bmw.Engine.Cylinder);

        }

        [Fact]
        public void MapUsingIgnoreMustNotAssignProperty()
        {

            var config = new MappingConfigurations();
            var engine = new Engine { Manufacturer = "BMWWW", Cylinder = 8, Volume = 3500 };
            config.Map<Car, ClassWithoutConstructor>()
                .ToProperty(d => d.Manufacturer, c => c.Manufacturer)
                .ToProperty(d => d.Name, c => c.Name)
                .Ignore(d => d.Engine);


            config.UseConfigurationalMapping();

            var car = new Car
            {
                Name = "BMW",
                Manufacturer = "BM",
                Engine = new Engine { Manufacturer = "BMW", Cylinder = 8, Volume = 3500 }
            };

            var bmw = new Mapper(config, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car)!;


            Assert.Null(bmw.Engine);

        }

        [Fact]
        public void MapToClassWithoutDefaultConstructorMustThrows()
        {

            var config = new MappingConfigurations();
            var engine = new Engine { Manufacturer = "BMWWW", Cylinder = 8, Volume = 3500 };
            config.Map<Car, ClassWithConstructor>()
                .ToConstant(d => d.CarId, -1)
                .ToConstant(d => d.Engine, engine);

            config.UseConfigurationalMapping();

            var car = new Car
            {
                Name = "BMW",
                Manufacturer = "BM",
                Engine = new Engine { Manufacturer = "BMW", Cylinder = 8, Volume = 3500 }
            };

            Assert.Throws<Exception>(() =>
            {
                _ = new Mapper(config, cnvMapper, cfgMapper).Map<ClassWithConstructor>(car)!;
            });

        }

        [Fact]
        public void ConventionalMappingMustNotWork()
        {
            var config = new MappingConfigurations();
            var engine = new Engine { Manufacturer = "BMWWW", Cylinder = 8, Volume = 3500 };
            
            config.UseConfigurationalMapping();

            var car = new Car
            {
                Name = "BMW",
                Manufacturer = "BM",
                Engine = new Engine { Manufacturer = "BMW", Cylinder = 8, Volume = 3500 }
            };
            
            var bmw = new Mapper(config, cnvMapper, cfgMapper).Map<ClassWithoutConstructor>(car);

            Assert.Null(bmw);
        }

    }




}
