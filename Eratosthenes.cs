using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;











































namespace goldbach
{
    internal class Eratosthenes
    {
        public async Task GoldbachConjectureAsync(int start, int end, RichTextBox outputBox, ProgressBar progressBar)
        {
            // Создаем список для хранения простых чисел
            List<int> primes = SieveOfEratosthenes(end);
            if (!primes.Contains(3)) primes.Insert(0, 1);
            if (!primes.Contains(1)) primes.Insert(0, 1);
            // Проверяем гипотезу Гольдбаха для четных чисел в заданном диапазоне
            for (int i = start; i <= end + 1; i += 2)
            {
                bool isGoldbach = false;

                for (int j = 0; j < primes.Count && primes[j] < i / 2; j++)
                {
                    int difference = i - primes[j];
                    if (primes.BinarySearch(difference) >= 0)
                    {
                        outputBox.AppendText(i + " = " + primes[j] + " + " + difference + Environment.NewLine);
                        isGoldbach = true;
                        break;
                    }
                }

                if (!isGoldbach)
                {
                    outputBox.AppendText(i + " = Нет разложения на сумму двух простых чисел" + Environment.NewLine);
                }

                // Обновляем значение ProgressBar
                progressBar.Value = (int)((i - start) / (double)(end - start) * 100);

                // Делаем паузу, чтобы пользователь мог увидеть процесс
                await Task.Delay(10);
            }
            progressBar.Value = 0;
        }

        private List<int> SieveOfEratosthenes(int n)
        {
            List<int> primes = new List<int>();
            bool[] isComposite = new bool[n + 1];

            for (int i = 2; i <= n; i++)
            {
                if (!isComposite[i])
                {
                    primes.Add(i);

                    for (int j = i * i; j <= n; j += i)
                    {
                        isComposite[j] = true;
                    }
                }
            }

            return primes;
        }
    }
}
