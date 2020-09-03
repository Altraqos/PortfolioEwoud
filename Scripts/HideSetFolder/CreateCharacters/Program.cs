using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CreateCharacters
{class Program
    {
        public static string defaultPassword = "password";

        static void Main(string[] args){top:Console.Clear();string pass = "";string password = "";pass = onStartup(pass);checkIfHidden();password = EnterPass(password);if (password == pass)makeHidden();if (password == "change")changePass();if (password == "reset")defaultPass();if (password == "close")Environment.Exit(0);
            else if (password != pass)
                SlowlyWrite("you entered the wrong password, try again");
            Task.Delay(125).Wait();
            goto top;
        }

        public static string EnterPass(string password)
        {
            StringBuilder sBuilder = new StringBuilder();
            string passHolder = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key != ConsoleKey.Backspace)
                    Console.Write("*");
                if (key.Key == ConsoleKey.Backspace && sBuilder.Length > 0)
                {
                    Console.Write("\b \b");
                    sBuilder.Length--;}else sBuilder.Append(key.KeyChar.ToString());passHolder = sBuilder.ToString();}Console.WriteLine();Console.Clear();return password = passHolder;}

        public static string onStartup(string password)
        {
        createPassFile:
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Pass.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                string passPathHolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/Pass.txt");
                FileStream fs = File.Open(passPathHolder, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);
                fw.WriteLine("sylvanas");
                fw.Flush();
                fw.Close();
                password = "";
                File.SetAttributes(path, FileAttributes.Hidden);
            }
            if (File.Exists(path))
            {
                try
                {
                    string[] pathTexts = File.ReadAllLines(path);
                    password = pathTexts[0];
                }
                catch
                {
                    File.Delete(path);
                    goto createPassFile;
                }
            }
            return password;
        }

        public static void defaultPass()
        {
            SlowlyWrite("Enter your current password:");
            string oldPass = "";
            string textPass = "";
            textPass = onStartup(textPass);
            oldPass = EnterPass(oldPass);
            if (oldPass == textPass)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Pass.txt"))
                {
                    string passPathHolder = AppDomain.CurrentDomain.BaseDirectory + "/Pass.txt";
                    File.WriteAllText(passPathHolder, String.Empty);
                    FileStream fs = File.Open(passPathHolder, FileMode.Append, FileAccess.Write);
                    StreamWriter fw = new StreamWriter(fs);
                    fw.WriteLine(defaultPassword);
                    fw.Flush();
                    fw.Close();
                }
                SlowlyWrite("changed password to the default password");
                return;
            }
        }

        public static void changePass()
        {
            SlowlyWrite("Enter your current password:");
            string oldPass = "";
            string textPass = "";
            textPass = onStartup(textPass);
            oldPass = EnterPass(oldPass);
            if (oldPass != textPass)
                SlowlyWrite("you entered the wrong password, try again");
            if (oldPass == textPass)
            {
                SlowlyWrite("Enter what you wish the password to be");
                string newPass = "";
                newPass = EnterPass(newPass);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Pass.txt"))
                {
                    string passPathHolder = AppDomain.CurrentDomain.BaseDirectory + "/Pass.txt";
                    File.WriteAllText(passPathHolder, String.Empty);
                    FileStream fs = File.Open(passPathHolder, FileMode.Append, FileAccess.Write);
                    StreamWriter fw = new StreamWriter(fs);
                    fw.WriteLine(newPass);
                    fw.Flush();
                    fw.Close();
                }
                SlowlyWrite("Succesfully changed your password");
            }
        }

        public static void checkIfHidden()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Location.txt";
            if (!File.Exists(path))
                File.Create(path);
            string[] pathTexts = File.ReadAllLines(path);
            var di = new DirectoryInfo(pathTexts[0]);
            if (!Directory.Exists(pathTexts[0]))
                SlowlyWrite("The specified folder doesn't excist in the current context.");
            else if (Directory.Exists(pathTexts[0]))
            {
                if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    SlowlyWrite("Enter the password to hide the good stuff");
                else
                    SlowlyWrite("Enter the password to show the good stuff");
            }
        }public static void makeHidden(){string[] pathTexts = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/Location.txt");var di = new DirectoryInfo(pathTexts[0]);if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden){di.Attributes |= FileAttributes.Hidden;SlowlyWrite("Should be hidden");}else if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden){di.Attributes = di.Attributes & ~FileAttributes.Hidden;SlowlyWrite("Should be visible");}}public static void SlowlyWrite(string SlowlyType){if (string.IsNullOrEmpty(SlowlyType))return;SlowlyType = char.ToUpper(SlowlyType[0]) + SlowlyType.Substring(1);foreach (char c in SlowlyType){Console.Write(c);Task.Delay(7).Wait();}if (Char.IsLetter(SlowlyType[SlowlyType.Length - 1]))Console.Write(".");Console.WriteLine();}}}