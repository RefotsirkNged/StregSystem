using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class NoProductsFoundException : Exception
    {
        public NoProductsFoundException()
        {
        }

        public NoProductsFoundException(string message)
            : base(message)
        {
        }

        public NoProductsFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
