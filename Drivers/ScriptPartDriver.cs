using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.Drivers
{
    public class ScriptPartDriver : ContentPartDriver<ScriptPart>
    {
        protected override string Prefix
        {
            get { return "OrchardHUN.Scripting.ScriptPart"; }
        }

        protected override DriverResult Editor(ScriptPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Script_Edit",
                    () => shapeHelper.EditorTemplate(
                            TemplateName: "Parts.Script",
                            Model: part,
                            Prefix: Prefix));
        }

        protected override DriverResult Editor(ScriptPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(ScriptPart part, ExportContentContext context)
        {
            var partName = part.PartDefinition.Name;

            context.Element(partName).SetAttributeValue("Engine", part.Engine);
            context.Element(partName).SetAttributeValue("Expression", part.Expression);
        }

        protected override void Importing(ScriptPart part, ImportContentContext context)
        {
            var partName = part.PartDefinition.Name;

            context.ImportAttribute(partName, "Engine", value => part.Engine = value);
            context.ImportAttribute(partName, "Expression", value => part.Expression = value);
        }
    }
}