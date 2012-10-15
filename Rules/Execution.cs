using System;
using System.Linq;
using System.Web.Mvc;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Events;
using Orchard.Localization;
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

        public ScriptExecutionAction(IScriptingManager scriptingManager)
        {
            _scriptingManager = scriptingManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

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
                _scriptingManager.ExecuteExpression(context.Properties["ScriptExecutionEngine"], context.Properties["ScriptExecutionScript"], scope);
            }

            return true;
        }
    }

    [OrchardFeature("OrchardHUN.Scripting.Rules")]
    public class ScriptExecutionForms : IFormProvider
    {
        protected dynamic Shape { get; set; }
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
                            Id: "scriptexecution-description", Name: "ScriptExecutionDescription",
                            Title: T("Description"),
                            Description: T("Message that will be displayed in the Actions list."),
                            Classes: new[] { "textMedium" }),
                        _Script: Shape.TextArea(
                            Id: "scriptexecution-script", Name: "ScriptExecutionScript",
                            Title: T("Script"),
                            Description: T("Enter some lines of code to run."),
                            Classes: new[] { "tokenized" }),
                        _Engine: Shape.SelectList(
                            Id: "scriptexecution-engine", Name: "ScriptExecutionEngine",
                            Title: T("Scripting Engine"),
                            Description: T("Select a scripting engine to run your code."),
                            Size: engines.Count(),
                            Multiple: false
                            )
                        );

                    foreach (var engine in engines)
                    {
                        f._Engine.Add(new SelectListItem { Value = engine, Text = engine });
                    }

                    return f;
                };

            context.Form("ScriptExecution", form);
        }
    }
}