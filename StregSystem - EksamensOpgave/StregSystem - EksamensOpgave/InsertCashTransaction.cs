using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class InsertCashTransaction : Transaction
    {

        public override string ToString()
        {
            return "Indbetaling: " + _transactionAmount.ToString() + TransactionUser.ToString() + _transactionDate.ToString() + _transactionID.ToString();
        }

        public new bool Execute()
        {
            return true;
        }
    }
}
