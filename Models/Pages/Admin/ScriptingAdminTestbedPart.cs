using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;

namespace OrchardHUN.Scripting.Models.Pages.Admin
{
    public class ScriptingAdminTestbedPart : ContentPart
    {
        private readonly LazyField<IEnumerable<string>> _registeredEngines = new LazyField<IEnumerable<string>>();
        public LazyField<IEnumerable<string>> RegisteredEnginesField { get { return _registeredEngines; } }
        public IEnumerable<string> RegisteredEngines
        {
            get { return _registeredEngines.Value; }
        }

        public string SelectedEngine { get; set; }
        public string Expression { get; set; }
        public string Output { get; set; }
    }
}