using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class StregSystem
    {
        public List<Transaction> TransactionList = new List<Transaction>();
        public List<User> UserList = new List<User>();
        public List<Product> ProductList = new List<Product>();
        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product);
            ExecuteTransaction(transaction);
        }

        public void AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
        }
       
        public void ExecuteTransaction(Transaction transaction)
        {
            if (transaction.Execute())
            {
                TransactionList.Add(transaction);
            }

            //Hvis transaction.execute returner false, hvad skal der så ske?
        }

        public Product GetProduct(int productID)
        {
            foreach (Product product in ProductList)
            {
                if (product.ProductID == productID)
                {
                    return product;
                }
            }
                throw new ProductNotFoundException("Product Not Found");
        }

        public User GetUser(string userName)
        {

            foreach (User user in UserList)
            {
                if (user.UserName == userName)
                {
                    return user;
                }
            }
            throw new ProductNotFoundException("User not found!");

        }

        public List<Transaction> GetTransactionList(User user)
        {
            List<Transaction> transactionsFound = new List<Transaction>();
            foreach (Transaction transaction in TransactionList)
            {
                if (transaction.TransactionUser == user)
                {
                    transactionsFound.Add(transaction);
                }
            }
            if (transactionsFound == null)
            {
                throw new TransactionNotFoundException("No transactions found for this user!");
            }
            return transactionsFound;
        }

        public List<Product> GetActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();
            foreach (Product product in ProductList)
            {
                if (product.Active)
                {
                    activeProducts.Add(product);
                }
            }
            if (activeProducts == null)
            {
                throw new NoProductsFoundException("No active products found!");
            }
            return activeProducts;
        }
    }
}
