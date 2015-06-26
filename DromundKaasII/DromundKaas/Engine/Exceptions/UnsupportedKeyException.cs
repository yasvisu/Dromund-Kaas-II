using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.Exceptions
{
    public class UnsupportedKeyException : ApplicationException
    {
        public UnsupportedKeyException()
            : base()
        { }

        public UnsupportedKeyException(string message)
            : base(message)
        { }

        public UnsupportedKeyException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
