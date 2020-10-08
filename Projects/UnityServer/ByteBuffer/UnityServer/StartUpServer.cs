using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityServer
{
    class StartUpServer
    {
        public static void StartServer()
        {
            ReadServerDataFiles.OnserverStartUp();
            ServerHandlePackets.InitializePackets();
            TCPServer server = new TCPServer();
            server.InitNetwork();
        }
    }
}
