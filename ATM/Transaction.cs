using System;

namespace ATM
{
    public class Transaction
    {
        public DateTime CreateTime { get; private set; }
        public DateTime[] Time { get; private set; }
        public string[] OperationContent { get; private set; }
        private int Count { get; set; } = default;

        private void AddTime(DateTime operationTime)
        {
            if (operationTime != null)
            {
                if (OperationContent.Length == 1)
                    CreateTime = DateTime.Now;


                DateTime[] temp = new DateTime[Count];

                if (Time != null)
                {
                    Time.CopyTo(temp, 0);
                }

                temp[Count - 1] = operationTime;
                Time = temp;
            }
        }

        public void AddOperation(string operation)
        {
            if (!String.IsNullOrWhiteSpace(operation))
            {
                ++Count;
                string[] temp = new string[Count];

                if (OperationContent != null)
                {
                    OperationContent.CopyTo(temp, 0);
                }
                temp[Count - 1] = operation;
                OperationContent = temp;
                AddTime(DateTime.Now);
            }
        }

        public void ShowTransaction(char day)
        {
            if ((!char.IsWhiteSpace(day)) && (!char.IsLetter(day)))
            {
                DateTime date = CreateTime.AddDays(day - '0' - 1);

                for (int i = 0; i < Time.Length; i++)
                {
                    if (date.Day >= Time[i].Day && date.Month >= Time[i].Month && date.Year >= Time[i].Year)
                    {
                        Console.Write(new string(' ', (Console.WindowWidth - OperationContent[i].Length) / 2));
                        ShowMessage(OperationContent[i]);
                        Console.WriteLine();
                    }
                }
            }
            Console.ResetColor();
        }

        private void ShowMessage(string content)
        {
            if ((!String.IsNullOrWhiteSpace(content)))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(content);
            }
        }
    }
}
