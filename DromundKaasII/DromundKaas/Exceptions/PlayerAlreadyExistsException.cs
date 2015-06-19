using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Exceptions
{
    public class PlayerAlreadyExistsException : Exception
    {
        public PlayerAlreadyExistsException()
            : base()
        { }

        public PlayerAlreadyExistsException(string message)
            : base(message)
        { }

        public PlayerAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
