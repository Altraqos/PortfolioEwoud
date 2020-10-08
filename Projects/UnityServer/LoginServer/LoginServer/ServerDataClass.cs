using System;
using System.IO;
using System.Threading;


namespace LoginServer
{
    class ServerDataClass
    {
        public static string ApplicationPath;
        public static string UserDataFolder;
        public static string GuildDataFolder;
        public static string ServerDataFolder;
        public static string ServerPathString;
        public static string CharacterPathString;
        public static string ServerWorldPathString;
        public static int maxPlayers = 125000;

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

        public static void OnServerStartUp()
        {
            Program.PlayerNames = new string[maxPlayers];
            ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            UserDataFolder = ApplicationPath + @"UserData";
            ServerDataFolder = ApplicationPath + @"ServerData";
            CharacterPathString = ApplicationPath + @"CharacterData";
            GuildDataFolder = ApplicationPath + @"GuildData";
            ServerPathString = ServerDataFolder + @"\ServerData.txt";
            ServerWorldPathString = ServerDataFolder + @"\ServerWorldData.txt";

            ReloadServerWorldData();
        }

        public static void ReloadServerWorldData()
        {
            try
            {
                if (File.Exists(ServerWorldPathString))
                {
                    string[] ServerWorldDataTextHolder = File.ReadAllLines(ServerWorldPathString);
                    string[] ServerWorldDataText = new string[15];

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
                    expRateString = ServerWorldDataText[0];
                    guildExpBonusString = ServerWorldDataText[1];
                    BonusExpString = ServerWorldDataText[2];
                    maxPlayersOnlineString = ServerWorldDataText[3];
                    maxPlayerLevelString = ServerWorldDataText[4];
                    minNameLengthString = ServerWorldDataText[5];
                    maxNameLengthString = ServerWorldDataText[6];
                    maxMoneyString = ServerWorldDataText[7];
                    maxGroupSizeString = ServerWorldDataText[8];
                    maxPlayersPerAccountString = ServerWorldDataText[9];
                    maxHeroicPlayersPerAccountString = ServerWorldDataText[10];
                    minPlayersToCreateGuildString = ServerWorldDataText[11];
                    ServerNameString = ServerWorldDataText[12];
                    ServerMotDString = ServerWorldDataText[13];
                    LoginMemoString = ServerWorldDataText[14];
                }
            }
            catch
            {
                return;
            }
        }

        public static void OnServerClose()
        {
            InitializeServerData.UpdateServerDataFiles();
        }
    }
}

