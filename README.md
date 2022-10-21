# Deep Mapper

It is an object mapper with zero configuration. It maps using two methods **conventional mapping** and **configurational mapping**. It also maps the nested classes recursivley to a new instance. This library can be used for deep copying as well.


Add DI to the project. 

```
services.AddDeepMapping(config => 
{ 
     config.UseConventionalMapping();
});

```

### Conventional Mapping
This method requires no configurations and uses property names to map objects and set the unmatched properties to default.
Use **IConventionalMapper** interface or **IDeepMapper**.

```
var obj = mapper.Map<TDestination>(TSource);

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






