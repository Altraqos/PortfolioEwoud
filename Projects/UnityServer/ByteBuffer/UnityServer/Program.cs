using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnityServer
{
    class Program
    {
        public static int GameServerPort = 3050;
        public static string ServerIP = "127.0.0.1";
        public static int currentPlayersOnline;

        static void Main(string[] args)
        {
            StartUpServer.StartServer();
        }
    }
}