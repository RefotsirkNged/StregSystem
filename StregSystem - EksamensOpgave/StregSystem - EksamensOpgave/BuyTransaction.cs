using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class BuyTransaction : Transaction
    {
        private Product _productBeingBought;
        private decimal _amount;

        public BuyTransaction(Guid transactionID, User user, DateTime transactionDate, decimal transactionAmount, Product product)
        {
            _transactionID = transactionID;
            _user = user;
            _transactionDate = transactionDate;
            _transactionAmount = transactionAmount;
            _productBeingBought = product;
        }
        public new bool Execute()
        {
            //brug insufficientCreditsException her hvis der ikke er nok credits
            return true;
        }
    }
}
