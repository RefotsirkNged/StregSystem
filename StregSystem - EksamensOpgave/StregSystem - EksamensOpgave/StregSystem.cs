using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class StregSystem
    {
        public void BuyProduct(User user, Product product)
        {
            BuyTransaction trans = new BuyTransaction(product, user);
            trans.TransactionDate = DateTime.Now;
            trans.TransactionAmount = product.Price;
            trans.TransactionID = Guid.NewGuid();
                

            trans.Execute();
        }

        public void AddCreditsToAccount(User user, decimal amount)
        {

        }

        public void ExecuteTransaction(Transaction transaction)
        {

        }

        public void GetProduct()
        {

        }

        public void GetUser()
        {

        }

        public void GetTransactionList()
        {

        }

        public void GetActiveProducts()
        {

        }
    }
}
