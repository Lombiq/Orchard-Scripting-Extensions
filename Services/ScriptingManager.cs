using System;
using System.Collections.Generic;
using System.Linq;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.Services
{
    public class ScriptingManager : IScriptingManager
    {
        private readonly Dictionary<string, IScriptingRuntime> _runtimes;


        public ScriptingManager(IEnumerable<IScriptingRuntime> runtimes)
        {
            _runtimes = runtimes.ToDictionary(runtime => runtime.Engine);
        }


        public IEnumerable<string> ListRegisteredEngines()
        {
            return _runtimes.Keys;
        }

        public ScriptScope CreateScope(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            return new ScriptScopeImpl(name);
        }

        public dynamic ExecuteExpression(string engine, string expression, ScriptScope scope)
        {
            if (!_runtimes.ContainsKey(engine)) throw new InvalidOperationException("No \"" + engine + "\" engine registered.");

            return _runtimes[engine].ExecuteExpression(expression, scope);
        }


        private class ScriptScopeImpl : ScriptScope
        {
            public ScriptScopeImpl(string name) : base(name)
            {
            }
        }
    }
}