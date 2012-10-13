using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.UI.Navigation;
using Orchard.Localization;

namespace OrchardHUN.Scripting
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder/*.AddImageSet("scripting")*/
                .Add(T("Scripting"), "4");
        }
    }
}