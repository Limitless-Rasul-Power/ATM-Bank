using System;
using System.Linq;
using System.Text;

namespace ATM
{
    public class Card
    {
        public string PAN { get; private set; }
        public string CVC { get; private set; }

        private string _pin;
        public DateTime ValidityTime { get; private set; } = DateTime.Now.AddYears(3);
        public string ExpireMonthAndYear { get; private set; }

        private decimal _balance;

        public Card(string pin, decimal balance)
        {
            PIN = pin;
            Balance = balance;
            PAN = CardOperationHelper.CreatePan();
            CVC = CardOperationHelper.CreateCVC();
            ExpireMonthAndYear += ValidityTime.Month + "/" + ValidityTime.Year;
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                if (Verify.IsBalanceNegativeNumber(value))
                    throw new InvalidOperationException("Balance must be more than 0.");

                _balance = value;
            }
        }

        public string PIN
        {
            get
            {
                return _pin;
            }
            set
            {
                if (Verify.IsPinIncorrect(value))
                    throw new InvalidOperationException("Pin must be 4 digit and number.");

                _pin = value;
            }
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"PAN: {PAN}");
            Console.WriteLine($"PIN: {CardOperationHelper.HidePin(_pin)}");
            Console.WriteLine($"CVC: {CVC}");
            Console.WriteLine($"Balance: {Balance}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Expire Time(Month/Year) : {ExpireMonthAndYear}");
            Console.ResetColor();
        }

    }
}
