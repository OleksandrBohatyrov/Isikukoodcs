using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isikukood
{
    public class IdCode
    {
        private string _idCode;

        public IdCode(string idCode)
        {
            _idCode = idCode;
        }



        public static bool IsValidIdCode(string idCode)
        {
            if (idCode.Length != 11)
                return false;

            foreach (char c in idCode)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            int controlNumber = int.Parse(idCode[10].ToString());
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int total = CalculateControlNumberWithWeights(idCode, weights);

            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }

            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            total = CalculateControlNumberWithWeights(idCode, weights2);

            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }

            return controlNumber == 0;
        }

        private static int CalculateControlNumberWithWeights(string idCode, int[] weights)
        {
            int total = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                total += int.Parse(idCode[i].ToString()) * weights[i];
            }

            return total;
        }

        private int GetGenderNumber()
        {
            return Convert.ToInt32(_idCode.Substring(0, 1));
        }

        private bool IsValidGenderNumber()
        {
            int genderNumber = GetGenderNumber();
            return genderNumber > 0 && genderNumber < 7;
        }

        private int Get2DigitYear()
        {
            return Convert.ToInt32(_idCode.Substring(1, 2));
        }

        public int GetFullYear()
        {
            int genderNumber = GetGenderNumber();

            return 1800 + (genderNumber - 1) / 2 * 100 + Get2DigitYear();
        }

        private int GetMonth()
        {
            return Convert.ToInt32(_idCode.Substring(3, 2));
        }

        private bool IsValidMonth()
        {
            int month = GetMonth();
            return month > 0 && month < 13;
        }

        private static bool IsLeapYear(int year)
        {
            return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        }

        private int GetDay()
        {
            return Convert.ToInt32(_idCode.Substring(5, 2));
        }

        private bool IsValidDay()
        {
            int day = GetDay();
            int month = GetMonth();
            int maxDays = 31;
            if (new List<int> { 4, 6, 9, 11 }.Contains(month))
            {
                maxDays = 30;
            }
            if (month == 2)
            {
                if (IsLeapYear(GetFullYear()))
                {
                    maxDays = 29;
                }
                else
                {
                    maxDays = 28;
                }
            }
            return 0 < day && day <= maxDays;
        }

        private int CalculateControlNumberWithWeights(int[] weights)
        {
            int total = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                total += Convert.ToInt32(_idCode.Substring(i, 1)) * weights[i];
            }
            return total;
        }

        private bool IsValidControlNumber()
        {
            int controlNumber = Convert.ToInt32(_idCode[^1..]);
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int total = CalculateControlNumberWithWeights(weights);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            total = CalculateControlNumberWithWeights(weights2);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            return controlNumber == 0;
        }

        public bool IsValid()
        {
            return IsValidLength() && ContainsOnlyNumbers()
                && IsValidGenderNumber() && IsValidMonth()
                && IsValidDay()
                && IsValidControlNumber();
        }

        public DateOnly GetBirthDate()
        {
            int month = GetMonth();
            int day = GetDay();
 
            int year = GetFullYear();
            return new DateOnly(year, day, month);
        }

        public string GetGender()
        {
            int genderNumber = GetGenderNumber();
            return genderNumber % 2 == 0 ? "Naised" : "Mehed";
        }

        private bool IsValidLength()
        {
            return _idCode.Length == 11;
        }

        private bool ContainsOnlyNumbers()
        {
            for (int i = 0; i < _idCode.Length; i++)
            {
                if (!Char.IsDigit(_idCode[i]))
                {
                    return false;
                }
            }
            return true;
        }


        public void GenerateIsic()
        {
            Random random = new Random();


            //Генерация гендера
            int gender = random.Next(1, 7);


            int year = random.Next(1800, DateTime.Now.Year + 1);
            int lastTwoDigitsOfYear = year % 100;
            string yearString = lastTwoDigitsOfYear.ToString("00");


            int month = random.Next(1, 13);
            string monthString = month.ToString("00");

            // Генерация дня (шестая и седьмая цифры)
            int maxDaysInMonth;
            if (month == 2 && IsLeapYear(year))
            {
                maxDaysInMonth = 29; // Высокосный год
            }
            else
            {
                maxDaysInMonth = DateTime.DaysInMonth(year, month);
            }
            int day = random.Next(1, maxDaysInMonth + 1);
            string dayString = day.ToString("00");


            // Генерируем 4 случайных числа (4 цифры)
            string randomDigits = random.Next(1000, 10000).ToString();

            // Собираем итоговый Isikukood
            _idCode = $"{gender}{yearString}{dayString}{monthString}{randomDigits}";
            Console.WriteLine(_idCode);

        }
    }

}
 