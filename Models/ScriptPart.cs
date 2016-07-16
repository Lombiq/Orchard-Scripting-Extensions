using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.ContentManagement.Utilities;
using Orchard.Data.Conventions;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.Models
{
    public class ScriptPart : ContentPart<ScriptPartRecord>, IScriptAspect
    {
        public string Engine
        {
            get { return Retrieve(x => x.Engine); }
            set { Store(x => x.Engine, value); }
        }

        public string Expression
        {
            get { return this.RetrieveVersioned<string>("Expression"); }
            set { this.StoreVersioned<string>("Expression", value); }
        }

        private readonly LazyField<IEnumerable<IEngineDescriptor>> _registeredEngines = new LazyField<IEnumerable<IEngineDescriptor>>();
        public LazyField<IEnumerable<IEngineDescriptor>> RegisteredEnginesField { get { return _registeredEngines; } }
        public IEnumerable<IEngineDescriptor> RegisteredEngines
        {
            get { return _registeredEngines.Value; }
        }
    }

    public class ScriptPartRecord : ContentPartVersionRecord
    {
        [StringLength(256)]
        public virtual string Engine { get; set; }
    }
}