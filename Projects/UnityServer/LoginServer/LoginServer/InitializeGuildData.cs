using System.IO;

namespace LoginServer
{
    class InitializeGuildData
    {
        public static string GuildName;
        public static string GuildMasterName;
        public static int GuildLevel = 1;
        public static int LastAssignedGuildID;
        public static int currentGuilds;

        public static void CreateGuild()
        {
            if (!File.Exists(Program.GuildInput))
            {
                currentGuilds++;
                LastAssignedGuildID++;
                FileStream fs = File.Open(Program.GuildInput, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);
                fw.WriteLine(GuildName);
                fw.WriteLine(GuildLevel);
                fw.WriteLine(GuildMasterName);
                fw.Flush();
                fw.Close();

                DebugConsole.CleanLine();
                DebugConsole.SlowlyWriteVarData("created guild '" + GuildName + "'");
            }
            else
            {
                DebugConsole.CleanLine();
                DebugConsole.SlowlyWriteError("Guild with name '" + GuildName + "' already exists.");
                return;
            }
        }

        public static void DeleteGuild()
        {
            File.Delete(Program.GuildInput);
        }

        public static void JoinGuild()
        {
            if (File.Exists(Program.GuildInput))
            {
                string[] GuildDataText = File.ReadAllLines(Program.GuildInput);

                FileStream fs = File.Open(Program.GuildInput, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);
                fw.WriteLine(Program.guildJoinName);
                fw.Flush();
                fw.Close();
            }
        }

        public static void LeaveGuild()
        {

        }
    }
}