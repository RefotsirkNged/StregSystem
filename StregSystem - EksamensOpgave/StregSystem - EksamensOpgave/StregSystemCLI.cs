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
        void DisplayUserNotFound();
        void DisplayProductNotFound();
        void DisplayUserInfo();
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
            //Skriv en loading skærm?
            Console.WriteLine("-----------------------StregSystem Eks Opgave-----------------------");
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Loading Product List!");
            _stregSystem.ProductList = _stregSystem.LoadProductData(path);
            system
        }
        public void DisplayUserNotFound()
        {
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound()
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo()
        {
            throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError()
        {
            throw new NotImplementedException();
        }

        public void DisplayAdminCommandsNotFoundMessage()
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash()
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }
    }
}
