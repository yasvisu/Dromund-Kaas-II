using System;

namespace DromundKaasII.Engine.Exceptions
{
    /// <summary>
    /// Thrown when an unsupported game input key is encountered.
    /// </summary>
    public class UnsupportedKeyException : ApplicationException
    {
        /// <summary>
        /// Initializes a new UnsupportedKeyException.
        /// </summary>
        public UnsupportedKeyException()
            : base()
        { }

        /// <summary>
        /// Initializes a new UnsupportedKeyException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public UnsupportedKeyException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new UnsupportedKeyException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="inner">The exception's inner exception.</param>
        public UnsupportedKeyException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
