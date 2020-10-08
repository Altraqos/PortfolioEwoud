using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer
{
    class DebugConsole
    {
        public static void SlowlyWrite(string SlowlyType)
        {
            string SlowlyTypeHolder = UppercaseFirst(SlowlyType);

            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];

            foreach (char c in SlowlyTypeHolder)
            {
                Console.Write(c);
                Thread.Sleep(12);
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        Thread.Sleep(12);
                    }
                }

                if (varMark == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    if ("'" != c.ToString())
                    {
                        Console.Write(c);
                        Thread.Sleep(12);
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
                        Thread.Sleep(12);
                    }
                }

                if (varMark == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    if ("'" != c.ToString())
                    {
                        Console.Write(c);
                        Thread.Sleep(12);
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
                Thread.Sleep(12);
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

            for (int i = 0; i < 7; i++)
            {
                Console.Write("-");
                Thread.Sleep(12);
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
