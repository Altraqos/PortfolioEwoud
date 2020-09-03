using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class ChoicesClass
    {


        public static void Choices()
        {
            int lastChoice = 0;
            StringBuilder stringBuilder = new StringBuilder();
            string choiceHolder = "";
            List<int> choiceList = new List<int>();
            ChooseChoice:
            for (int i = 0; i < choiceList.Count; i++)
            {
                SlowlyWrite("your current choices are: Choice [" + i + "] {" + choiceList[i] + "}");
            }
            SlowlyWrite("-------------------------------------------------------------------");
            SlowlyWrite("press a number from 1-4, you last choice was: " + lastChoice);
            int choice = 1;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    stringBuilder.Append("1");
                    choiceHolder = stringBuilder.ToString();
                    lastChoice = choice;
                    choiceList.Add(lastChoice);
                    OpenVideoWithId(choiceHolder);
                    goto ChooseChoice;

                case 2:
                    stringBuilder.Append("2");
                    choiceHolder = stringBuilder.ToString();
                    lastChoice = choice;
                    choiceList.Add(lastChoice);
                    OpenVideoWithId(choiceHolder);
                    goto ChooseChoice;

                case 3:
                    stringBuilder.Append("3");
                    choiceHolder = stringBuilder.ToString();
                    lastChoice = choice;
                    choiceList.Add(lastChoice);
                    OpenVideoWithId(choiceHolder);
                    goto ChooseChoice;

                case 4:
                    stringBuilder.Append("4");
                    choiceHolder = stringBuilder.ToString();
                    lastChoice = choice;
                    choiceList.Add(lastChoice);
                    OpenVideoWithId(choiceHolder);
                    goto ChooseChoice;

                default:
                    stringBuilder.Append("1");
                    choiceHolder = stringBuilder.ToString();
                    lastChoice = 1;
                    choiceList.Add(lastChoice);
                    OpenVideoWithId(choiceHolder);
                    goto ChooseChoice;
            }
        }

        public static void OpenVideoWithId(string stringID)
        {
            int id;
            Int32.TryParse(stringID, out id);
            SlowlyWrite("open video with id: [" + id + "]");
        }


        private static void SlowlyWrite(string SlowlyType)
        {
            string SlowlyTypeHolder = char.ToUpper(SlowlyType[0]) + SlowlyType.Substring(1);
            char last = SlowlyTypeHolder[SlowlyTypeHolder.Length - 1];
            foreach (char c in SlowlyTypeHolder)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            if (last.ToString() != (".") && last.ToString() != "!" && last.ToString() != "?" && last.ToString() != " " && last.ToString() != ":" && last.ToString() != "-")
                Console.Write(".");
            Console.WriteLine();
        }
    }
}
