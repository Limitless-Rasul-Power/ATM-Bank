using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            Card card1 = new Card("1234", 1000);
            Card card2 = new Card("1111", 2000);
            Card card3 = new Card("2222", 3000);
            Card card4 = new Card("3333", 4000);
            Card card5 = new Card("4554", 5000);


            Client c1 = new Client("Mike", "Tyson", card1);
            Client c2 = new Client("John", "Guerrera", card2);
            Client c3 = new Client("Anthony", "Williams", card3);
            Client c4 = new Client("Will", "Smith", card4);
            Client c5 = new Client("Marciel", "Paulo", card5);
            Client[] clients = new Client[5] { c1, c2, c3, c4, c5 };

            string[] operations = Bank.GetOperations();
            string[] withdrawOptions = Bank.GetWithdrawOptions();
            string[] transactions = Bank.GetTransactions();

            while (true)
            {

                try
                {
                    Console.Write("Enter card pin: ");
                    string pin = Console.ReadLine();
                    int index = Bank.FindPin(clients, pin);

                    clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} visits Bank. Time[{DateTime.Now.ToString("F")}]");

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Welcome {clients[index].Name} {clients[index].Surname} which operation do you want to do ?");

                        Bank.Print(operations);

                        Console.Write("Enter: ");
                        char option = Console.ReadKey().KeyChar;

                        while (Verify.IsChoiceIncorrect(operations.Length, option))
                        {
                            Console.WriteLine();
                            Console.Write("Enter one of this choices (1, 2, 3, 4, 5): ");
                            option = Console.ReadKey().KeyChar;
                        }

                        Console.Clear();

                        if (option == (char)Options.Exit)
                        {
                            Console.WriteLine("See you soon.");
                            clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} exits bank. Time[{DateTime.Now.ToString("F")}]");
                            break;
                        }

                        switch (option)
                        {
                            case (char)Options.Balance:
                                {
                                    Bank.ShowClientBalance(clients[index]);

                                    clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} selects show balance choice. Time[{DateTime.Now.ToString("F")}]");
                                    Console.Write("Press any key to continue...");
                                    Console.Read();
                                }
                                break;
                            case (char)Options.Withdraw:
                                {
                                    Console.WriteLine("How much money do you want to withdraw ?");
                                    Bank.PrintWithdrawOptions(withdrawOptions);
                                    char choice = Console.ReadKey().KeyChar;
                                    clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} selects withdraw choice. Time[{DateTime.Now.ToString("F")}]");

                                    while (Verify.IsChoiceIncorrect(withdrawOptions.Length, choice))
                                    {
                                        Console.WriteLine();
                                        Console.Write("Enter one of this choices (1, 2, 3, 4, 5): ");
                                        choice = Console.ReadKey().KeyChar;
                                    }
                                    decimal withdrawedMoney;

                                    try
                                    {
                                        Console.Clear();
                                        if (choice != withdrawOptions.Length + '0')
                                        {
                                            int choiceIndex = choice - '0' - 1;
                                            withdrawedMoney = decimal.Parse(withdrawOptions[choiceIndex]);

                                            if (Verify.IsBalanceMoreThanWithdrawedMoney(clients[index].Card.Balance, withdrawedMoney))
                                            {
                                                Console.WriteLine("Withdraw successfully completed.");
                                                clients[index].Card.Balance -= withdrawedMoney;

                                                clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} withdrawed {withdrawedMoney} $. Time[{DateTime.Now.ToString("F")}]");
                                            }
                                            else
                                            {
                                                clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} do invalid operation in withdraw section. Time[{DateTime.Now.ToString("F")}]");
                                                throw new InvalidOperationException($"Sorry there is no {withdrawOptions[choiceIndex]} $ in your balance.");
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.Write("How much money do you want to withdraw: ");

                                            string otherWithdrawOption;
                                            while (Verify.IsInputNotNumber(otherWithdrawOption = Console.ReadLine()))
                                            {
                                                Console.WriteLine();
                                                Console.Write("Enter number please: ");
                                            }

                                            withdrawedMoney = decimal.Parse(otherWithdrawOption);
                                            if (Verify.IsBalanceMoreThanWithdrawedMoney(clients[index].Card.Balance, withdrawedMoney))
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Withdraw successfully completed.");
                                                clients[index].Card.Balance -= withdrawedMoney;

                                                clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} withdrawed {withdrawedMoney} $. Time[{DateTime.Now.ToString("F")}]");
                                            }
                                            else
                                            {
                                                clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} do invalid operation in withdraw section. Time[{DateTime.Now.ToString("F")}]");
                                                throw new InvalidOperationException($"Sorry there is no {withdrawedMoney} $ in your balance or entered number is negative.");
                                            }
                                        }
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                    catch (InvalidOperationException caption)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(caption.Message);
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                }
                                break;
                            case (char)Options.ListOfTransactions:
                                {
                                    Bank.Print(transactions);
                                    char choice = Console.ReadKey().KeyChar;

                                    while (Verify.IsChoiceIncorrect(transactions.Length, choice))
                                    {
                                        Console.WriteLine();
                                        Console.Write("Enter one of this choices (1, 2, 3): ");
                                        choice = Console.ReadKey().KeyChar;
                                    }
                                    Console.WriteLine();
                                    Console.Clear();

                                    if (choice == (char)TransactionNames.WithinOneDay)
                                    {
                                        clients[index].Transaction.ShowTransaction((char)TransactionNames.WithinOneDay);
                                    }
                                    else if (choice == (char)TransactionNames.WithinFiveDay)
                                    {
                                        clients[index].Transaction.ShowTransaction((char)TransactionNames.WithinFiveDay);
                                    }
                                    else
                                    {
                                        clients[index].Transaction.ShowTransaction((char)TransactionNames.WithinTenDay);
                                    }
                                    Console.Write("Press any key to continue...");
                                    Console.ReadLine();
                                }
                                break;
                            case (char)Options.MoneyTransfer:
                                {
                                    Console.WriteLine("Enter transfered card pin to start money transfer: ");
                                    string transferedCardPin = Console.ReadLine();
                                    clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} selects Money Transfer choice. Time[{DateTime.Now.ToString("F")}]");

                                    try
                                    {
                                        int transferedClientIndex = Bank.FindTransferedPin(clients, transferedCardPin, pin);
                                        Console.Clear();
                                        Console.WriteLine($"How much money do you want to transfer to {clients[transferedClientIndex].Name} {clients[transferedClientIndex].Surname}: ");

                                        string input;
                                        while (Verify.IsInputNotNumber(input = Console.ReadLine()))
                                        {
                                            Console.WriteLine();
                                            Console.Write("Enter number please: ");
                                        }
                                        decimal transferedMoney = decimal.Parse(input);

                                        if (Verify.IsBalanceMoreThanWithdrawedMoney(clients[index].Card.Balance, transferedMoney))
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Transfer Successfully Completed!");
                                            clients[transferedClientIndex].Card.Balance += transferedMoney;
                                            clients[index].Card.Balance -= transferedMoney;
                                            System.Threading.Thread.Sleep(1000);

                                            clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} transfers {transferedMoney} $ to {clients[transferedClientIndex].Name} {clients[transferedClientIndex].Surname}. Time[{DateTime.Now.ToString("F")}]");
                                        }
                                        else
                                        {
                                            clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} do invalid operation in Money Transfer section. Time[{DateTime.Now.ToString("F")}]");
                                            throw new InvalidOperationException($"Sorry there is no {transferedMoney} $ in your card or this money is negative number.");
                                        }
                                    }
                                    catch (InvalidOperationException caption)
                                    {
                                        clients[index].Transaction.AddOperation($"{clients[index].Name} {clients[index].Surname} do invalid operation in Money Transfer section. Time[{DateTime.Now.ToString("F")}]");
                                        Console.Clear();
                                        Console.WriteLine(caption.Message);
                                        System.Threading.Thread.Sleep(1000);
                                    }

                                }
                                break;
                        }
                        Console.Clear();
                    }
                }
                catch (InvalidOperationException caption)
                {
                    Console.Clear();
                    Console.WriteLine(caption.Message);
                    System.Threading.Thread.Sleep(1000);
                }
                Console.Clear();
            }
        }
    }    
}
