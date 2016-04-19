using System;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentPicker.Fields;
using OrchardHUN.Scripting.Models;
using Piedone.HelpfulLibraries.Contents;

namespace OrchardHUN.Scripting.Handlers
{
    public class CompositeScriptPartHandler : ContentHandler
    {
        public CompositeScriptPartHandler()
        {
            OnActivated<CompositeScriptPart>((context, part) =>
            {
                part.EngineField.Loader(() =>
                    {
                        var contentItems = context.ContentItem.AsField<ContentPickerField>("CompositeScriptFieldsPart").ContentItems;
                        if (contentItems.Count() == 0) return "";
                        return contentItems.First().As<ScriptPart>().Engine;
                    });

                part.ExpressionField.Loader(() =>
                    {
                        var contentItems = context.ContentItem.AsField<ContentPickerField>("CompositeScriptFieldsPart").ContentItems;
                        if (contentItems.Count() == 0) return "";

                        return String.Join("", contentItems.Select(item => item.As<ScriptPart>().Expression));
                    });
            });
        }
    }
}