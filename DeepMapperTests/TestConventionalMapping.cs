using DeepMapperTests.Helpers;
using DeepMapper;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeepMapperTests
{
	public class TestConventionalMapping
	{
		
		
		public TestConventionalMapping()
		{
		
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


			var ClassWithoutConstructor = new ConventionalMapper().Map<ClassWithoutConstructor>(car);

			

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


			var ClassWithConstructor = new ConventionalMapper().Map<ClassWithConstructor>(car);

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

			var mapped = new ConventionalMapper().Map<ClassWithConstructor>(ClassWithConstructor);

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

			var ClassWithConstructor = new ConventionalMapper().Map<ClassWithConstructor>(car);
			var ClassWithoutConstructor = new ConventionalMapper().Map<ClassWithoutConstructor>(car);


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


			var b = new ConventionalMapper().Map<ClassWithLowerCase>(car);


			Assert.Equal(car.Name, b?.name);
			Assert.Equal(car.Manufacturer, b?.manufacturer);
			Assert.Equal(0, b?.carId);


		}

		[Fact]
		public void MapToNullObjectMustBeNull()
		{

			var b = new ConventionalMapper().Map<ClassWithoutConstructor>(null);

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


			var ClassWithConstructor = new ConventionalMapper().Map<ClassWithConstructor>(car);
			var ClassWithoutConstructor = new ConventionalMapper().Map<ClassWithoutConstructor>(car);


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


			var b = new ConventionalMapper().Map<CarValue>(car);


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

			var twoLevel = new ConventionalMapper().Map<ClassWithTwoLevelEngine>(car);
			


			Assert.False(object.ReferenceEquals(car.TwoLevel, twoLevel?.TwoLevel));
			Assert.False(object.ReferenceEquals(car.TwoLevel.Engine, twoLevel?.TwoLevel.Engine));

		}

		[Fact]
		public void MapUsingKeyValueDictionayCaseInSensitive()
		{
			var keyValue = new Dictionary<string, object?>();
			keyValue["Name"] = "BMW";
			keyValue["manufacturer"] = "BM";
			keyValue["Engine"] = null;

			var c = new ConventionalMapper().Map<ClassWithoutConstructor>(keyValue);

			Assert.Equal("BMW", c?.Name);
			Assert.Equal("BM", c?.Manufacturer);
			Assert.Null(c?.Engine);
		}

		[Fact]
		public void MapUsingDictionaryMustWorkOnNestedClasses()
		{
			var keyValues = new Dictionary<string, object?>();
			keyValues["Name"] = "BMW";
			keyValues["manufacturer"] = "BM";
			keyValues["Engine"] = new Engine { Cylinder = 1, Manufacturer = "MM", Volume = 1 } ;

			var c = new ConventionalMapper().Map<ClassWithoutConstructor>(keyValues);

			Assert.Equal("BMW", c?.Name);
			Assert.Equal("BM", c?.Manufacturer);
			Assert.Equal((keyValues["Engine"] as Engine)?.Cylinder, c?.Engine.Cylinder);
		}

		[Fact]
		public void DeepMapTesting()
		{
			var customer = new Customer()
			{
				Id = 1,
				Email = "123@gmail.com",
				Address = "Ankara",
				LastName = "Ta",
				Name = "Mo"
			};

			var account = new Account()
			{
				Customer = customer,
				AccountNumber = "1",
				AccountType = "B",
				Created = DateTime.Now
			};

			var a = new ConventionalMapper().Map<AccountDto>(account);

			Assert.True(a.Customer.Name == account.Customer.Name);

		}

		[Fact]
		public void MapArrayObject()
		{
			Car[] c = new Car[]
			{
				new  Car
				{
					Name ="BMW",
					Manufacturer = "BMW",
					Engine = null
				},
				new  Car
				{
					Name = "Benz",
					Manufacturer = "Benz",
					Engine = new Engine
					{
						 Manufacturer = "Benz",
						 Cylinder = 8,
						 Volume = 3200
					}
				}

			};

			var cc = new ConventionalMapper();

			ClassWithoutConstructor[] a = new ClassWithoutConstructor[0];
			
			var array = cc.Map<ClassWithoutConstructor[]>(c);


			Assert.Equal(2, array!.Length);
			Assert.Equal("BMW", array[0].Name);
            Assert.Equal("Benz", array[1].Name);




        }

		[Fact]
		public void MapClassWithArrayProperty()
		{

			var c = new 
			{
				Name = "BMW",
				Cars = new Car[]
				{
					new Car
					{
						Name = "BMW",
						Manufacturer = "BMW",
						Engine = null
					},
					new Car
					{
						Name = "Benz",
						Manufacturer = "Benz",
						Engine = new Engine
						{
							Manufacturer = "Benz",
							Cylinder = 8,
							Volume = 3200
						}
					}
				}
            };


			var mapper = new ConventionalMapper();
			var a = mapper.Map<ClassWithArray>(c);

			Assert.Equal("BMW", a.Name);
            Assert.Equal("BMW", a.Cars[0].Name);
            Assert.Equal("Benz", a.Cars[1].Name);





        }


        //[Fact]
        //public void MapFromJsonStringMustWork()
        //{
        //    var json = "{ \"name\" : \"BMW\", \"manufacturer\" : \"BM\", \"Engine\":{\"Volume\":1} }";

        //    //var jObject = JObject.Parse(json);
        //    //var name = jObject.Property("name").ToObject(typeof(string));
        //    //var manu = jObject.Property("manufacturer").ToObject(typeof(string));
        //    //var eng = jObject.Property("Engine").To  ToObject(typeof(Engine));






        //    var c = new ConventionalMapper().Map<ClassWithoutConstructor>(json);

        //    //Assert.Equal("BMW", c?.Name);
        //    //Assert.Equal("manufacturer", c?.Manufacturer);

        //}



    }
}