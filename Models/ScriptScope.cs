using System;
using System.Collections.Generic;

namespace OrchardHUN.Scripting.Models
{
    public abstract class ScriptScope : IDisposable
    {
        protected readonly Dictionary<string, dynamic> _variables = new Dictionary<string, dynamic>();

        public string Name { get; private set; }
        public IEnumerable<KeyValuePair<string, dynamic>> Variables
        {
            // This should rather return a ReadOnlyDictionary: http://msdn.microsoft.com/en-us/library/gg712875%28v=VS.110%29.aspx
            // Revise after .NET 4.5 upgrade
            get { return _variables; }
        }


        protected ScriptScope(string name)
        {
            Name = name;
        }


        public dynamic GetVariable(string name)
        {
            if (!_variables.ContainsKey(name)) return null;
            return _variables[name];
        }

        public void SetVariable(string name, dynamic value)
        {
            _variables[name] = value;
        }

        public void Dispose()
        {
            _variables.Clear();
        }
    }
}