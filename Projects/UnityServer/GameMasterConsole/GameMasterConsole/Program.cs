using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameMasterConsole
{
    class Program
    {
        public static string ConsoleInput;

        static void Main(string[] args)
        {
            while (true)
            {
                if (ConsoleInput == "clear")
                {
                    Console.Clear();
                    ConsoleInput = null;
                }

                if (ConsoleInput == "help")
                {
                    SlowlyWrite("--------");
                    SlowlyWrite("these are possible commands you can use:");
                    SlowlyWrite("--------");
                    SlowlyWrite("account create");
                    SlowlyWrite("account login");
                    SlowlyWrite("account logout");
                    SlowlyWrite("account delete");
                    SlowlyWrite("--------");
                    SlowlyWrite("guild create");
                    SlowlyWrite("guild join");
                    SlowlyWrite("guild leave");
                    SlowlyWrite("--------");
                    SlowlyWrite("save all");
                    SlowlyWrite("--------");
                    SlowlyWrite("debug");
                    SlowlyWrite("--------");
                }

                else if(ConsoleInput != "help" && ConsoleInput != "clear")
                {
                    TcpClient TCPClient = new TcpClient("127.0.0.1", 3045);
                    NetworkStream nwStreamTCP = TCPClient.GetStream();
                    ConsoleInput = Console.ReadLine();
                    byte[] bytesToSend;
                    bytesToSend = ASCIIEncoding.ASCII.GetBytes(ConsoleInput);
                    nwStreamTCP.Write(bytesToSend, 0, bytesToSend.Length);
                }
            }
        }

        public static void SlowlyWrite(string SlowlyType)
        {
            string SlowlyTypeHolder = UppercaseFirst(SlowlyType);

            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];

            foreach (char c in SlowlyTypeHolder)
            {
                Console.Write(c);
                Thread.Sleep(12);
            }
            if (last.ToString() != "." && last.ToString() != "!" && last.ToString() != "?" && last.ToString() != "-" && last.ToString() != "," && last.ToString() != ":" && last.ToString() != ";" && last.ToString() != "|" && last.ToString() != "/" && last.ToString() != "&");
            {
                Console.Write(".");
            }
            Console.WriteLine();
            ConsoleInput = null;
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
