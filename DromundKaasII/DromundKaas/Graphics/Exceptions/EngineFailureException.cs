using System;

namespace DromundKaasII.Graphics.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an exception is thrown within the engine.
    /// </summary>
    public class EngineFailureException : ApplicationException
    {
        /// <summary>
        /// Initializes a new EngineFailureException.
        /// </summary>
        public EngineFailureException()
            : base()
        { }

        /// <summary>
        /// Initializes a new EngineFailureException with a message.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public EngineFailureException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new EngineFailureException with a message and an internal exception.
        /// </summary>
        /// <param name="message">The message of this exception.</param>
        /// <param name="inner">The internal exception of this exception.</param>
        public EngineFailureException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
