using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleLessen
{
    class Program
    {
        static void Main(string[] args)
        {
            SlowlyWrite("Typ een getal van 1 tot 6 in om de desbetreffende opdracht te openen");
            Console.WriteLine();
            string UserInputMain = Console.ReadLine();

            try
            {
                int Case;
                Int32.TryParse(UserInputMain, out Case);

                switch (Case)
                {
                    case 1 :
                        SlowlyWrite("Opdracht 1.");
                        Console.WriteLine();
                        Console.WriteLine();

                        Opdracht1();
                        break;

                    case 2:
                        SlowlyWrite("Opdracht 2.");
                        Console.WriteLine();
                        Console.WriteLine();

                        Opdracht2();
                        break;

                    case 3:
                        SlowlyWrite("Opdracht 3.");
                        Console.WriteLine();
                        Console.WriteLine();

                        Opdracht3();
                        break;

                    case 4:
                        SlowlyWrite("Opdracht 4.");
                        Console.WriteLine();
                        Console.WriteLine();

                        Opdracht4();
                        break;

                    case 5:
                        SlowlyWrite("Opdracht 5.");
                        Console.WriteLine();
                        Console.WriteLine();

                        Opdracht5();
                        break;

                    case 6:
                        SlowlyWrite("Opdracht 6.");
                        Console.WriteLine();
                        Console.WriteLine();

                        Opdracht6();
                        break;

                }
            }

            catch
            {
                SlowlyWrite("Sorry dat was geen cijfer...");
            }
        }

        public static void Opdracht1()
        {
            //Opracht 1

            bool isLeaving = false;
                SlowlyWrite("Voer je chat-naam in.");
                Console.WriteLine();
                string NameHolder = Console.ReadLine();
                string Name = UpperCaseFirst(NameHolder.ToLower());

                SlowlyWrite("Je bent ingelogt " + Name + "!");
                Console.WriteLine();

                while (!isLeaving)
                {
                    SlowlyWrite(Name + ": ");
                    string QuestionHolder = Console.ReadLine();
                    string Question = QuestionHolder.ToLower();

                    if (Question == "/leave")
                    {
                        isLeaving = true;
                    }
                    else
                    {
                        SlowlyWrite(UpperCaseFirst(Question) + "?");
                        Console.WriteLine();
                    }
                }
        }

        public static void Opdracht2()
        {
            //Opracht 2

            SlowlyWrite("Typ een zin.");
            Console.WriteLine();
            string SentenceHolder = Console.ReadLine();
            string Sentence = SentenceHolder.ToLower();

            SlowlyWrite("Resultaat:");
            Console.WriteLine();

            SlowlyWriteOpdracht2(Sentence);

            SlowlyWrite("Finished!");
            Console.WriteLine();
        }

        public static void Opdracht3()
        {
            //Opracht 3
            string SentenceHolder;
            Console.WriteLine("Typ de geheime boodschap.");
            SentenceHolder = Console.ReadLine();

            string Sentence = SentenceHolder.ToLower();

            Console.WriteLine("Vul het rotatie nummer in.");

            int RotationVal = int.Parse(Console.ReadLine());
            string sum = null;

            for (int i = 0; i < Sentence.Length; i++)
            {
                if (Sentence[i] == ' ')
                {
                    sum += Sentence[i].ToString();
                }

                if ('a' <= Sentence[i] && Sentence[i] <= 'z')
                {
                    int nextLetter = Sentence[i] + RotationVal;

                    if (nextLetter > 'z')
                    {
                        nextLetter = nextLetter - 26;
                    }
                    char c;
                    c = (char)nextLetter;
                    sum += c.ToString();
                }
                else
                    sum += Sentence[i].ToString();
            }
            SlowlyWrite("Resultaat:");
            Console.WriteLine();
            SlowlyWrite(UpperCaseFirst(sum));
            Console.WriteLine();
        }

        public static void Opdracht4()
        {
            //Opdracht 4

            SlowlyWrite("Typ een zin.");
            Console.WriteLine();


            string UserSentenceHolder = Console.ReadLine();

            string[] UserSentenceArray = UserSentenceHolder.Split(' ');

            int longestWord = 0;
            int longestWordPos = 0;

            for (int i = 0; i < UserSentenceArray.Length; i++)
            {
                if (UserSentenceArray[i].Length > longestWord)
                {
                    longestWord = UserSentenceArray[i].Length;
                    longestWordPos = i;
                }
            }
            SlowlyWrite(UpperCaseFirst("longest word is " + longestWord + " characters long."));
            Console.WriteLine();
            SlowlyWrite(UpperCaseFirst("the longest word is: " + UserSentenceArray[longestWordPos]));
            Console.WriteLine();
        }

        public static void Opdracht5()
        {
            //Opdracht 5

            SlowlyWrite("Typ een zin.");
            Console.WriteLine();


            string UserSentenceHolder = Console.ReadLine();

            int[] maxCount = new int[1000];
            int max = 0;
            char MostFrequent = Char.MinValue;
            Array.Clear(maxCount, 0, maxCount.Length - 1);

            foreach (char c in UserSentenceHolder)
            {
                if (" " != c.ToString())
                {
                    if (++maxCount[c] > max)
                    {
                        max = maxCount[c];
                        MostFrequent = c;
                    }
                }
            }

            SlowlyWrite(UpperCaseFirst("most used letter is: ") + MostFrequent);
            Console.WriteLine();
        }

        public static void Opdracht6()
        {
            //Opdracht 6
            int output = 0;
            string Input = "";
            int InputInt = 0;
            int numberCount = 0;
            int length = 1;
            int[] numberHolders;

            while (true)
            {
            Start:
                numberHolders = new int[length];

                SlowlyWrite("Typ een nummer.");
                Console.WriteLine();
                Input = Console.ReadLine();

                if (Input == "/restart")
                {
                    Array.Clear(numberHolders, 0, numberHolders.Length);
                    output = 0;
                    Input = "";
                    InputInt = 0;
                    numberCount = 0;
                    length = 1;
                    goto Start;
                }
                if (Input == "/stop")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Int32.TryParse(Input, out InputInt);

                    numberHolders[numberCount] = InputInt;
                    output += numberHolders[numberCount];
                    numberCount++;
                    length++;

                    if (numberCount > 1)
                    {
                        float result = output / numberHolders.Length;
                        SlowlyWrite("gemiddlde is: " + result);
                    }
                    else
                    {
                        SlowlyWrite("gemiddlde is: " + InputInt);
                    }
                }
            }
        }

        public static void SlowlyWrite(string SlowlyWriteString)
        {
            foreach (char c in SlowlyWriteString)
            {
                Console.Write(c);
                Thread.Sleep(12);
            }
        }

        public static void SlowlyWriteOpdracht2(string SlowlyWriteString)
        {
            foreach (char c in SlowlyWriteString)
            {
                if(c.ToString() != "a" && c.ToString() != "e" && c.ToString() != "o" && c.ToString() != "u" && c.ToString() != "i")
                Console.Write(c);
                Thread.Sleep(12);
            }
            Console.WriteLine();
        }

        public static string UpperCaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
