using Orchard;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// Represents a scripting runtime (like PHP, C#) that can be used to evaluate script source code.
    /// </summary>
    public interface IScriptingRuntime : ISingletonDependency
    {
        string Engine { get; }
        dynamic ExecuteExpression(string expression, ScriptScope scope);
    }
}
