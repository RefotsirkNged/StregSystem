using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            StregSystem stregsystem = new StregSystem();
            StregSystemCLI cli = new StregSystemCLI(stregsystem);
            StregSystemCommandParser parser = new StregSystemCommandParser(cli, stregsystem);
            cli.Start(parser);
        }
    }
}
