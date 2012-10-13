using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// Represents a scripting runtime (like PHP, C#) that can be used to evaluate script source.
    /// </summary>
    public interface IScriptingRuntime : IDependency
    {
        string Engine { get; }
        object Run(string code);
    }
}
