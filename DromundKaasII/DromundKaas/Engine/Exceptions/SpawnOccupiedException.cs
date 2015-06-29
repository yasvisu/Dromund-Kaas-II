using System;

namespace DromundKaasII.Engine.Exceptions
{
    /// <summary>
    /// Thrown when the spawn point of a spawn candidate is occupied.
    /// </summary>
    public class SpawnOccupiedException : ApplicationException
    {
        /// <summary>
        /// Initializes a new SpawnOccupiedException.
        /// </summary>
        public SpawnOccupiedException()
            : base()
        { }

        /// <summary>
        /// Initializes a new SpawnOccupiedException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public SpawnOccupiedException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new SpawnOccupiedException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="inner">The exception's inner exception.</param>
        public SpawnOccupiedException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
