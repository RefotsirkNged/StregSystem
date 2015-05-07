using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        public List<Product> LoadProductData(string path)
        {
            List<Product> productsFound = new List<Product>();
            List<string[]> dataFound = new List<string[]>();
            using (StreamReader readCSVFile = new StreamReader(path, Encoding.Default))
            {
                string linje;
                string[] række;
                while ((linje = readCSVFile.ReadLine()) != null)
                {
                    række = linje.Split(';');
                    dataFound.Add(række);
                }
            }

            findAndDestroyHTML(dataFound);

            foreach (var item in dataFound)
            {
                Product p = new Product();
                int id;
                Int32.TryParse(item[0], out id);
                p.ProductID = id;
                p.ProductName = item[1];
                decimal price;
                Decimal.TryParse(item[2], out price);
                p.Price = price;
                bool isActive;
                Boolean.TryParse(item[3], out isActive);
                p.Active = isActive;
                productsFound.Add(p);
            }
            return productsFound;
        }
        public List<string[]> findAndDestroyHTML(List<string[]> dataFound)
        {
            foreach (var item in dataFound)
            {
                if (item[1].Contains("<b>") || item[1].Contains("</b>"))
                {
                    item[1].Replace("<b>", "");
                    item[1].Replace("</b>", "");
                }
                if(item[1].Contains("<h2>") || item[1].Contains("</h2>"))
                {
                    item[1].Replace("<h2>", "");
                    item[1].Replace("</h2>", "");
                }
                if (item[1].Contains("<h1>") || item[1].Contains("</h1>"))
                {
                    item[1].Replace("<h1>", "");
                    item[1].Replace("</h1>", "");
                }
            }
            return dataFound;
        }
    }
}
