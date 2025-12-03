using System;

namespace Scheduling_App
{
    class LoginException : ApplicationException
    {
        public LoginException() : base("Incorrect form input")
        {
        }

        public LoginException(string message) : base(message)
        {
        }

        public LoginException(string message, ApplicationException inner) : base(message, inner)
        {
        }
    }
}
