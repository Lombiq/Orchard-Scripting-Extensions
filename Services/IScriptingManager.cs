using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace OrchardHUN.Scripting.Services
{
    /// <summary>
    /// Central entry point to run scripts
    /// </summary>
    /// <remarks>
    /// Gets IScriptingRuntimes injected.
    /// </remarks>
    public interface IScriptingManager : IDependency
    {
        object Run(string engine, string code);
    }
}