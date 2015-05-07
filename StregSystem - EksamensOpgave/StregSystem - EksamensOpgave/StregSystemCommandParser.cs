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
        public Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>
        {{":activate", }
            ;
        public StregSystemCommandParser(StregSystemCLI cli, StregSystem stregsystem)
        {
            _stregSystem = stregsystem;
            _CLI = cli;
        }
        //DICTIONARY SHIT FUCK NIGGER I HATE IT
        public void ParseCommand(string command)
        {
            if (command[0] == ':')
            {
                if (command.Contains("quit") || command == ":q")
                {
                    _CLI.Close();
                }
                else
                {
                    string[] commandSplit = command.Split(' ');
                    
                    if (commandSplit[0].Contains(":activate") || commandSplit[0].Contains(":deactivate"))
                    {
                        
                        toggleProductActive(commandSplit);
                        
                    }
                    else if (commandSplit[0].Contains("crediton") || commandSplit[0].Contains("creditoff"))
                    {
                        toggleProductCredit(commandSplit);
                    }
                    else if (commandSplit[0].Contains("addcredits"))
                    {
                        insertCashTransactionHelper(commandSplit);

                    }
                    else
                    {
                        _CLI.DisplayAdminCommandsNotFoundMessage();
                    }
                }
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
