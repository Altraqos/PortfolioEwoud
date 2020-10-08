using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginTCPServer
{
    class InitializeServerData
    {
        public static int expRate = 1;
        public static int guildExpBonus = 2;
        public static int BonusExp = 1;
        public static int maxPlayerLevel = 100;
        public static int maxPlayerOnline = 25000;
        public static int minNameLength = 1;
        public static int maxNameLength = 12;
        public static int maxMoney = 1250000;
        public static int maxGroupSize = 30;
        public static int maxPlayersPerAccount = 20;
        public static int maxHeroicPlayersPerAccount = 2;
        public static int minPlayersToCreateGuild = 5;
        public static string ServerName = "Server";
        public static string ServerMotD = "This is the server messsage of the day";
        public static string LoginMemo = "This is a message you will see when you login on the server";

        public static string expRateString;
        public static string guildExpBonusString;
        public static string BonusExpString;
        public static string maxPlayersOnlineString;
        public static string maxPlayerLevelString;
        public static string minNameLengthString;
        public static string maxNameLengthString;
        public static string maxMoneyString;
        public static string maxGroupSizeString;
        public static string maxPlayersPerAccountString;
        public static string maxHeroicPlayersPerAccountString;
        public static string minPlayersToCreateGuildString;
        public static string ServerNameString;
        public static string ServerMotDString;
        public static string LoginMemoString;

        public static string Server_Ip;
        public static string WorldServerPort;
        public static string LoginServerPort;
        public static string LoginTCPServerPort;

        public static void CreateServerDataFiles()
        {
            if (!File.Exists(ServerDataClass.ServerPathString))
            {
                string ServerPathString = ServerDataClass.ServerPathString;
                ServerPathString = Path.Combine(ServerDataClass.ServerDataFolder, "ServerData.txt");
                FileStream fs = File.Open(ServerPathString, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);

                fw.WriteLine("***ServerDataFile***");
                fw.WriteLine();
                fw.WriteLine(Program.LastAssignedName);
                fw.WriteLine(Program.LastAssignedID);
                fw.WriteLine(Program.LastLoginName);
                fw.WriteLine(Program.LastAssignedGuildName);
                fw.WriteLine(Program.LastAssignedGuildID);
                fw.WriteLine("Online since: " + DateTime.Now.ToString("MMMM dd, HH:mm"));
                fw.Flush();
                fw.Close();
            }
        }

        public static void CreateServerWorldDataFiles()
        {
            if (!File.Exists(ServerDataClass.ServerWorldPathString))
            {
                string ServerWorldPathString = ServerDataClass.ServerWorldPathString;
                ServerWorldPathString = Path.Combine(ServerDataClass.ServerDataFolder, "ServerWorldData.txt");

                FileStream fs = File.Open(ServerWorldPathString, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);

                fw.WriteLine("***ServerWorldDataFile***");
                fw.WriteLine();
                fw.WriteLine("***Exp Rate Which Players Level By.***");
                fw.WriteLine("***Default Exp Rate = 1.***");
                fw.WriteLine("#" + expRate);
                fw.WriteLine();

                fw.WriteLine("***Guild Bonus Exp Rate Which Players Can Get.***");
                fw.WriteLine("***Default Guild Bonus Exp Rate = 2.***");
                fw.WriteLine("#" + guildExpBonus);
                fw.WriteLine();

                fw.WriteLine("***Bonus Exp Rate Which Players Can Get.***");
                fw.WriteLine("***Default Bonus Exp Rate = 1.***");
                fw.WriteLine("#" + BonusExp);
                fw.WriteLine();

                fw.WriteLine("***Max Level Players Can Reach.***");
                fw.WriteLine("***Default Max Player Level = 100.***");
                fw.WriteLine("#" + maxPlayerLevel);
                fw.WriteLine();

                fw.WriteLine("***Max Players That Can Be Online At Once.***");
                fw.WriteLine("***Default Max Players Online = 25000.***");
                fw.WriteLine("#" + maxPlayerOnline);
                fw.WriteLine();

                fw.WriteLine("***Minumum Amount A Player Name Can Be.***");
                fw.WriteLine("***Default Minimum Player Name = 1.***");
                fw.WriteLine("#" + minNameLength);
                fw.WriteLine();

                fw.WriteLine("***Max Amount A Player Name Can Be.***");
                fw.WriteLine("***Default Max Player Name = 12.***");
                fw.WriteLine("#" + maxNameLength);
                fw.WriteLine();

                fw.WriteLine("***Max Amount Of Money Players Can Reach.***");
                fw.WriteLine("***Default Max Player Money Cap = 125000.***");
                fw.WriteLine("#" + maxMoney);
                fw.WriteLine();

                fw.WriteLine("***Max Amount Of Players Per Group.***");
                fw.WriteLine("***Default Max Players Per Group = 5.***");
                fw.WriteLine("#" + maxGroupSize);
                fw.WriteLine();

                fw.WriteLine("***Max Amount Of Players Per User.***");
                fw.WriteLine("***Default Max Players Per User = 20.***");
                fw.WriteLine("#" + maxPlayersPerAccount);
                fw.WriteLine();

                fw.WriteLine("***Max Amount Of Heroic Players Per User.***");
                fw.WriteLine("***Default Max Heroic Players Per User = 2.***");
                fw.WriteLine("#" + maxHeroicPlayersPerAccount);
                fw.WriteLine();

                fw.WriteLine("***Minumum Amount Of Players To Create Guild.***");
                fw.WriteLine("***Default Minumum Players To Create Guild = 5.***");
                fw.WriteLine("#" + minPlayersToCreateGuild);
                fw.WriteLine();

                fw.WriteLine("***Server Name.***");
                fw.WriteLine("***Default Server Name = Server Name.***");
                fw.WriteLine("#" + ServerName);
                fw.WriteLine();

                fw.WriteLine("***Server MotD.***");
                fw.WriteLine("***Default Server Message Of The Day = Server Message Of The Day.***");
                fw.WriteLine("#" + ServerMotD);
                fw.WriteLine();

                fw.WriteLine("***Login Text.***");
                fw.WriteLine("***Default Login Text = Login Text.***");
                fw.WriteLine("#" + LoginMemo);
                fw.WriteLine();

                fw.WriteLine("***Server Ip.***");
                fw.WriteLine("***Default Server Ip Adress = '127.0.0.1'.***");
                fw.WriteLine("#" + "127.0.0.1");
                fw.WriteLine();

                fw.WriteLine("***Login Server Port.***");
                fw.WriteLine("***Default Login Server Port = '3085'.***");
                fw.WriteLine("#" + "3085");
                fw.WriteLine();

                fw.WriteLine("***World Server Port.***");
                fw.WriteLine("***Default World Server Port = '3050'.***");
                fw.WriteLine("#" + "3050");
                fw.WriteLine();

                fw.WriteLine("***Login TCP Server Port.***");
                fw.WriteLine("***Default Login TCP Server Port = '3045'.***");
                fw.WriteLine("#" + "3045");
                fw.WriteLine();


                fw.Flush();
                fw.Close();

            }
            else
            {
                return;
            }
        }

        public static void InitializeServerDataFiles()
        {
            Program.OnlineTime = DateTime.Now.ToString("MMMM dd, HH:mm");
            Console.ForegroundColor = ConsoleColor.Green;
            DebugConsole.SlowlyWrite("Found a 'UserData' folder at " + ServerDataClass.UserDataFolder);
            DebugConsole.SlowlyWrite("Found a 'GuildData' folder at " + ServerDataClass.GuildDataFolder);
            DebugConsole.SlowlyWrite("Found a 'ServerData' folder at " + ServerDataClass.ServerDataFolder);
            DebugConsole.SlowlyWrite("Found a 'ServerData' file at " + ServerDataClass.ServerPathString);
            DebugConsole.SlowlyWrite("Found a 'ServerWorldData' file at " + ServerDataClass.ServerWorldPathString);
            DebugConsole.SlowlyWrite("found a 'CharacterData' folder at " + ServerDataClass.CharacterPathString);
            Console.ForegroundColor = ConsoleColor.Gray;
            DebugConsole.CleanLine();

            ReadMainServerDataFiles();
            ReadWorldServerDataFiles();
        }

        public static void ReadMainServerDataFiles()
        {
            string[] ServerDataText = File.ReadAllLines(ServerDataClass.ServerPathString);
            Program.LastAssignedName = ServerDataText[2];
            Int32.TryParse(ServerDataText[3], out Program.LastAssignedID);
            Program.LastLoginName = ServerDataText[4];
            Program.LastAssignedGuildName = ServerDataText[5];
            Int32.TryParse(ServerDataText[6], out Program.LastAssignedGuildID);
            File.WriteAllLines(ServerDataClass.ServerPathString, ServerDataText);
            DebugConsole.SlowlyWriteVarData("Online since: '" + Program.OnlineTime + "'");

            if (Program.LastAssignedName != "")
            {
                DebugConsole.SlowlyWriteVarData("Last created account: '" + DebugConsole.UppercaseFirst(Program.LastAssignedName) + "'");
                DebugConsole.SlowlyWriteVarData("Last assigned player ID: '" + Program.LastAssignedID.ToString() + "'");
            }
            if (Program.LastLoginName != "")
            {
                DebugConsole.SlowlyWriteVarData("last player that logged in: '" + DebugConsole.UppercaseFirst(Program.LastLoginName) + "'");
            }

            if (Program.LastAssignedGuildName != "")
            {
                DebugConsole.SlowlyWriteVarData("last created guild name: '" + DebugConsole.UppercaseFirst(Program.LastAssignedGuildName) + "' | Guild ID : '" + Program.LastAssignedGuildID + "'");
            }

            DebugConsole.CleanLine();
        }

        public static void ReadWorldServerDataFiles()
        {
            if (File.Exists(ServerDataClass.ServerWorldPathString))
            {
                string[] ServerWorldDataTextHolder = File.ReadAllLines(ServerDataClass.ServerWorldPathString);
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

                Int32.TryParse(expRateString = ServerWorldDataText[0], out expRate);
                Int32.TryParse(guildExpBonusString = ServerWorldDataText[1], out guildExpBonus);
                Int32.TryParse(BonusExpString = ServerWorldDataText[2], out BonusExp);
                Int32.TryParse(maxPlayersOnlineString = ServerWorldDataText[3], out maxPlayerOnline);
                Int32.TryParse(maxPlayerLevelString = ServerWorldDataText[4], out maxPlayerLevel);
                Int32.TryParse(minNameLengthString = ServerWorldDataText[5], out minNameLength);
                Int32.TryParse(maxNameLengthString = ServerWorldDataText[6], out maxNameLength);
                Int32.TryParse(maxMoneyString = ServerWorldDataText[7], out maxMoney);
                Int32.TryParse(maxGroupSizeString = ServerWorldDataText[8], out maxGroupSize);
                Int32.TryParse(maxPlayersPerAccountString = ServerWorldDataText[9], out maxPlayersPerAccount);
                Int32.TryParse(maxHeroicPlayersPerAccountString = ServerWorldDataText[10], out maxHeroicPlayersPerAccount);
                Int32.TryParse(minPlayersToCreateGuildString = ServerWorldDataText[11], out minPlayersToCreateGuild);
                ServerName = ServerNameString = ServerWorldDataText[12];
                ServerMotD = ServerMotDString = ServerWorldDataText[13];
                LoginMemo = LoginMemoString = ServerWorldDataText[14];
                Server_Ip = ServerWorldDataText[15];
                LoginServerPort = ServerWorldDataText[16];
                WorldServerPort = ServerWorldDataText[17];
                LoginTCPServerPort = ServerWorldDataText[18];

                DebugConsole.ShowServerWorldStats();
            }
        }

        public static void UpdateServerDataFiles()
        {
            string[] ServerDataText = File.ReadAllLines(ServerDataClass.ServerPathString);
            ServerDataText[2] = Program.LastAssignedName;
            ServerDataText[3] = Program.LastAssignedID.ToString();
            ServerDataText[4] = Program.LastLoginName;
            ServerDataText[5] = Program.LastAssignedGuildName;
            ServerDataText[6] = Program.LastAssignedGuildID.ToString();
            ServerDataText[7] = Program.OnlineTime;
            File.WriteAllLines(ServerDataClass.ServerPathString, ServerDataText);
        }
    }
}
