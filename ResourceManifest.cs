using Orchard.UI.Resources;

namespace OrchardHUN.Scripting
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineStyle("ScriptingAdmin").SetUrl("orchardhun-scripting-admin.css");
        }
    }
}