using Orchard.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.EventHandlers
{
    public interface IScriptingEventHandler : IEventHandler
    {
        void BeforeExecution(BeforeExecutionContext context);
        void AfterExecution(AfterExecutionContext context);
    }

    public abstract class ScriptingEventContext
    {
        public string Engine { get; private set; }
        public string Expression { get; private set; }
        public ScriptScope Scope { get; private set; }

        protected ScriptingEventContext(string engine, string expression, ScriptScope scope)
        {
            Engine = engine;
            Expression = expression;
            Scope = scope;
        }
    }

    public class BeforeExecutionContext : ScriptingEventContext
    {
        public BeforeExecutionContext(string engine, string expression, ScriptScope scope)
            : base(engine, expression, scope)
        {
        }   
    }

    public class AfterExecutionContext : ScriptingEventContext
    {
        public dynamic Output { get; private set; }

        public AfterExecutionContext(string engine, string expression, ScriptScope scope, dynamic output)
            : base(engine, expression, scope)
        {
            Output = output;
        }   
    }
}
