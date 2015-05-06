using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount)
        {
            _transactionID = Guid.NewGuid();
            _user = user;
            _transactionDate = DateTime.Now;
            _transactionAmount = amount;
        }

        public override string ToString()
        {
            return "Indbetaling: " + _transactionAmount.ToString() + TransactionUser.ToString() + _transactionDate.ToString() + _transactionID.ToString();
        }

        public new bool Execute()
        {
            TransactionUser.Balance += TransactionAmount;
            return true;
        }
    }
}
