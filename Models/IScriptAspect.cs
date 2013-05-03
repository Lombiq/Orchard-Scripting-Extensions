using Orchard.ContentManagement;

namespace OrchardHUN.Scripting.Models
{
    public interface IScriptAspect : IContent
    {
        string Engine { get; }
        string Expression { get; }
    }
}
