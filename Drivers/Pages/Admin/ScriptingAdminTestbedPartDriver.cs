﻿using System;
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
using OrchardHUN.Scripting.Models;

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