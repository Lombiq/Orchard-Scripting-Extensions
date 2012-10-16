using System.Collections.Generic;
namespace OrchardHUN.Scripting.ViewModels
{
    public class TestBedViewModel
    {
        public IEnumerable<string> RegisteredEngines { get; set; }
        public string SelectedEngine { get; set; }
        public string Code { get; set; }
        public string Output { get; set; }
    }
}