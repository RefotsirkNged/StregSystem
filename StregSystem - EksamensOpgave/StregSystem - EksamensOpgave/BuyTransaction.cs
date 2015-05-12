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

        public BuyTransaction(User user, Product product)
        {
            _transactionID = Guid.NewGuid();
            _user = user;
            _transactionDate = DateTime.Now;
            _transactionAmount = product.Price;
            _productBeingBought = product;
        }
        public new bool Execute()
        {

                if ((TransactionUser.Balance - TransactionAmount) < 0)
                {
                    throw new InsufficientCreditsException("Not enough cash!");
                }
            //If no exception is thrown, do the stuff
            TransactionUser.Balance -= TransactionAmount;
            return true;
        }

        public Product ProductBeingBought
        {
            get { return _productBeingBought; }
            set { _productBeingBought = value; }
        }
    }
}
