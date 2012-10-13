using Orchard;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// Central entry point to run scripts
    /// </summary>
    public interface IScriptingManager : IDependency
    {
        ScriptScope CreateScope(string name);
        dynamic ExecuteExpression(string engine, string expression, ScriptScope scope);
    }
}