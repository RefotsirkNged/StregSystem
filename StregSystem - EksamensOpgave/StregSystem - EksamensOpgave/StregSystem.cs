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
        //De lister med data der bliver brugt igennem programmet.
        public List<Transaction> TransactionList = new List<Transaction>();
        public List<User> UserList = new List<User>();
        public List<Product> ProductList = new List<Product>();

        //buyproduct, addcreditstoaccount og executetransaction bliver brugt til at håndtere transactioner, og få dem added rigtigt til de forskellige lister/skrevet til logfilen
        public void BuyProduct(User user, Product product)
        {
            //Laver en transaction, og executer den
            BuyTransaction transaction = new BuyTransaction(user, product);
            ExecuteTransaction(transaction);
        }

        public void AddCreditsToAccount(User user, decimal amount)
        {
            //Laver en transaction, og executer den
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
        }
       
        public void ExecuteTransaction(Transaction transactionIn)
        {
            //Denne funktion er splittet i to dele, for at tage højde for om det er en BuyTransaction eller en InsertCashTransaction
            if (transactionIn is BuyTransaction)
            {
                var transaction = (BuyTransaction)transactionIn;
                if (transaction.Execute())
                {
                    TransactionList.Add(transaction);
                    WriteTransactionToFile(transaction);
                }
                else
                {
                    throw new ArgumentException("Transaction Failed!");
                }
            }
            if (transactionIn is InsertCashTransaction)
            {
                var transaction = (InsertCashTransaction)transactionIn;
                if (transaction.Execute())
                {
                    TransactionList.Add(transaction);
                    WriteTransactionToFile(transaction);
                }
                else
                {
                    throw new ArgumentException("Transaction Failed!");
                }
            }
            

        }

        //GetProduct, GetUser, GetTransactionsList og GetActiveProducts er implementerede, men bliver i realiteten ikke brugt. 
        //Jeg har istedet brugt nogle nogle lambda sætninger til at slå brugere/produkter op, og istedet for at lave en hel seperat liste til de aktive produkter, har jeg alle i en liste, 
        //og så bliver de vist hvis deres bool isActive er true.
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

        //loads data from the file
        public List<Product> LoadProductData(string path)
        {
            List<Product> productsFound = new List<Product>();
            List<string[]> dataFound = new List<string[]>();
            using (StreamReader readCSVFile = new StreamReader(path, Encoding.Default))
            {
                string linje;
                string[] række;
                //læser en linje, og splitter den op i elementer der svarer til en property i et product
                while ((linje = readCSVFile.ReadLine()) != null)
                {
                    række = linje.Split(';');
                    dataFound.Add(række);
                }
            }

            //tilføjer til en midlertidig product p, og putter den ud i listen, der så til sidst bliver returned.
            foreach (var item in dataFound)
            {

                Product p = new Product();
                int id;
                Int32.TryParse(item[0], out id);
                p.ProductID = id;
                p.ProductName = item[1];
                decimal price;
                Decimal.TryParse(item[2], out price);
                p.Price = price/100;
                if (item[3] == "0")
                    p.Active = false;
                if (item[3] == "1")
                    p.Active = true;
                findAndDestroyHTML(p);
                productsFound.Add(p);
            }
            return productsFound;
        }

        public void WriteTransactionToFile(Transaction itemIn)
        {
            //splittet op i to dele, en for buytransaction, og en for insertcashtransaction
            //ellers pænt standard, når den bliver kaldt, skriver den den pågældende transaktion ud i logfilen.
            if (itemIn is BuyTransaction)
            {
                var item = (BuyTransaction)itemIn;
                if (!File.Exists("TransactionList.txt"))
                {
                    using (StreamWriter sw = File.CreateText("TransactionList.txt"))
                    {
                        sw.WriteLine(item.TransactionID + "\t" + item.TransactionDate + "\t" + item.TransactionUser.UserName + "\t" + item.TransactionAmount + ".kr\t" + item.ProductBeingBought.ProductName);
                        sw.WriteLine("\n");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("TransactionList.txt"))
                    {
                        sw.WriteLine(item.TransactionID + "\t" + item.TransactionDate + "\t" + item.TransactionUser.UserName + "\t" + item.TransactionAmount + ".kr\t" + item.ProductBeingBought.ProductName);
                        sw.WriteLine("\n");
                    }
                }
            }
            if (itemIn is InsertCashTransaction)
            {
                var item = (InsertCashTransaction)itemIn;
                if (!File.Exists("TransactionList.txt"))
                {
                    using (StreamWriter sw = File.CreateText("TransactionList.txt"))
                    {
                        sw.WriteLine(item.TransactionID + "\t" + item.TransactionDate + "\t" + item.TransactionUser.UserName + "\t" + item.TransactionAmount + ".kr\t");
                        sw.WriteLine("\n");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("TransactionList.txt"))
                    {
                        sw.WriteLine(item.TransactionID + "\t" + item.TransactionDate + "\t" + item.TransactionUser.UserName + "\t" + item.TransactionAmount + ".kr\t");
                        sw.WriteLine("\n");
                    }
                }
            }
            
            
        }
        public void findAndDestroyHTML(Product p)
        {
            //Bliver brugt til at fjerne unødvendig HTML kode i .csv filen. Flyttet herind for klarhed 
            if (p.ProductName.Contains("<b>"))
            {
                p.ProductName = p.ProductName.Replace("<b>", "");
                p.ProductName = p.ProductName.Replace("</b>", "");
            }
            if (p.ProductName.Contains("<h2>"))
            {
                p.ProductName = p.ProductName.Replace("<h2>", "");
                p.ProductName = p.ProductName.Replace("</h2>", "");
            }
            if (p.ProductName.Contains("<h1>"))
            {
                p.ProductName = p.ProductName.Replace("<h1>", "");
                p.ProductName = p.ProductName.Replace("</h1>", "");
            }
            if (p.ProductName.Contains("\""))
            {
                p.ProductName = p.ProductName.Replace("\"", "");
            }
        }
    }
}
