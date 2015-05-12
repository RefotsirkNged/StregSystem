using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace StregSystem___EksamensOpgave
{
    interface IStregsystemUI
    {
        void DisplayUserNotFound(string user);
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
        //stregsystemCommandLineInterface, i denne class sker alt output til brugeren
        private StregSystem _stregSystem;
        string path = Directory.GetCurrentDirectory() + @"\products.csv";
        public StregSystemCLI(StregSystem stregSystem)
        {
            _stregSystem = stregSystem;
            
        }
        //starter programmet, indeholder det while loop hele programmet ligger og kører rundt i.  allerførst bliver produktlisten loaded, og så er der ellers klar til at blive startet for systemet
        public void Start(StregSystemCommandParser parser)
        {
            ClearAndDrawTop();
            Console.WriteLine("Loading Product List!");
            _stregSystem.ProductList = _stregSystem.LoadProductData(path);
            Console.BufferHeight = _stregSystem.ProductList.Count + 2;
            //System.Threading.Thread.Sleep(500);

            while(true)
            {
                Console.Clear();
                DisplayProductList();
                string command = Console.ReadLine();
                try
                {
                    parser.ParseCommand(command);
                }
                catch (InsufficientCreditsException)
                {
                    DisplayInsufficientCash();
                }
                catch (NoProductsFoundException)
                {
                    ClearAndDrawTop();
                    Console.WriteLine("No products were found!");
                    Console.ReadLine();
                }
                catch(Exception e)
                {
                    ClearAndDrawTop();
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                
            }
        }

        public void DisplayAreYouSureScreen()
        {
            ClearAndDrawTop();
            Console.WriteLine("Are you sure?(y/n)");
            string decision = Console.ReadLine();
            if (decision == "n")
            {
                throw new ArgumentException("Okay! Going back to main menu...");
            }
            else if (decision == "y")
            {
                return;
            }
        }

        public void DisplayAddUserScreen()
        {
            string[] userInfo = new string[6];
            Regex rxUserName = new Regex(@"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-z0-9._]+(?<![_.])$");
            Regex rxEmailAddress = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            ClearAndDrawTop();
            Console.WriteLine("You are adding a new user!\n Start by entering username:\n");
            userInfo[0] = Console.ReadLine();
            while (!rxUserName.IsMatch(userInfo[0]))
            {
                Console.WriteLine("That is not a valid username! You must have both numbers and lower-case letters.");
                userInfo[0] = Console.ReadLine();
            }
            Console.WriteLine("First name:");
            userInfo[1] = Console.ReadLine();
            while (userInfo[1] == "" || userInfo[1] == null)
            {
                Console.WriteLine("That is not a valid first name!");
                userInfo[1] = Console.ReadLine();
            }
            Console.WriteLine("Last name:");
            userInfo[2] = Console.ReadLine();
            while (userInfo[2] == "" || userInfo[2] == null)
            {
                Console.WriteLine("That is not a valid last name!");
                userInfo[2] = Console.ReadLine();
            }
            Console.WriteLine("Email-address:");
            userInfo[3] = Console.ReadLine();
            while (!rxEmailAddress.IsMatch(userInfo[3]))
            {
                Console.WriteLine("That is not a valid Email!");
                userInfo[3] = Console.ReadLine();
            }

            User u = new User(userInfo[1], userInfo[2], userInfo[0], userInfo[3]);
            _stregSystem.UserList.Add(u);
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
                if (item.Active)
                {
                    Console.Write(item.ProductID + "\t");
                    Console.Write(item.ProductName + "\t");
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
            Console.WriteLine("User " + user +  " wasn't found!");
            Console.ReadLine();
        }

        public void DisplayProductNotFound()
        {
            Console.Clear();
            Console.WriteLine("That product either dosent exist, or isnt active!");
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
            System.Threading.Thread.Sleep(500);
            Environment.Exit(1);
        }

        public void DisplayInsufficientCash()
        {
            ClearAndDrawTop();
            Console.WriteLine("You dont have enough money!");
            Console.ReadLine();
        }

        public void DisplayGeneralError(string errorString)
        {
            ClearAndDrawTop();
            Console.WriteLine("Stuff is broken:" + errorString);
            Console.ReadLine();
        }
    }
}
