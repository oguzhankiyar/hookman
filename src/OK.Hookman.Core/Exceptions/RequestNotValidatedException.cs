using System;

namespace OK.Hookman.Core.Exceptions
{
    public class RequestNotValidatedException : Exception
    {
        public string[] Failures { get; set; }

        public RequestNotValidatedException()
        {
            Failures = new string[] { };
        }

        public RequestNotValidatedException(string[] failures)
        {
            Failures = failures;
        }

        public RequestNotValidatedException(string message) : base(message)
        {
            Failures = new string[] { };
        }

        public RequestNotValidatedException(string message, string[] failures) : base(message)
        {
            Failures = failures;
        }
    }
}