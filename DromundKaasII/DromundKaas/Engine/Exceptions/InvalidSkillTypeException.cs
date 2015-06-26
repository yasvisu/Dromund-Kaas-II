using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.Exceptions
{
    public class InvalidSkillTypeException : Exception
    {
        public InvalidSkillTypeException()
            : base()
        { }

        public InvalidSkillTypeException(string message)
            : base(message)
        { }

        public InvalidSkillTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
