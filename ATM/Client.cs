using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ATM
{
    public class Client
    {
        public readonly Transaction Transaction = new Transaction();

        private string _name;

        private string _surname;

        private Card _card;
        public Client(in string name, in string surname, in Card card)
        {
            Name = name;
            Surname = surname;
            Card = card;
        }

        public Card Card
        {
            get
            {
                return _card;
            }
            set
            {
                _card = value ?? throw new ArgumentNullException("Card must be not null.");
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[\p{L}]+$"))
                    throw new InvalidOperationException("Surname must be contain only characters.");

                _surname = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[\p{L}]+$"))
                    throw new InvalidOperationException("Name must be contain only characters.");

                _name = value;
            }
        }


        public void Show()
        {
            Console.WriteLine("================ Personal Information ================");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Surname: {Surname}");

            Console.WriteLine("================ Card Information ================");
            Card.Show();
        }
    }
}
