using System;

namespace OrchardHUN.Scripting.Exceptions
{
    [Serializable]
    public class ScriptRuntimeException : Exception
    {
        public ScriptRuntimeException(string message)
            : base(message)
        {
        }

        public ScriptRuntimeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}