using Orchard;
using Orchard.Localization;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// Represents a scripting runtime (like PHP, C#) that can be used to evaluate script source code.
    /// </summary>
    public interface IScriptingRuntime : ISingletonDependency
    {
        IEngineDescriptor Descriptor { get; }
        dynamic ExecuteExpression(string expression, ScriptScope scope);
    }

    public interface IEngineDescriptor
    {
        string Name { get; }
        LocalizedString DisplayName { get; }
    }

    public class EngineDescriptor : IEngineDescriptor
    {
        public string Name { get; private set; }
        public LocalizedString DisplayName { get; private set; }

        public EngineDescriptor(string name, LocalizedString displayName)
        {
            Name = name;
            DisplayName = displayName;
        }
    }
}
