using Orchard.UI.Resources;

namespace OrchardHUN.Scripting
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("AceEditor").SetUrl("ace/min-noconflict/ace.js", "ace/noconflict/ace.js").SetCdn("//d1n0x3qji82z53.cloudfront.net/src-min-noconflict/ace.js");
            manifest.DefineScript("ScriptingAdmin").SetDependencies("AceEditor", "jQuery");
            
            manifest.DefineStyle("ScriptingAdmin").SetUrl("orchardhun-scripting-admin.css");
        }
    }
}