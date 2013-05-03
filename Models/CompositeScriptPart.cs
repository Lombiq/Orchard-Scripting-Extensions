using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;

namespace OrchardHUN.Scripting.Models
{
    public class CompositeScriptPart : ContentPart, IScriptAspect
    {
        private readonly LazyField<string> _engine = new LazyField<string>();
        public LazyField<string> EngineField { get { return _engine; } }
        public string Engine
        {
            get { return _engine.Value; }
        }

        private readonly LazyField<string> _expression = new LazyField<string>();
        public LazyField<string> ExpressionField { get { return _expression; } }
        public string Expression
        {
            get { return _expression.Value; }
        }
    }
}