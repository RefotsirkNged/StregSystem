using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class UserNotFoundExceptionException : Exception
    {
        public UserNotFoundExceptionException()
        {
        }

        public UserNotFoundExceptionException(string message)
            : base(message)
        {
        }

        public UserNotFoundExceptionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
