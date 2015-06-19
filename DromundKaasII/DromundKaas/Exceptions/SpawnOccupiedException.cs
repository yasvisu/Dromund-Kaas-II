using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Exceptions
{
    public class SpawnOccupiedException : Exception
    {
        public SpawnOccupiedException()
            : base()
        { }

        public SpawnOccupiedException(string message)
            : base(message)
        { }

        public SpawnOccupiedException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
