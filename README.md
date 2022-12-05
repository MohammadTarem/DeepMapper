# Deep Mapper

It is an object mapper with zero configuration. It maps using two methods **conventional mapping** and **configurational mapping**. It also maps **nested classes** recursivley to a new instance. This library can be used for **deep copying** as well.


Add DI to the project. 

```
services.AddDeepMapping(config => 
{ 
     config.UseConventionalMapping();
});

```

### Conventional Mapping
This method requires no configurations and uses property names to map objects and set the unmatched properties to default.
Use **IConventionalMapper** interface or **IDeepMapper**. This class can be used **without DI** just by instantiating a new instance of ConventionalMapper class.

```
var obj = mapper.Map<TDestination>(TSource);

//or 

var obj = new ConventionalMapper().Map<TDestination>(TSource);

//or 

var destintion = new ConventionalMapper().Map(Type destinationType, object? sourceObject)

```

Conventioal mapping can use constructors to fill destination class.

```
class TDestination 
{
  public TProperty Property {get; private set;}
}

// Or

class TDestination 
{
  TDestination(TProperty property)
  {
     Property = property
  }
  public TProperty Property {get;}
}
 
```

Conventional mapper is able to map **structs** to **classes** (and vice versa) to fill properties with the same name. The convention is **case insensitive**. 




### Configurational Mapping 
In this method the library uses configuration for a types in the service registeration. 

```
config.Map<TSource, TDestination>()
              .ToProperty(destination => TProperty, source => TProperty)
              .ToConstant(destination => TProperty, object)
              .ToDefault(destination => TProperty)
              .Ignore(destination => TProperty);
```
The **UseConventionalMapping()** in the configuration enables the configurational mapper to map the rest of the properties using name conventions.

```
// Use IDeepMapper for configurational mapping
var obj = mapper.Map<TDestination>(TSource);

```

### Version 1.0.3
- **MapAsync** has been added to IDeepMapper for async mapping.
- Map functions has been added to IEnumerable interface for mapping collection in **DeepMapper.Extensions** namespace. This must be used when DI activated.
- The DI extension moved to new namespcace **DeepMapper.Extensions** alongside Map for IEnumerable.


```
// Map for IEnumerable
IEnumerable<TDestination> objects = IEnumerable.Map<TDestination>();

```





