using System;
using System.IO;
using System.Threading;

namespace UnityServer
{
    class ReadServerDataFiles
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
        public static string ApplicationPath;
        public static string ServerDataFolder;
        public static string ServerWorldPathString;

        public static void OnserverStartUp()
        {
            ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            ServerDataFolder = ApplicationPath + @"ServerData";
            ServerWorldPathString = ServerDataFolder + @"\ServerWorldData.txt";

            ReadWorldServerDataFiles();
        }

        public static void ReadWorldServerDataFiles()
        {
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
            }
        }
    }
}
