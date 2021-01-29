using System;
using System.Text;

namespace ATM
{
    public class CardOperationHelper
    {
        private static readonly string[] CVCCollector = new string[100];
        private static readonly StringBuilder[] PANCollector = new StringBuilder[100];
        private static int ClientCount { get; set; } = default;


        private static int Randomizer(int minimum, int maximum)
        {
            Random random = new Random();
            return random.Next(minimum, maximum);
        }
        private static int TakeNumberThatIsNotInTheCVCCollection(int minimum, int maximum)
        {
            int randomThreeDigitNumber = Randomizer(minimum, maximum);
            bool isSame = false;
            for (int i = 0; i < ClientCount; i++, isSame = false)
            {
                while (CVCCollector[i] == randomThreeDigitNumber.ToString())
                {
                    randomThreeDigitNumber = Randomizer(minimum, maximum);
                    isSame = true;
                }
                if (isSame)
                    i = 0;
            }
            return randomThreeDigitNumber;
        }
        private static bool IsSamePanExist(in string pan)
        {
            for (int i = 0; i < ClientCount; i++)
            {
                if (PANCollector[i].ToString() == pan)
                    return true;
            }
            return false;
        }
        private static void PANHelper(StringBuilder pan)
        {
            const int randomCount = 4;
            const int minimum = 1000;
            const int maximum = 10000;

            for (int i = 0; i < randomCount; i++)
            {
                if (i != 3)
                    pan.Append(Randomizer(minimum, maximum)).Append("_");
                else
                    pan.Append(Randomizer(minimum, maximum));
            }
        }

        static public string CreatePan()
        {
            StringBuilder pan = new StringBuilder();
            PANHelper(pan);

            while (IsSamePanExist(pan.ToString()))
            {
                pan.Clear();
                PANHelper(pan);
            }
            PANCollector[ClientCount] = pan;
            return pan.ToString();
        }
        static public string CreateCVC()
        {
            const int minimum = 100;
            const int maximum = 1000;
            StringBuilder cvc = new StringBuilder();

            int randomNumber = TakeNumberThatIsNotInTheCVCCollection(minimum, maximum);
            cvc.Append(randomNumber);
            CVCCollector[ClientCount] = cvc.ToString();

            ++ClientCount;
            return CVCCollector[ClientCount - 1];
        }
        static public string HidePin(string pin)
        {
            if (pin == null)
                return null;

            return new string('*', pin.Length);
        }

    }
}
