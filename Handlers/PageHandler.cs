using Orchard.ContentManagement.Handlers;
using OrchardHUN.Scripting.Models.Pages.Admin;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Piedone.HelpfulLibraries.Contents;

namespace OrchardHUN.Scripting.Handlers
{
    public class PageHandler : ContentHandler
    {
        public void OnPageInitializing(PageContext pageContext)
        {
            if (!pageContext.Page.IsPage("Testbed", PageConfigs.AdminGroup)) return;

            pageContext.Page.Weld<ScriptingAdminTestbedPart>();
        }
    }
}