using System.Collections.Generic;
using Orchard.DisplayManagement.Shapes;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// This class provides a statically-types proxy for accessing shape objects so scriping environments not 
    /// supporting dynamic objects can also deal with them.
    /// </summary>
    public class StaticShape
    {
        private readonly dynamic _shape;

        public ShapeMetadata Metadata { get { return _shape.Metadata; } }
        public string Id { get { return _shape.Id; } }
        public IList<string> Classes { get { return _shape.Classes; } }
        public IDictionary<string, string> Attributes { get { return _shape.Attributes; } }
        public IEnumerable<dynamic> Items { get { return _shape.Items; } }
        public object Value { get { return _shape; } }


        public StaticShape(dynamic shape)
        {
            _shape = shape;
        }


        // Argument defaults don't work from PHP
        public StaticShape Add(object item)
        {
            return Add(item, null);
        }

        public StaticShape Add(object item, string position)
        {
            _shape.Add(item, position);
            return this;
        }

        public StaticShape AddRange(IEnumerable<object> items)
        {
            return AddRange(items, "5");
        }

        public StaticShape AddRange(IEnumerable<object> items, string position)
        {
            _shape.AddRange(items, position);
            return this;
        }

        public StaticShape Get(string name)
        {
            return new StaticShape(_shape[name]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}