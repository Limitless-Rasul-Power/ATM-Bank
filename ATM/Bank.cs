using System;

namespace ATM
{
    public class Bank
    {
        public static int FindPin(Client[] clients, string pin)
        {
            if (clients != null && pin.Length == 4)
            {
                for (int i = 0; i < clients.Length; i++)
                {
                    if (clients[i].Card.PIN == pin)
                    {
                        return i;
                    }
                }
            }
            throw new InvalidOperationException($"There is No \"{pin}\" pin in the bank.");
        }
        public static string[] GetWithdrawOptions()
        {
            string[] withdrawOptions = new string[] { "10", "20", "50", "100", "Other" };
            return withdrawOptions;
        }
        public static string[] GetOperations()
        {
            string[] operations = new string[] { "1.Balance", "2.Withdraw", "3.List of Transactions", "4.Money Transfer(card to card)", "5.Exit" };
            return operations;
        }

        public static string[] GetTransactions()
        {
            string[] transactions = new string[] { "1.Within 1 Day", "2.Within 5 Day", "3.Within 10 Day" };
            return transactions;
        }
        public static void Print(string[] operations)
        {
            if (operations != null)
            {
                for (int i = 0; i < operations.Length; i++)
                    Console.WriteLine(operations[i]);
            }

        }

        public static void ShowClientBalance(Client client)
        {
            if (client != null)
                client.Show();
        }

        public static void PrintWithdrawOptions(string[] options)
        {
            if (options != null)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {options[i]}");
                }
            }
        }

        static public int FindTransferedPin(Client[] clients, string searchedPin, string yourPin)
        {
            if (clients != null && (!string.IsNullOrWhiteSpace(searchedPin)) && (!string.IsNullOrWhiteSpace(yourPin)))
            {
                if (searchedPin.Length == 4 && yourPin.Length == 4 && searchedPin != yourPin)
                {
                    for (int i = 0; i < clients.Length; i++)
                    {
                        if (clients[i].Card.PIN == searchedPin)
                        {
                            return i;
                        }
                    }
                }
            }

            throw new InvalidOperationException($"Searced PIN => {searchedPin} didn't exist or it is your PIN, you don't allowed to transfer money to yourself.");
        }

    }
}

