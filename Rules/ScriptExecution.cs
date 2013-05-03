using System;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Events;
using Orchard.Localization;
using OrchardHUN.Scripting.Models;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.Rules
{
    public interface IActionProvider : IEventHandler
    {
        void Describe(dynamic describe);
    }

    public interface IFormProvider : IEventHandler
    {
        void Describe(dynamic context);
    }

    [OrchardFeature("OrchardHUN.Scripting.Rules")]
    public class ScriptExecutionAction : IActionProvider
    {
        private readonly IScriptingManager _scriptingManager;
        private readonly IContentManager _contentManager;

        public Localizer T { get; set; }


        public ScriptExecutionAction(IScriptingManager scriptingManager, IContentManager contentManager)
        {
            _scriptingManager = scriptingManager;
            _contentManager = contentManager;

            T = NullLocalizer.Instance;
        }
        

        public void Describe(dynamic describe)
        {
            Func<dynamic, LocalizedString> display = context => new LocalizedString(context.Properties["ScriptExecutionDescription"]);

            describe.For("Script Execution", T("Script Execution"), T("Script Execution"))
                .Element("ScriptExecution", T("Script Execution"), T("Executes a script using a scripting engine."), (Func<dynamic, bool>)Run, display, "ScriptExecution");
        }

        private bool Run(dynamic context)
        {
            using (var scope = _scriptingManager.CreateScope(context.Properties["ScriptExecutionDescription"] + " ActionScript"))
            {
                scope.SetVariable("Tokens", context.Tokens);
                var script = ((ContentItem)_contentManager.Get(int.Parse(context.Properties["ScriptExecutionScriptId"]), VersionOptions.Published)).As<IScriptAspect>();
                _scriptingManager.ExecuteExpression(script.Engine, script.Expression, scope);
            }

            return true;
        }
    }

    [OrchardFeature("OrchardHUN.Scripting.Rules")]
    public class ScriptExecutionForms : IFormProvider
    {
        private dynamic Shape { get; set; }
        private readonly IScriptingManager _scriptingManager;

        public Localizer T { get; set; }


        public ScriptExecutionForms(IShapeFactory shapeFactory, IScriptingManager scriptingManager)
        {
            Shape = shapeFactory;
            _scriptingManager = scriptingManager;

            T = NullLocalizer.Instance;
        }


        public void Describe(dynamic context)
        {
            Func<IShapeFactory, object> form =
                shape =>
                {
                    var engines = _scriptingManager.ListRegisteredEngines();
                    var f = Shape.Form(
                        Id: "ScriptExecution",
                        _Description: Shape.Textbox(
                            Id: "orchardhun-scripting-script-execution-description", Name: "ScriptExecutionDescription",
                            Title: T("Description"),
                            Description: T("Text that will be displayed in the Actions list."),
                            Classes: new[] { "textMedium" }),
                        _ScriptPicker: Shape.ScriptPicker(
                            Name: "ScriptExecutionScriptId")
                        );

                    return f;
                };

            context.Form("ScriptExecution", form);
        }
    }
}