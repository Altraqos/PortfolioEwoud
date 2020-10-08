using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LoginServer
{
    class StartServerExectutables
    {
        public static string ApplicationPath;
        public static string UserDataFolder;
        public static string GuildDataFolder;
        public static string ServerDataFolder;


        public static void InitializeServerFiles()
        {
            ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            UserDataFolder = ApplicationPath + @"UserData";
            ServerDataFolder = ApplicationPath + @"ServerData";
            GuildDataFolder = ApplicationPath + @"GuildData";

            if (!Directory.Exists(UserDataFolder))
            {

                DebugConsole.SlowlyWriteError("Couldn't find a 'UserData' folder at " + ApplicationPath);
                DebugConsole.CleanLine();
            }

            if (!Directory.Exists(GuildDataFolder))
            {
                DebugConsole.SlowlyWriteError("Couldn't find a 'GuildData' folder at " + ApplicationPath);
                DebugConsole.CleanLine();
            }

            if (!Directory.Exists(ServerDataFolder))
            {
                DebugConsole.SlowlyWriteError("Couldn't find a 'ServerData' folder at " + ApplicationPath);
                DebugConsole.CleanLine();
            }

            else
            {
                ServerData.ReadWorldServerDataFiles();
            }
        }
    }
}
