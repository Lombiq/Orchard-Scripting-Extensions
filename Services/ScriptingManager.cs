using System;
using System.Collections.Generic;
using System.Linq;
using OrchardHUN.Scripting.Models;
using OrchardHUN.Scripting.EventHandlers;

namespace OrchardHUN.Scripting.Services
{
    public class ScriptingManager : IScriptingManager
    {
        private readonly Dictionary<string, IScriptingRuntime> _runtimes;
        private readonly IScriptingEventHandler _eventHandler;


        public ScriptingManager(IEnumerable<IScriptingRuntime> runtimes, IScriptingEventHandler eventHandler)
        {
            _runtimes = runtimes.ToDictionary(runtime => runtime.Engine);
            _eventHandler = eventHandler;
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

            _eventHandler.BeforeExecution(new BeforeExecutionContext(engine, expression, scope));
            var output = _runtimes[engine].ExecuteExpression(expression, scope);
            _eventHandler.AfterExecution(new AfterExecutionContext(engine, expression, scope, output));
            return output;
        }


        private class ScriptScopeImpl : ScriptScope
        {
            public ScriptScopeImpl(string name) : base(name)
            {
            }
        }
    }
}