using System;
using System.Text;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Notify;
using OrchardHUN.Scripting.Exceptions;
using OrchardHUN.Scripting.Models;
using OrchardHUN.Scripting.Models.Pages.Admin;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.Drivers.Pages.Admin
{
    public class ScriptingAdminTestbedPartDriver : ContentPartDriver<ScriptingAdminTestbedPart>
    {
        // No prefix here so the inner editor works
        private readonly IScriptingManager _scriptingManager;
        private readonly IContentManager _contentManager;
        private readonly INotifier _notifier;

        public Localizer T { get; set; }


        public ScriptingAdminTestbedPartDriver(
            IScriptingManager scriptingManager, 
            IContentManager contentManager,
            INotifier notifier)
        {
            _scriptingManager = scriptingManager;
            _contentManager = contentManager;
            _notifier = notifier;

            T = NullLocalizer.Instance;
        }


        protected override DriverResult Display(ScriptingAdminTestbedPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                        ContentShape("Pages_ScriptingAdminTestbed_ScriptPicker",
                            () => shapeHelper.DisplayTemplate(
                                        TemplateName: "Pages/Admin/Testbed.ScriptPicker",
                                        Model: part,
                                        Prefix: Prefix)),
                        ContentShape("Pages_ScriptingAdminTestbed",
                                () => {
                                    if (part.ScriptId == 0) part.EditorShape = part.EditorShape ?? _contentManager.BuildEditor(_contentManager.New("Script"));
                                    else if (part.Script == null) part.Script = _contentManager.Get(part.ScriptId, VersionOptions.Latest);

                                    return shapeHelper.DisplayTemplate(
                                            TemplateName: "Pages/Admin/Testbed",
                                            Model: part,
                                            Prefix: Prefix);
                                }));
        }

        protected override DriverResult Editor(ScriptingAdminTestbedPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            ContentItem script;
            if (part.ScriptId != 0)
            {
                script = part.Script = _contentManager.Get(part.ScriptId, VersionOptions.Latest);
                part.EditorShape = _contentManager.UpdateEditor(_contentManager.New("Script"), updater);
            }
            else
            {
                script = _contentManager.New("Script");
                part.EditorShape = _contentManager.UpdateEditor(script, updater);
            }


            var scriptPart = script.As<IScriptAspect>();
            if (scriptPart == null) throw new InvalidOperationException("The Script content type should have a part implementing IScriptAspect attached.");

            if (!String.IsNullOrEmpty(scriptPart.Expression))
            {
                try
                {
                    using (var scope = _scriptingManager.CreateScope("testbed"))
                    {
                        part.Output = _scriptingManager.ExecuteExpression(scriptPart.Engine, scriptPart.Expression, scope);
                    }
                }
                catch (ScriptRuntimeException ex)
                {
                    var builder = new StringBuilder();
                    for (Exception current = ex; current != null; current = current.InnerException)
                        builder.Append(Environment.NewLine + current.Message);

                    _notifier.Error(T("There was a glitch with your code: {0}", builder.ToString()));
                }
            }

            return Display(part, "Detail", shapeHelper);
        }
    }
}