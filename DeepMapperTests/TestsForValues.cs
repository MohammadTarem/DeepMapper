﻿using DeepMapperTests.Helpers;
using DeepMapper;


namespace DeepMapperTests
{
    public class TestsForValues
    {

        private IDeepMapper mapper;
        public TestsForValues(IConventionalMapper cnvMapper, IConfigurationalMapper cngMapper)
        {
            var config = new MappingConfigurations();
            config.UseConventionalMapping();
            mapper = new Mapper(config, cnvMapper, cngMapper);
            
        }

        [Fact]
        public void MapValueByProperties()
        {
            CarValue carValue = new CarValue { Name = "Benz", Volume = 2600 };

            var bmw = mapper.Map<ValueWithoutConstructor>(carValue);

            Assert.Equal(bmw.Name, carValue.Name);
            Assert.Equal(bmw.Volume, carValue.Volume);
            Assert.Equal(0, bmw.Id);

        }

        [Fact]
       public void MapValueByConstructor()
        {
            CarValue carValue = new CarValue { Name = "Benz", Volume = 2600 };

            var benz = mapper.Map<ValueWithConstructor>(carValue);

            Assert.Equal(benz.Name, carValue.Name);
            Assert.Equal(benz.Volume, carValue.Volume);
            Assert.Equal(0, benz.Id);

        }

        [Fact]
        public void MapValueToClassMustFillPropertiesWithSameName()
        {
            var car = new CarValue
            {
                Name = "i9",
                Volume = 2600
            };

            var ClassWithoutConstructor =  mapper.Map<ClassWithoutConstructor>(car);

            Assert.NotNull(ClassWithoutConstructor);
            Assert.Equal(car.Name, ClassWithoutConstructor?.Name);

        }


    }
}
