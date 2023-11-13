using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Http通讯
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Server server = new Server();
            server.ListenAsync();
            Console.ReadLine();
        }
    }
}
