using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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