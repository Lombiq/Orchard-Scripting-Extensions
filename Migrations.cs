using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using OrchardHUN.Scripting.Models;
using Orchard.Localization;

namespace OrchardHUN.Scripting
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(ScriptPartRecord).Name,
                table => table
                    .ContentPartVersionRecord()
                    .Column<string>("Engine", column => column.WithLength(256))
                    .Column<string>("Expression", column => column.Unlimited())
                );

            ContentDefinitionManager.AlterTypeDefinition("Script", 
                cfg => cfg
                    .WithPart("TitlePart")
                    .WithPart(typeof(ScriptPart).Name)
                    .WithPart("CommonPart")
                    .Creatable()
                    .Draftable()
            );

            ContentDefinitionManager.AlterPartDefinition("CompositeScriptFieldsPart",
                part => part
                    .WithField("Scripts", cfg => cfg
                        .OfType("ContentPickerField")
                        // Localization here throws an exception
                        .WithSetting("ContentPickerFieldSettings.Hint", "Select multiple script items. These will be executed sequentially (concatenated into a single script, sharing the same namespace) when this item is run. All scripts should use the same engine!")
                        .WithSetting("ContentPickerFieldSettings.Required", "true")
                        .WithSetting("ContentPickerFieldSettings.Multiple", "true"))
                );

            ContentDefinitionManager.AlterTypeDefinition("CompositeScript",
                cfg => cfg
                    .WithPart("TitlePart")
                    .WithPart(typeof(CompositeScriptPart).Name)
                    .WithPart("CompositeScriptFieldsPart")
                    .WithPart("CommonPart")
                    .Creatable()
                    .Draftable()
            );


            return 1;
        }
    }
}