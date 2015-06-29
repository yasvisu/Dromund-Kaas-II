using System;

namespace DromundKaasII.Engine.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid skill type is encountered.
    /// </summary>
    public class InvalidSkillTypeException : ApplicationException
    {
        /// <summary>
        /// Initializes a new InvalidSkillTypeException.
        /// </summary>
        public InvalidSkillTypeException()
            : base()
        { }

        /// <summary>
        /// Initializes a new InvalidSkillTypeException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public InvalidSkillTypeException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new InvalidSkillTypeException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="inner">The exception's inner exception.</param>
        public InvalidSkillTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
