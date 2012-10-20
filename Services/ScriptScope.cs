using System;
using System.Collections.Generic;
using System.Reflection;

namespace OrchardHUN.Scripting.Services
{
    public abstract class ScriptScope : IDisposable
    {
        protected readonly Dictionary<string, dynamic> _variables = new Dictionary<string, dynamic>();
        protected readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();

        public string Name { get; private set; }

        public IEnumerable<KeyValuePair<string, dynamic>> Variables
        {
            get { return _variables; }
        }

        public IEnumerable<Assembly> Assemblies
        {
            get { return _assemblies; }
        }


        protected ScriptScope(string name)
        {
            Name = name;
        }


        public bool ContainsVariable(string name)
        {
            return GetVariable(name) != null;
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

        public void RemoveVariable(string name)
        {
            if (ContainsVariable(name)) _variables.Remove(name);
        }

        public void LoadAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
        }

        public bool ContainsAssembly(Assembly assembly)
        {
            return _assemblies.Contains(assembly);
        }

        public void RemoveAssembly(Assembly assembly)
        {
            if (ContainsAssembly(assembly)) _assemblies.Remove(assembly);
        }

        public void Dispose()
        {
            _variables.Clear();
        }
    }
}