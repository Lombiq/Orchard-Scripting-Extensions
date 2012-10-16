using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrchardHUN.Scripting.Models.Pages.Admin;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement;
using OrchardHUN.Scripting.Exceptions;
using OrchardHUN.Scripting.Services;
using Orchard.UI.Notify;
using Orchard.Localization;

namespace OrchardHUN.Scripting.Drivers.Pages.Admin
{
    public class ScriptingAdminTestbedPartDriver : ContentPartDriver<ScriptingAdminTestbedPart>
    {
        private readonly IScriptingManager _scriptingManager;
        private readonly INotifier _notifier;

        public Localizer T { get; set; }


        public ScriptingAdminTestbedPartDriver(IScriptingManager scriptingManager, INotifier notifier)
        {
            _scriptingManager = scriptingManager;
            _notifier = notifier;

            T = NullLocalizer.Instance;
        }


        protected override DriverResult Display(ScriptingAdminTestbedPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Pages_ScriptingAdminTestbed",
                        () => shapeHelper.DisplayTemplate(
                                        TemplateName: "Pages/Admin/Testbed",
                                        Model: part,
                                        Prefix: Prefix));
        }

        protected override DriverResult Editor(ScriptingAdminTestbedPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            if (!String.IsNullOrEmpty(part.Expression))
            {
                try
                {
                    using (var scope = _scriptingManager.CreateScope("testbed"))
                    {
                        part.Output = _scriptingManager.ExecuteExpression(part.SelectedEngine, part.Expression, scope);
                    }
                }
                catch (ScriptRuntimeException ex)
                {
                    _notifier.Error(
                        T("There was a glitch with your code: {0}"
                        + Environment.NewLine + Environment.NewLine
                        + "Details:" + Environment.NewLine + "{1}", ex.Message, ex.InnerException.Message));
                }
            }

            return Display(part, "Detail", shapeHelper);
        }
    }
}