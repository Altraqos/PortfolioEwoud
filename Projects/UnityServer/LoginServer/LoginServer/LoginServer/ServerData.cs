using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer
{
    class ServerData
    {
        public static string ServerIP;
        public static string WorldServerPort;
        public static string LoginServerPort;
        public static string LoginTCPServerPort;

        public static string ServerWorldPathString;



        public static void ReadWorldServerDataFiles()
        {
            ServerWorldPathString = StartServerExectutables.ServerDataFolder + @"\ServerWorldData.txt";

            if (File.Exists(ServerWorldPathString))
            {
                string[] ServerWorldDataTextHolder = File.ReadAllLines(ServerWorldPathString);
                string[] ServerWorldDataText = new string[19];

                int CurrentLineToAdd = 0;
                for (int i = 0; i < ServerWorldDataTextHolder.Length; i++)
                {

                    if (ServerWorldDataTextHolder[i].Contains("#"))
                    {
                        ServerWorldDataText[CurrentLineToAdd] = ServerWorldDataTextHolder[i].Replace("#", null);
                        CurrentLineToAdd++;
                        Thread.Sleep(2);
                    }
                }

                ServerIP = ServerWorldDataText[15];
                LoginServerPort = ServerWorldDataText[16];
                WorldServerPort = ServerWorldDataText[17];
                LoginTCPServerPort = ServerWorldDataText[18];
            }
        }
    }
}
