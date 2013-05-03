using System.Collections.Generic;
using Orchard;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// Central entry point to run scripts
    /// </summary>
    public interface IScriptingManager : IDependency
    {
        IEnumerable<IEngineDescriptor> ListRegisteredEngines();
        ScriptScope CreateScope(string name);
        dynamic ExecuteExpression(string engine, string expression, ScriptScope scope);
    }
}