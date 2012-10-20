using Orchard;
using Orchard.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrchardHUN.Scripting.EventHandlers
{
    public class DefaultAssemblyLoader : IScriptingEventHandler
    {
        public void BeforeExecution(BeforeExecutionContext context)
        {
            var scope = context.Scope;
            scope.LoadAssembly(typeof(string).Assembly); // mscorlib.dll
            scope.LoadAssembly(typeof(Uri).Assembly); // System.dll
            scope.LoadAssembly(typeof(HttpContext).Assembly); // System.Web.dll
            scope.LoadAssembly(typeof(AjaxHelper).Assembly); // System.Web.Mvc.dll
            scope.LoadAssembly(typeof(Shapes).Assembly); // Orchard Core
            scope.LoadAssembly(typeof(WorkContext).Assembly); // Orchard Framework
        }

        public void AfterExecution(AfterExecutionContext context)
        {
        }
    }
}