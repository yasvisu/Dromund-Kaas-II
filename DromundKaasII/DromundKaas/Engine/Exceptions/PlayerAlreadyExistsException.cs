using System;

namespace DromundKaasII.Engine.Exceptions
{
    /// <summary>
    /// Thrown when a player exists and that's a problem.
    /// </summary>
    public class PlayerAlreadyExistsException : ApplicationException
    {
        /// <summary>
        /// Initializes a new PlayerAlreadyExistsException.
        /// </summary>
        public PlayerAlreadyExistsException()
            : base()
        { }

        /// <summary>
        /// Initializes a new PlayerAlreadyExistsException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public PlayerAlreadyExistsException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new PlayerAlreadyExistsException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="inner">The exception's inner exception.</param>
        public PlayerAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
