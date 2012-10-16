using Orchard.Localization;
using Orchard.UI.Navigation;
using Orchard.Security;

namespace OrchardHUN.Scripting
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder/*.AddImageSet("scripting")*/
                .Add(T("Scripting"), "4",
                    menu => menu.Action("Testbed", "Admin", new { area = "OrchardHUN.Scripting" }).Permission(StandardPermissions.SiteOwner));
        }
    }
}