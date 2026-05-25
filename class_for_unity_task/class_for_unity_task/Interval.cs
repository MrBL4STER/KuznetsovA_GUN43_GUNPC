using System;

namespace class_for_unity_task
{
    public struct Interval
    {
        private static readonly Random _random = new Random();

        public float Min { get; private set; }
        public float Max { get; private set; }

        public Interval(int minValue, int maxValue)
        {
            int tempMin = minValue;
            int tempMax = maxValue;

            if (tempMin > tempMax)
            {
                Console.WriteLine($"Ошибка: minValue ({tempMin}) больше maxValue ({tempMax}). Числа swapped.");
                int temp = tempMin;
                tempMin = tempMax;
                tempMax = temp;
            }

            if (tempMin < 0)
            {
                Console.WriteLine($"Ошибка: minValue ({tempMin}) меньше 0. Значение изменено на 0.");
                tempMin = 0;
            }
            if (tempMax < 0)
            {
                Console.WriteLine($"Ошибка: maxValue ({tempMax}) меньше 0. Значение изменено на 0.");
                tempMax = 0;
            }

            if (tempMin == tempMax)
            {
                Console.WriteLine($"Ошибка: minValue ({tempMin}) равен maxValue ({tempMax}). Максимальное значение увеличено на 10.");
                tempMax += 10;
            }

            Min = tempMin;
            Max = tempMax;
        }

        public float Get()
        {
            return Min + (float)_random.NextDouble() * (Max - Min);
        }
    }
}