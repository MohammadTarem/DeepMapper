using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapper
{
    public abstract class Bind
    {
        public Bind(string destination)
        {
            Destination = destination;
        }
        public string Destination { get; }
    }

    internal class PropertyBind : Bind
    {
        public PropertyBind(string source, string destination) : base(destination)
        {
            Source = source;
        }
        public string Source { get; }
    }

    internal class ConstantBind : Bind
    {
        public ConstantBind(object? constant, string destination) : base(destination)
        {
            Constant = constant;
        }

        public object? Constant {get;}

    }

    internal class IgnoreBind : Bind
    {
        public IgnoreBind(string destination) : base(destination) { }
        
    }

    internal class DefaultBind : Bind
    {
        public DefaultBind(string destination) : base(destination) { }
    }



}
