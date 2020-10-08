using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace LoginTCPServer
{
    class Program
    {
        public static string GuildDataFolder;
        public static string ApplicationPath;
        public static string UserDataFolder;
        public static string ServerDataFolder;
        public static string CharacterPathString;
        public static string pathString;
        public static string ServerPathString;
        public static string ServerWorldPathString;
        public static string UpdatePathString;
        public static string UserName;
        public static string Password;
        public static int currentPlayers = 0;
        public static int ActivePlayers = -1;
        public static string GuildInput;

        //Player Data
        public static int CurrentExp = 0;
        public static int CurrentLevel = 1;
        public static int GmLevel = 1;
        public static string[] PlayerNames;
        public static string LastLoginName;
        public static string LastAssignedName;
        public static string LastAssignedGuildName;
        public static string guildJoinName;
        public static int LastAssignedID = 0;
        public static int NetUserID = 0;
        public static int LastAssignedGuildID = 0;
        public static string OnlineTime;

        static void Main(string[] args)
        {
            ServerDataClass.OnServerStartUp();
            UserDataFolder = ServerDataClass.UserDataFolder;
            GuildDataFolder = ServerDataClass.GuildDataFolder;
            ApplicationPath = ServerDataClass.ApplicationPath;
            ServerDataFolder = ServerDataClass.ServerDataFolder;
            ServerPathString = ServerDataClass.ServerPathString;
            CharacterPathString = ServerDataClass.CharacterPathString;
            ServerWorldPathString = ServerDataClass.ServerWorldPathString;

            DebugConsole.OnstartServerDebug();
            InitializeServerData.InitializeServerDataFiles();
            DebugConsole.DebugLog();

            while (true)
            {
                ConnectToLoginServer.TryConnect();             
            }
        }

        public static void CreateAccount()
        {
            if (!File.Exists(pathString))
            {
                currentPlayers++;
                LastAssignedID++;
                NetUserID = LastAssignedID;

                FileStream fs = File.Open(pathString, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);
                fw.WriteLine(UserName);
                fw.WriteLine(Password);
                fw.WriteLine(CurrentLevel);
                fw.WriteLine(GmLevel);
                fw.WriteLine(CurrentExp);
                fw.WriteLine(UserName);
                fw.WriteLine(LastAssignedID);
                fw.Flush();
                fw.Close();

                DebugConsole.SlowlyWriteVarData("Succesfully created account '" + DebugConsole.UppercaseFirst(UserName) + "'");
                DebugConsole.CleanLine();
            }
            else
            {
                DebugConsole.CleanLine();
                DebugConsole.SlowlyWriteErrorVarData("Account with name '" + DebugConsole.UppercaseFirst(UserName) + "' already exists.");
                return;
            }
        }

        public static void DeleteAccount()
        {
            File.Delete(pathString);
        }

        public static void LoginAccount()
        {
            DebugConsole.SlowlyWrite("You have logged in now.");
            DebugConsole.CleanLine();
            ActivePlayers++;
            DebugConsole.ShowActivePlayers();
            ServerPathString = Path.Combine(ServerDataFolder, "ServerData.txt");
            InitializeServerData.UpdateServerDataFiles();
        }

        public static void WriteToAccountData()
        {
            if (File.Exists(UpdatePathString))
            {
                string[] PlayerDataText = File.ReadAllLines(UpdatePathString);
                PlayerDataText[2] = GmLevel.ToString();
                PlayerDataText[3] = CurrentLevel.ToString();
                PlayerDataText[4] = CurrentExp.ToString();
                PlayerDataText[5] = PlayerNames[12];
                File.WriteAllLines(UpdatePathString, PlayerDataText);
            }
            else
            {
                DebugConsole.SlowlyWriteErrorVarData("Can't write to account '" + DebugConsole.UppercaseFirst(UserName) + "'.");
                return;
            }
        }
    }
}

