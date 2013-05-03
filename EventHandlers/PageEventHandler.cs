using OrchardHUN.Scripting.Models.Pages.Admin;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

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