using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException()
        {
        }

        public TransactionNotFoundException(string message)
            : base(message)
        {
        }

        public TransactionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
