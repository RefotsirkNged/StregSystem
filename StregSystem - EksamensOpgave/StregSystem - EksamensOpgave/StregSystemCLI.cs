using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StregSystem___EksamensOpgave
{
    interface IStregsystemUI
    {
        void DisplayUserNotFound(string user
            );
        void DisplayProductNotFound();
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError();
        void DisplayAdminCommandsNotFoundMessage();
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash();
        void DisplayGeneralError(string errorString);
    }
    class StregSystemCLI : IStregsystemUI
    {
        private StregSystem _stregSystem;
        string path = Directory.GetCurrentDirectory() + @"\products.csv";
        public StregSystemCLI(StregSystem stregSystem)
        {
            _stregSystem = stregSystem;
            
        }
        public void Start(StregSystemCommandParser parser)
        {
            ClearAndDrawTop();
            Console.WriteLine("Loading Product List!");
            _stregSystem.ProductList = _stregSystem.LoadProductData(path);
            Console.BufferHeight = _stregSystem.ProductList.Count + 2;
            System.Threading.Thread.Sleep(500);
            Console.Clear();

            while(true)
            {
                Console.Clear();
                DisplayProductList();
                string command = Console.ReadLine();
                try
                {
                    parser.ParseCommand(command);
                }
                catch(Exception e)
                {
                    //catch no element from the admincommands lambda
                    //fang no elements in list ting fra addcredits i parseren
                    ClearAndDrawTop();
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                
            }
        }

        public void ThatWasNotANumber()
        {
            ClearAndDrawTop();
            Console.WriteLine("That was not a number! \nCorrect format is: Username ProductID");
            Console.ReadLine();
        }
        public void ThatWasNotAPositiveNumberMultiBuy()
        {
            ClearAndDrawTop();
            Console.WriteLine("That was not a positive number! The number in amount has to be a positive format! \nCorrect format is: Username Amount ProductID");
            Console.ReadLine();
        }
        public void ThatWasNotANumberMultiBuy()
        {
            ClearAndDrawTop(); ;
            Console.WriteLine("That was not a number in either amount or product ID! \nCorrect format is: Username Amount ProductID");
            Console.ReadLine();
        }

        public void CreditsAdded(User user, decimal newBalance)
        {
            ClearAndDrawTop();
            Console.WriteLine(newBalance + " kr added to user " + user.UserName + "'s balance.");
            Console.WriteLine("New balance: " + user.Balance);
            Console.ReadLine();
        }
        public void DisplayProductList()
        {
            ClearAndDrawTop();

            foreach (Product item in _stregSystem.ProductList)
            {
                if (item.Active == true)
                {
                    Console.Write(item.ProductID + "  ");
                    Console.Write(item.ProductName + "  ");
                    Console.CursorLeft = Console.BufferWidth - 30;
                    Console.Write("Price: " + item.Price + "kr.\n");
                }
            }
            Console.WriteLine("\n");
            Console.Write("quickBuy: ");
        }
        public void DisplayUserNotFound(string user)
        {
            Console.Clear();
            Console.WriteLine("User " + user +  " wasnt found!");
            Console.ReadLine();
        }

        public void DisplayProductNotFound()
        {
            Console.Clear();
            Console.WriteLine("Stuff (product) wasnt found");
            Console.ReadLine();
        }

        public void ClearAndDrawTop()
        {
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
        }

        public void DisplayUserInfo(User user)
        {
            ClearAndDrawTop();
            Console.WriteLine("Username: " + user.UserName);
            Console.WriteLine("Firstname: " + user.FirstName);
            Console.WriteLine("Lastname: " + user.LastName);
            Console.WriteLine("Email-Address: " + user.EmailAdrress);
            Console.WriteLine("UserID: " + user.UserID.ToString());
            Console.WriteLine("User balance: " + user.Balance.ToString());
            var usertransactions = _stregSystem.TransactionList.Where(ut => ut.TransactionUser.UserID == user.UserID);
            usertransactions.OrderBy(trans => trans.TransactionDate);
            int TransactionsShown = 0;
            foreach (var item in usertransactions)
            {
                Console.WriteLine(item.TransactionID.ToString() + item.TransactionUser + item.TransactionDate + item.TransactionAmount);
                ++TransactionsShown;
                if(TransactionsShown <= 10)
                {
                    break;
                }
            }
            if(user.Balance < 50)
            {
                Console.WriteLine("You have less than 50kr left on your account. Please add funds!");
            }
            Console.ReadLine();
        }


        public void DisplayTooManyArgumentsError()
        {
            ClearAndDrawTop();
            Console.Clear();
            Console.WriteLine("That command had too many arguments");
            Console.ReadLine();
        }

        public void DisplayAdminCommandsNotFoundMessage()
        {
            ClearAndDrawTop();
            Console.Clear();
            Console.WriteLine("That admin command wasnt found!");
            Console.ReadLine();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            ClearAndDrawTop();
            Console.WriteLine(transaction.TransactionUser.FirstName + " " + transaction.TransactionUser.LastName + " bought a " + transaction.ProductBeingBought.ProductName + ". \nNew user balance: " + transaction.TransactionUser.Balance);
            Console.ReadLine();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            ClearAndDrawTop();
            Console.WriteLine(transaction.TransactionUser.FirstName + " " + transaction.TransactionUser.LastName + " bought " + count + " " + transaction.ProductBeingBought.ProductName + ". \nNew user balance: " + transaction.TransactionUser.Balance);
            Console.ReadLine();
        }

        public void Close()
        {
            ClearAndDrawTop();
            Console.WriteLine("Thank you for using this program. CYA!");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(1);
        }

        public void DisplayInsufficientCash()
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            ClearAndDrawTop();
            Console.WriteLine("Stuff was broke:" + errorString);
            Console.ReadLine();
        }
    }
}
