using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapper
{
    internal static class TypeExtensions
    {
        public static ConstructorInfo? GetConstroctorWithMaxParams(this Type type) => type.GetConstructors()
                       .MaxBy(ctor => ctor.GetParameters().Length);

        public static IEnumerable<PropertyInfo> GetWritableProperties(this Type type) =>
            type.GetProperties().Where(p => p.CanWrite);

        public static PropertyInfo? GetProperty(this Type type, string name, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return type.GetProperty(name.ToLower(),
                                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            }
            else
            {
                return type.GetProperty(name);
            }
        }

        public static bool IsNestedProperty(this PropertyInfo property) =>
                    property.PropertyType.IsClass && property.PropertyType != typeof(string);

        public static ConstructorInfo? GetDefaultConstructor(this Type type) => 
            type.GetConstructors().FirstOrDefault( c => c.GetParameters().Length == 0);

        public static object? ConstructNewInstance(this Type type) => type.Assembly.CreateInstance(type.FullName!);
        
    }
}
