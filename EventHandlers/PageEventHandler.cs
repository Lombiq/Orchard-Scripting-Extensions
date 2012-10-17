using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using OrchardHUN.Scripting.Models.Pages.Admin;
using OrchardHUN.Scripting.Services;
using Orchard.Environment;
using Orchard.ContentManagement;

namespace OrchardHUN.Scripting.EventHandlers
{
    public class PageEventHandler : IPageEventHandler
    {
        public void OnPageInitializing(PageContext pageContext)
        {
            if (!pageContext.Page.IsPage("Testbed", PageConfigs.AdminGroup)) return;

            pageContext.Page.ContentItem.Weld(new ScriptingAdminTestbedPart());
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