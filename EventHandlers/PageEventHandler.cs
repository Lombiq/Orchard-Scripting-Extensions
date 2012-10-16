using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using OrchardHUN.Scripting.Models.Pages.Admin;
using OrchardHUN.Scripting.Services;
using Orchard.Environment;

namespace OrchardHUN.Scripting.EventHandlers
{
    public class PageEventHandler : IPageEventHandler
    {
        private readonly Work<IScriptingManager> _scriptingManagerWork;


        public PageEventHandler(Work<IScriptingManager> scriptingManagerWork)
        {
            _scriptingManagerWork = scriptingManagerWork;
        }


        public void OnPageInitializing(PageContext pageContext)
        {
            if (pageContext.Group != PageConfigs.AdminGroup) return;

            var testbedPart = new ScriptingAdminTestbedPart();
            testbedPart.RegisteredEnginesField.Loader(() => _scriptingManagerWork.Value.ListRegisteredEngines());
            pageContext.Page.ContentItem.Weld(testbedPart);
        }

        public void OnPageInitialized(PageContext pageContext)
        {
        }

        public void OnPageBuilt(PageContext pageContext)
        {
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
        }
    }
}