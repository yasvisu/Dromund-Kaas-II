using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Graphics.Exceptions
{
    public class EngineFailureException : ApplicationException
    {
        public EngineFailureException()
            : base()
        { }

        public EngineFailureException(string message)
            : base(message)
        { }

        public EngineFailureException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
