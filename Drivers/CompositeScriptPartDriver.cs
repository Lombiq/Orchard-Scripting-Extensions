using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Drivers;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.Drivers
{
    // The sole purpose of this driver is to enable the "casting" of content items to CompositeScriptPart
    public class CompositeScriptPartDriver : ContentPartDriver<CompositeScriptPart>
    {
    }
}