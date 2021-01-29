using System;

namespace ATM
{
    public class Verify
    {
        static public bool IsPinIncorrect(string pin)
        {
            if (pin.Length != 4)
                return true;

            return !int.TryParse(pin, out _);
        }

        static public bool IsBalanceNegativeNumber(decimal balance)
        {
            return balance < 0;
        }

        static public bool IsInputNotNumber(string balance)
        {
            return !decimal.TryParse(balance, out _);
        }

        static public bool IsChoiceIncorrect(in int operationCount, in char option)
        {
            for (int i = 1; i <= operationCount; i++)
            {
                if (option - '0' == i)
                    return false;
            }
            return true;
        }

        static public bool IsBalanceMoreThanWithdrawedMoney(in decimal money, in decimal withdrawedMoney)
        {
            return money >= withdrawedMoney && withdrawedMoney >= 0;
        }


    }
}
