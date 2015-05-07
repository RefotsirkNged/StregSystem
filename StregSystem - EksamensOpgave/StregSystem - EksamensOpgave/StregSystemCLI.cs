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
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n\n\n");
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

                    //fang no elements in list ting fra addcredits i parseren
                    Console.Clear();
                    Console.WriteLine("Stuff threw somthin");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.Data);
                    Console.ReadLine();
                }
                
            }
        }

        public void CreditsAdded(User user, decimal newBalance)
        {
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            Console.WriteLine(newBalance + " kr added to user " + user.UserName + "'s balance.");
            Console.WriteLine("New balance: " + user.Balance);
            Console.ReadLine();
        }
        public void DisplayProductList()
        {
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");

            foreach (Product item in _stregSystem.ProductList)
            {
                if (item.Active == true)
                {
                    Console.Write(item.ProductID + "  ");
                    Console.Write(item.ProductName + "  ");
                    Console.CursorLeft = Console.BufferWidth - 30;
                    Console.Write("Pris: " + item.Price + "kr.\n");
                }
            }
            Console.Write("\n");
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

        public void DisplayUserInfo(User user)
        {
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Username: " + user.UserName);
            Console.WriteLine("Firstname: " + user.FirstName);
            Console.WriteLine("Lastname: " + user.LastName);
            Console.WriteLine("Email-Address: " + user.EmailAdrress);
            Console.WriteLine("UserID: " + user.UserID.ToString());
            Console.WriteLine("User balance: " + user.Balance.ToString());
            Console.ReadLine();
        }


        public void DisplayTooManyArgumentsError()
        {
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            Console.Clear();
            Console.WriteLine("Stuff had too many arguments");
            Console.ReadLine();
        }

        public void DisplayAdminCommandsNotFoundMessage()
        {
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            Console.Clear();
            Console.WriteLine("That Command Wasnt found!");
            Console.ReadLine();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            throw new NotImplementedException();
        }

        public void Close()
        {
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
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
            Console.Clear();
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Stuff was broke:" + errorString);
            Console.ReadLine();
        }
    }
}
