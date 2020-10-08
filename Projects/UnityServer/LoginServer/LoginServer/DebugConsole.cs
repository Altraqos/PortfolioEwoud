using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer
{
    class DebugConsole
    {
        public static int TextSpeed = 1;


        public static void OnstartServerDebug()
        {
            if (!Directory.Exists(Program.UserDataFolder))
            {
                try
                {
                    Directory.CreateDirectory(Program.UserDataFolder);
                    SlowlyWriteError("Couldn't find a 'UserData' folder at " + Program.ApplicationPath + ", created the folder.");
                    CleanLine();
                }
                catch
                {
                    SlowlyWriteError("Couldn't create a 'UserData' folder at " + Program.ApplicationPath + ", please try again.");
                    CleanLine();
                }
            }

            if (!Directory.Exists(Program.GuildDataFolder))
            {
                try
                {
                    Directory.CreateDirectory(Program.GuildDataFolder);
                    SlowlyWriteError("Couldn't find a 'GuildData' folder at " + Program.ApplicationPath + ", created the folder.");
                    CleanLine();
                }
                catch
                {
                    SlowlyWriteError("Couldn't create a 'GuildData' folder at " + Program.ApplicationPath + ", please try again.");
                    CleanLine();
                }
            }

            if (!Directory.Exists(Program.ServerDataFolder))
            {
                try
                {
                    Directory.CreateDirectory(Program.ServerDataFolder);
                    SlowlyWriteError("Couldn't find a 'ServerData' folder at " + Program.ApplicationPath + ", created the folder.");
                    CleanLine();
                }
                catch
                {
                    SlowlyWriteError("Couldn't create a 'ServerData' folder at " + Program.ApplicationPath + ", please try again.");
                    CleanLine();
                }
            }

            if (!Directory.Exists(Program.CharacterPathString))
            {
                try
                {
                    Directory.CreateDirectory(Program.CharacterPathString);
                    SlowlyWriteError("Couldn't find a 'CharacterData' folder at " + Program.ApplicationPath + ", created the folder.");
                    CleanLine();
                }
                catch
                {
                    SlowlyWriteError("Couldn't create a 'CharacterData' folder at " + Program.ApplicationPath + ", please try again.");
                    CleanLine();
                }
            }

            if (!File.Exists(Program.ServerPathString))
            {
                InitializeServerData.CreateServerDataFiles();
                SlowlyWriteError("Couldn't find a 'ServerData' file in " + Program.ApplicationPath + ", created the file.");
                CleanLine();
            }

            if (!File.Exists(Program.ServerWorldPathString))
            {
                InitializeServerData.CreateServerWorldDataFiles();
                SlowlyWriteError("Couldn't find a 'ServerWorldData' file in " + Program.ApplicationPath + ", created the file.");
                CleanLine();
            }
        }

        public static void DebugLog()
        {
            if (Program.ActivePlayers > 0)
            {
                for (int i = 0; i <= Program.ActivePlayers; i++)
                {
                    SlowlyWrite(Program.PlayerNames[i] + i);
                }
                CleanLine();
            }

            if (Program.LastAssignedName != "")
            {
                string[] UserDataFiles = new string[InitializeServerData.maxPlayerOnline];
                int n = 0;
                SlowlyWrite("searching for player data files");
                CleanLine();

                foreach (string s in Directory.GetFiles(ServerDataClass.UserDataFolder, "*.txt").Select(Path.GetFileName))
                {
                    UserDataFiles[n] = s.Replace(" - UserData.txt", null);
                    SlowlyWriteVarData("Found data for user: '" + UserDataFiles[n] + "'");
                    n++;
                    Thread.Sleep(TextSpeed);
                }
                CleanLine();
            }

            if (Program.LastAssignedGuildName != "")
            {
                string[] GuildDataFiles = new string[InitializeServerData.maxPlayerOnline];
                int n = 0;
                SlowlyWrite("searching for guild data files");
                CleanLine();

                foreach (string s in Directory.GetFiles(ServerDataClass.GuildDataFolder, "*.txt").Select(Path.GetFileName))
                {
                    GuildDataFiles[n] = s.Replace(" - GuildData.txt", null);
                    SlowlyWriteVarData("Found data for guild: '" + GuildDataFiles[n] + "'");
                    n++;
                    Thread.Sleep(TextSpeed);
                }
                CleanLine();
            }
        }

        public static void ShowActivePlayers()
        {          
            int ActivePlayers = Program.ActivePlayers;
            if (ActivePlayers != 0)
            {
                if (ActivePlayers == 1)
                {
                    SlowlyWrite("Now " + (ActivePlayers + 1) + " player online.");
                    CleanLine();
                }
                else
                {
                    SlowlyWrite("Now " + (ActivePlayers + 1) + " players online.");
                    CleanLine();
                }
                ArrangePlayerNamesInArray();
            }
        }

        public static void ArrangePlayerNamesInArray()
        {
            try
            {
                int ActivePlayers = Program.ActivePlayers;

                Program.PlayerNames[Program.ActivePlayers] = Program.LastLoginName;
                for (int i = 0; i <= Program.ActivePlayers; i++)
                {
                    string ArrangePlayerNamePathString = Path.Combine(Program.UserDataFolder, (Program.PlayerNames[i] + " - UserData.txt"));
                    string[] PlayerDataText = File.ReadAllLines(ArrangePlayerNamePathString);
                    //PlayerDataText[6] = i.ToString();
                    SlowlyWriteVarData("User: '" + Program.PlayerNames[i] + "' | ID: '" + PlayerDataText[6] + "'");
                    File.WriteAllLines(ArrangePlayerNamePathString, PlayerDataText);
                }
                CleanLine();
            }
            catch
            {
                return;
            }
        }

        public static void ShowServerWorldStats()
        {
            SlowlyWrite("server Stats");
            CleanLine();
            SlowlyWriteVarData("current EXP Rate: '" + InitializeServerData.expRate + "'");
            SlowlyWriteVarData("Current Guild Bonus EXP: '" + InitializeServerData.guildExpBonus + "'");
            SlowlyWriteVarData("Current Bonus EXP: '" + InitializeServerData.BonusExp + "'");
            SlowlyWriteVarData("Maximum Players Online: '" + InitializeServerData.maxPlayerOnline + "'");
            SlowlyWriteVarData("Current Max Player Level: '" + InitializeServerData.maxPlayerLevel + "'");
            SlowlyWriteVarData("Current Player Minimum Name Length: '" + InitializeServerData.minNameLength + "'");
            SlowlyWriteVarData("Current Player Maximum Name Length: '" + InitializeServerData.maxNameLength + "'");
            SlowlyWriteVarData("current Player Maximum Money: '" + InitializeServerData.maxMoney + "'");
            SlowlyWriteVarData("Current Maximun Group Size: '" + InitializeServerData.maxGroupSize + "'");
            SlowlyWriteVarData("Current Maximum Players Per User: '" + InitializeServerData.maxPlayersPerAccount + "'");
            SlowlyWriteVarData("Current Maxumum Heroic Players Per User: '" + InitializeServerData.maxHeroicPlayersPerAccount + "'");
            SlowlyWriteVarData("Current Minimum Players To Create Guilds: '" + InitializeServerData.minPlayersToCreateGuild + "'");
            SlowlyWriteVarData("Server Name: '" + InitializeServerData.ServerName + "'");
            SlowlyWriteVarData("Server Message Of The Day: '" + InitializeServerData.ServerMotD + "'");
            SlowlyWriteVarData("login Memo: '" + InitializeServerData.LoginMemo + "'");
            SlowlyWriteVarData("Server IP adress: '" + InitializeServerData.Server_Ip + "'");
            SlowlyWriteVarData("Login server port: '" + InitializeServerData.LoginServerPort + "'");
            SlowlyWriteVarData("world server port: '" + InitializeServerData.WorldServerPort + "'");
            SlowlyWriteVarData("TCP server port: '" + InitializeServerData.LoginTCPServerPort + "'");

            DebugConsole.SlowlyWriteVarData("this server will use ip: '" + InitializeServerData.Server_Ip + ":" + InitializeServerData.LoginTCPServerPort + "'");

            CleanLine();
        }

        public static void SlowlyWrite(string SlowlyType)
        {
            string SlowlyTypeHolder = UppercaseFirst(SlowlyType);

            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];

            foreach (char c in SlowlyTypeHolder)
            {
                Console.Write(c);
                Thread.Sleep(TextSpeed);
            }
            if (last.ToString() != "." && last.ToString() != "!" && last.ToString() != "?")
            {
                Console.Write(".");
            }
            Console.WriteLine();
        }

        public static void SlowlyWriteVarData(string SlowlyType)
        {
            string SlowlyTypeHolder = UppercaseFirst(SlowlyType);
            int varMark = 0;
            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];

            foreach (char c in SlowlyTypeHolder)
            {
                if ("'" == c.ToString())
                {
                    varMark += 1;

                    if (varMark == 2)
                    {
                        varMark = 0;
                    }
                }
                if (varMark > 0)
                {
                    if ("'" != c.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(c);
                        Thread.Sleep(TextSpeed);
                    }
                }

                if (varMark == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    if ("'" != c.ToString())
                    {
                        Console.Write(c);
                        Thread.Sleep(TextSpeed);
                    }
                }
            }

            if (last.ToString() != "." && last.ToString() != "!" && last.ToString() != "?")
            {
                Console.Write(".");
            }
            Console.WriteLine();
        }

        public static void SlowlyWriteErrorVarData(string SlowlyType)
        {
            string SlowlyTypeHolder = UppercaseFirst(SlowlyType);
            int varMark = 0;
            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];

            foreach (char c in SlowlyTypeHolder)
            {
                if ("'" == c.ToString())
                {
                    varMark += 1;

                    if (varMark == 2)
                    {
                        varMark = 0;
                    }
                }
                if (varMark > 0)
                {
                    if ("'" != c.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(c);
                        Thread.Sleep(TextSpeed);
                    }
                }

                if (varMark == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    if ("'" != c.ToString())
                    {
                        Console.Write(c);
                        Thread.Sleep(TextSpeed);
                    }
                }
            }

            if (last.ToString() != "." && last.ToString() != "!" && last.ToString() != "?")
            {
                Console.Write(".");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void SlowlyWriteError(string SlowlyType)
        {
            string SlowlyTypeHolder = UppercaseFirst(SlowlyType);

            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char c in SlowlyTypeHolder)
            {
                Console.Write(c);
                Thread.Sleep(TextSpeed);
            }
            if (last.ToString() != "." && last.ToString() != "!" && last.ToString() != "?")
            {
                Console.Write(".");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void CleanLine()
        {
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < 17; i++)
            {
                Console.Write("-");
                Thread.Sleep(TextSpeed);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
