using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using JetBrains.Annotations;
using OrchardHUN.Scripting.Models;
using Orchard.Environment;
using Piedone.HelpfulLibraries.Contents;
using Orchard.ContentManagement;
using Orchard.ContentPicker.Fields;
using System.Text;

namespace OrchardHUN.Scripting.Handlers
{
    [UsedImplicitly]
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