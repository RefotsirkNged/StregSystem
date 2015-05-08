using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class StregSystemCommandParser
    {
        StregSystem _stregSystem;
        StregSystemCLI _CLI;
        private Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();

        public StregSystemCommandParser(StregSystemCLI cli, StregSystem stregsystem)
        {
            _stregSystem = stregsystem;
            _CLI = cli;
            //all the admin commands
            _adminCommands.Add(":activate", toggleProductActive);
            _adminCommands.Add(":deactivate", toggleProductActive);
            _adminCommands.Add(":creditson", toggleProductCredit);
            _adminCommands.Add(":creditsoff", toggleProductCredit);
            _adminCommands.Add(":addcredits", insertCashTransactionHelper);
            _adminCommands.Add(":quit", quitHelper);
            _adminCommands.Add(":q", quitHelper);

            //all the user commands


        }
        public void ParseCommand(string command)
        {
            string[] commandSplit = command.Split(' ');
            if (commandSplit[0].StartsWith(":"))
            {
                var dictionaryValue = _adminCommands.FirstOrDefault(dv => dv.Key.Contains(commandSplit[0]));
                if (dictionaryValue.Value != null)
                {
                    _adminCommands[dictionaryValue.Key].Invoke(commandSplit);
                }
                else
                    _CLI.DisplayAdminCommandsNotFoundMessage();

            }
            else
            {
                if ((_stregSystem.UserList.FirstOrDefault(ul => ul.UserName == commandSplit[0]) != null))
                {
                    User u = (_stregSystem.UserList.FirstOrDefault(ul => ul.UserName == commandSplit[0]));
                    if(commandSplit.Count() == 1) 
                    {
                        _CLI.DisplayUserInfo(u);
                    }
                    if (commandSplit.Count() == 2)
                    {
                        int n;
                        if (!(int.TryParse(commandSplit[1], out n)))
                        {
                            _CLI.ThatWasNotANumber();
                        }
                        Product p = _stregSystem.ProductList.FirstOrDefault(pl => pl.ProductID.ToString() == commandSplit[1]);
                        if (p != null)
                        {
                            BuyTransaction transaction = new BuyTransaction(u, p);
                            transaction.Execute();
                            _stregSystem.TransactionList.Add(transaction);
                            _CLI.DisplayUserBuysProduct(transaction);
                        }
                        else
                        {
                            _CLI.DisplayProductNotFound();
                        }
                    }
                    if (commandSplit.Count() == 3)
                    {
                        
                        int n;
                        if(!(int.TryParse(commandSplit[1], out n)) || !(int.TryParse(commandSplit[2], out n)))
                        { 
                            _CLI.ThatWasNotANumberMultiBuy();
                        }
                        int.TryParse(commandSplit[2], out n);
                        if (!(n >= 0))
                        {
                            _CLI.ThatWasNotAPositiveNumberMultiBuy();
                        }
                        Product p = _stregSystem.ProductList.FirstOrDefault(pl => pl.ProductID.ToString() == commandSplit[1]);
                        if (p != null)
                        {
                            if (!((u.Balance - p.Price * n) < 0))
                            {
                                BuyTransaction transaction = new BuyTransaction(u, p);
                                for (int i = 0; i < n; i++)
                                {
                                   
                                    transaction.Execute();
                                    _stregSystem.TransactionList.Add(transaction);
                                }
                                _CLI.DisplayUserBuysProduct(n, transaction);
                            }
                        }
                        
                    }
                   
                }
                else
                    _CLI.DisplayUserNotFound(commandSplit[0]);
            }
            
        }

        //notimplemented
        public void userNameOnlyHelper()
        {

        }




        public void quitHelper(string[] commandSplit)
        {
            if (commandSplit[0].Contains("quit") || commandSplit[0] == ":q")
            {
                _CLI.Close();
            }
        }
        public void insertCashTransactionHelper(string[] commandSplit)
        {
            if (commandSplit[1] == null)
            {
                _CLI.DisplayUserNotFound(commandSplit[1]);
            }
            else if (commandSplit[2] == null)
            {
                _CLI.DisplayGeneralError("Der blev ikke indtastet nogen cash amount under admin kommandoen addcredits");
            }
            else
            {            
                User userFound;
                userFound = _stregSystem.UserList.FirstOrDefault(ul => ul.UserName == commandSplit[1]);
                int index = _stregSystem.UserList.FindIndex(ul => ul.UserName == commandSplit[1]);
                if (userFound == null)
                {
                    _CLI.DisplayUserNotFound(commandSplit[1]);
                }
                else
                {
                    decimal amount;
                    Decimal.TryParse(commandSplit[2], out amount);
                    InsertCashTransaction ict = new InsertCashTransaction(userFound, amount);
                    ict.Execute();
                    _stregSystem.UserList[index] = userFound;
                    _CLI.CreditsAdded(userFound, amount);
                }
                
            }
        }
        public void toggleProductCredit(string[] commandSplit)
        {
            if (commandSplit[1] == null)
            {
                _CLI.DisplayProductNotFound();
            }
            else if (commandSplit.Count() != 2)
            {
                _CLI.DisplayTooManyArgumentsError();
            }
            else
            {
                Product productFound;
                productFound = _stregSystem.ProductList.First(pl => pl.ProductID.ToString() == commandSplit[1].ToString());
                int index = _stregSystem.ProductList.FindIndex(pl => pl.ProductID.ToString() == commandSplit[1].ToString());
                if (productFound == null)
                {
                    _CLI.DisplayProductNotFound();
                }
                else
                {
                    //defaults to false
                    string tf = commandSplit[0];
                    bool tfParsed = false;
                    if (tf.Contains(":crediton"))
                    {
                        tfParsed = true;
                    }
                    else if (tf.Contains(":creditoff"))
                    {
                        tfParsed = false;
                    }
                    productFound.CanBeBoughtOnCredit = tfParsed;
                    _stregSystem.ProductList[index] = productFound;
                }
            }


        }
        public void toggleProductActive(string[] commandSplit)
        {

            //checking if the input is correct
            if (commandSplit[1] == null)
            {
                
                _CLI.DisplayProductNotFound();
            }
            else if (commandSplit.Count() != 2)
            {
                _CLI.DisplayTooManyArgumentsError();
            }
            //input is correct, lets do something with it
            else
            {
                
                Product productFound;
                productFound = _stregSystem.ProductList.First(pl => pl.ProductID.ToString() == commandSplit[1].ToString());
                int index = _stregSystem.ProductList.FindIndex(pl => pl.ProductID.ToString() == commandSplit[1].ToString());
                if (productFound == null)
                {
                    _CLI.DisplayProductNotFound();
                }
                else
                {
                    //defaults to false
                    string tf = commandSplit[0];
                    bool tfParsed = false;
                    if (tf.Contains(":activate"))
                    {
                        tfParsed = true;
                    }
                    else if (tf.Contains(":deactivate"))
                    {
                        tfParsed = false;
                    }
                    productFound.Active = tfParsed;
                    _stregSystem.ProductList[index] = productFound;
                }
            }
            
        }
    }
}
