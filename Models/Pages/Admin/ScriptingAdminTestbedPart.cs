using Orchard.ContentManagement;

namespace OrchardHUN.Scripting.Models.Pages.Admin
{
    public class ScriptingAdminTestbedPart : ContentPart
    {
        public int ScriptId { get; set; }
        public ContentItem Script { get; set; }
        public dynamic EditorShape { get; set; }
        public string Output { get; set; }
        public bool ConvertOutputLinebreaks { get; set; }
    }
}