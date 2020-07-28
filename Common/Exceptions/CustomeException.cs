
namespace CommonModel.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Define Custome Exception class
    /// </summary>
    public class CustomeException : Exception
    {
        /// <summary>
        /// Define Enum method
        /// </summary>
        public enum ExceptionType
        {
            /// <summary>
            /// Declare null field exception variable
            /// </summary>
            NULL_FIELD_EXCEPTION,

            /// <summary>
            /// Declare invalid field exception variable
            /// </summary>
            INVALID_FIELD_EXCEPTION,

            /// <summary>
            /// Declare unwanted exception variable
            /// </summary>
            UNWANTED_EXCEPTION,

            /// <summary>
            /// Declare empty field exception variable
            /// </summary>
            EMPTY_FIELD_EXCEPTION
        }

        /// <summary>
        /// Define exception type variable
        /// </summary>
        public ExceptionType type;

        /// <summary>
        /// Define Counstuctor
        /// </summary>
        /// <param name="type">Passing Exception type variable</param>
        /// <param name="message">Passing message string</param>
        public CustomeException(CustomeException.ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }

    }
}
