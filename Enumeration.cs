using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goldbach
{
    internal class Enumeration
    {
        public string GoldbachConjecture(int number)
        {

            for (int i = 2; i < number; i++)
            {
                if (IsPrime(i) && IsPrime(number - i))
                {
                    return $"{number} = {i} + {number - i}";
                }
            }

            return "Нет разложения на сумму двух простых чисел";
        }

        private bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
