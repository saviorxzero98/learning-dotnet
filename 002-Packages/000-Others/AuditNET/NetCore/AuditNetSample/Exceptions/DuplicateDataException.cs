using System;

namespace AuditNetSample.Exceptions
{
    public class DuplicateDataException : Exception
    {
        public DuplicateDataException()
        {

        }
        public DuplicateDataException(string message) : base (message)
        {

        }
        public DuplicateDataException(string message, Exception innerException) : base (message, innerException)
        {

        }
    }
}
