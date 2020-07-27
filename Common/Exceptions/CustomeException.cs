using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModel.Exceptions
{
    public class CustomeException : Exception
    {

        public enum ExceptionType
        {
            NULL_FIELD_EXCEPTION,
            INVALID_FIELD_EXCEPTION,
            UNWANTED_EXCEPTION,
            EMPTY_FIELD_EXCEPTION
        }

        public ExceptionType type;

        public CustomeException(CustomeException.ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }

    }
}
