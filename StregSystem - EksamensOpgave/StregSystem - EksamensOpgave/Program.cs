using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StregSystem___EksamensOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            StregSystem stregsystem = new StregSystem();
            StregSystemCLI cli = new StregSystemCLI(stregsystem);
            StregSystemCommandParser parser = new StregSystemCommandParser(cli, stregsystem);
            User u = new User("Kristoffer", "Degn", "krist373", "kristoffer@hylleby.dk");
            u.Balance = 2000;
            User u1 = new User("", "Leding", "ledning373", "ledning@aalborg.dk");
            u.Balance = 1;
            stregsystem.UserList.Add(u);
            stregsystem.UserList.Add(u1);
            cli.Start(parser);
        }
    }
}
