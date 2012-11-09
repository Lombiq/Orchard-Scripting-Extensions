using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.ComponentModel.DataAnnotations;
using Orchard.Data.Conventions;
using Orchard.Core.Common.Utilities;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.Models
{
    public class ScriptPart : ContentPart<ScriptPartRecord>, IScriptAspect
    {
        public string Engine
        {
            get { return Record.Engine; }
            set { Record.Engine = value; }
        }

        public string Expression
        {
            get { return Record.Expression; }
            set { Record.Expression = value; }
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

        [StringLengthMax]
        public virtual string Expression { get; set; }
    }
}