using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    abstract class Transaction
    {
        protected Guid _transactionID;
        protected User _user;
        protected DateTime _transactionDate;
        protected decimal _transactionAmount;

        




        public bool Execute()
        {
            //Supposed to execute the transaction, transfer funds so on
            return true;
        }
        public override string ToString()
        {
            return _transactionID.ToString() + _transactionAmount.ToString() + _transactionDate.ToString();
        }

        #region Getter/setter
        public Guid TransactionID
        {
            get { return _transactionID; }
            set { _transactionID = value; }
        }

        public User TransactionUser
        {
            get { return _user; }
            set { _user = value; }
        }

        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set { _transactionDate = value; }
        }

        public decimal TransactionAmount
        {
            get { return _transactionAmount; }
            set { _transactionAmount = value; }
        } 
        #endregion
    }
}
