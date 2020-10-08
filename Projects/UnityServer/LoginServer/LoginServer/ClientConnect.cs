using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace LoginServer
{
    class ClientConnect
    {
        public static int Port;
        public static string ServerIP;

        public static string LoginServerInput;

        public static void TryConnect()
        {
            DebugConsole.SlowlyWrite("Login TCP Server");
            Int32.TryParse(InitializeServerData.LoginTCPServerPort, out Port);                             //Get the loginServer port, and convert to an int.
            ServerIP = InitializeServerData.Server_Ip;                                                     //get the server ip address.
            IPAddress localAdd = IPAddress.Parse(ServerIP);                                                //Convert the serverip string to an IPAddress.
            TcpListener server = new TcpListener(localAdd, Port);                                          //Combine the server ip and the login port to an TcpListener.
            server.Start();                                                                                //And start the server after that.
            DebugConsole.SlowlyWriteVarData("Started server on IP: '" + ServerIP + ":" + Port + "'");
            DebugConsole.SlowlyWrite("Connection avaiable to TCP Server.");
            DebugConsole.CleanLine();
            LoginServerInput = "";

            while (true)
            {
                try
                {
                    string[] ConsoleStringArray = LoginServerInput.Split(' ');

                    if (ConsoleStringArray[0] == "account")
                    {
                        if (ConsoleStringArray[1] == "create")
                        {
                            if (Program.currentPlayers != ServerDataClass.maxPlayers)
                            {
                                try
                                {
                                    Program.UserName = ConsoleStringArray[2];
                                    Program.Password = ConsoleStringArray[3];
                                    Program.LastAssignedName = Program.UserName;
                                    Program.pathString = Path.Combine(Program.UserDataFolder, (Program.UserName + " - UserData.txt"));
                                    Program.CreateAccount();
                                }
                                catch
                                {
                                    DebugConsole.SlowlyWriteError("Error, failed to create account.");
                                }
                            }
                            else
                            {
                                DebugConsole.SlowlyWriteErrorVarData("Error, at account limit: '" + ServerDataClass.maxPlayers + "'");
                            }
                        }
                        if (ConsoleStringArray[1] == "delete")
                        {
                            try
                            {
                                Program.pathString = Path.Combine(Program.UserDataFolder, (ConsoleStringArray[2] + " - UserData.txt"));
                                DebugConsole.SlowlyWriteVarData("Succesfully deleted account '" + DebugConsole.UppercaseFirst(ConsoleStringArray[2]) + "'");
                                Program.DeleteAccount();
                                ConsoleStringArray[2] = null;
                            }
                            catch
                            {
                                DebugConsole.SlowlyWriteErrorVarData("Error, failed to delete account' " + DebugConsole.UppercaseFirst(ConsoleStringArray[2]) + "'.");
                            }
                        }
                        if (ConsoleStringArray[1] == "login")
                        {
                            try
                            {
                                Program.UserName = ConsoleStringArray[2];
                                string UserDataString = Program.UserDataFolder + @"\" + Program.UserName + " - UserData.txt";

                                if (File.Exists(UserDataString))
                                {
                                    string[] UserDataText = File.ReadAllLines(UserDataString);

                                    if (UserDataText[1] == ConsoleStringArray[3])
                                    {
                                        if (!Program.PlayerNames.Contains(Program.UserName))
                                        {
                                            Program.LastLoginName = ConsoleStringArray[2];
                                            Program.LoginAccount();
                                        }
                                        else
                                        {
                                            DebugConsole.SlowlyWriteError("Couldn't log in, are you already logged in?");
                                        }
                                    }
                                    else
                                    {
                                        DebugConsole.SlowlyWriteError("Wrong password for user: " + Program.UserName + ".");
                                    }
                                }
                                else
                                {
                                    DebugConsole.SlowlyWriteError("This account doesn't exist.");
                                }
                            }
                            catch
                            {
                                DebugConsole.SlowlyWriteError("Error couldn't login.");
                            }
                        }
                        if (ConsoleStringArray[1] == "update")
                        {
                            Program.UpdatePathString = Path.Combine(Program.UserDataFolder, (ConsoleStringArray[2] + " - UserData.txt"));
                            Program.WriteToAccountData();
                        }
                        if (ConsoleStringArray[1] == "logout")
                        {
                            Program.UpdatePathString = Path.Combine(Program.UserDataFolder, (ConsoleStringArray[2] + " - UserData.txt"));
                            Program.WriteToAccountData();
                            Program.ActivePlayers--;
                            DebugConsole.ArrangePlayerNamesInArray();
                            DebugConsole.ShowActivePlayers();
                        }
                        if (ConsoleStringArray[1] == "debug")
                        {
                            DebugConsole.ShowActivePlayers();
                        }
                    }
                    if (ConsoleStringArray[0] == "save")
                    {
                        if (ConsoleStringArray[1] == "all")
                        {
                            Program.ServerPathString = Path.Combine(Program.ServerDataFolder, "ServerData.txt");
                            InitializeServerData.UpdateServerDataFiles();
                        }
                    }
                    if (ConsoleStringArray[0] == "guild")
                    {
                        if (ConsoleStringArray[1] == "create")
                        {
                            int GuildNameInt = 0;
                            string GuildName = null;

                            if (ConsoleStringArray[3].Contains("'"))
                            {
                                var builder = new StringBuilder();

                                for (int i = 3; i < ConsoleStringArray.Length; i++)
                                {
                                    foreach (char c in ConsoleStringArray[i])
                                    {
                                        if ("'" == c.ToString())
                                        {
                                            GuildNameInt += 1;

                                            if (GuildNameInt == 2)
                                            {
                                                GuildNameInt = 0;
                                            }
                                        }
                                        if (GuildNameInt > 0)
                                        {
                                            if ("'" != c.ToString())
                                            {
                                                builder.Append(c);
                                            }
                                        }
                                    }
                                    if (i < ConsoleStringArray.Length - 1)
                                    {
                                        builder.Append(" ");
                                    }
                                }
                                GuildName = builder.ToString();
                            }
                            Program.LastAssignedGuildName = GuildName;
                            InitializeGuildData.GuildMasterName = ConsoleStringArray[2];
                            Program.GuildInput = Path.Combine(Program.GuildDataFolder, (GuildName + " - GuildData.txt"));
                            InitializeGuildData.GuildName = GuildName;
                            InitializeGuildData.CreateGuild();
                        }

                        if (ConsoleStringArray[1] == "join")
                        {
                            int GuildNameInt = 0;
                            string GuildName = null;
                            Program.guildJoinName = ConsoleStringArray[2];
                            string UserDataString = Program.UserDataFolder + @"\" + Program.guildJoinName + " - UserData.txt";

                            if (File.Exists(UserDataString))
                            {
                                if (ConsoleStringArray[3].Contains("'"))
                                {
                                    var builder = new StringBuilder();

                                    for (int i = 3; i < ConsoleStringArray.Length; i++)
                                    {
                                        foreach (char c in ConsoleStringArray[i])
                                        {
                                            if ("'" == c.ToString())
                                            {
                                                GuildNameInt += 1;

                                                if (GuildNameInt == 2)
                                                {
                                                    GuildNameInt = 0;
                                                }
                                            }
                                            if (GuildNameInt > 0)
                                            {
                                                if ("'" != c.ToString())
                                                {
                                                    builder.Append(c);
                                                }
                                            }
                                        }
                                        if (i < ConsoleStringArray.Length - 1)
                                        {
                                            builder.Append(" ");
                                        }
                                    }
                                    GuildName = builder.ToString();
                                    Program.GuildInput = Path.Combine(Program.GuildDataFolder, GuildName + " - GuildData.txt");

                                    if (File.Exists(Program.GuildInput))
                                    {
                                        DebugConsole.SlowlyWriteVarData("'" + Program.guildJoinName + "' succesfully joined guild: '" + Program.LastAssignedGuildName + "'");
                                        InitializeGuildData.JoinGuild();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    DebugConsole.CleanLine();
                    //DebugConsole.SlowlyWriteError("ERROR");
                    DebugConsole.SlowlyWriteError(e.ToString());
                }
            }
        }
    }
}
