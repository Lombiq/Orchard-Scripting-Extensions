using Orchard;
using OrchardHUN.Scripting.Models;
using System.Collections.Generic;

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