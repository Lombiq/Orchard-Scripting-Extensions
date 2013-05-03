using JetBrains.Annotations;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment;
using OrchardHUN.Scripting.Models;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.Handlers
{
    [UsedImplicitly]
    public class ScriptPartHandler : ContentHandler
    {
        public ScriptPartHandler(
            IRepository<ScriptPartRecord> repository,
            Work<IScriptingManager> scriptingManagerWork)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<ScriptPart>((context, part) =>
            {
                part.RegisteredEnginesField.Loader(() => scriptingManagerWork.Value.ListRegisteredEngines());
            });
        }
    }
}