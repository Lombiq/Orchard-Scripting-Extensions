using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;

namespace OrchardHUN.Scripting.Models
{
    public interface IScriptAspect : IContent
    {
        string Engine { get; }
        string Expression { get; }
    }
}
