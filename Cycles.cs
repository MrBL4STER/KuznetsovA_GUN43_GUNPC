using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Задание 1
            int[] array = new int[10];
            array[0] = 0;
            array[1] = 1;
            for (int i = 2; i < array.Length; i++)
            {
                array[i] = array[i - 1] + array[i - 2];
            }
            Console.WriteLine("Задание 1 - Числа Фибоначчи:");
            Console.WriteLine(string.Join(", ", array) + '\n');
            
            
            //Задание 2
            int[] array_even = new int[10];
            array_even[0] = 2;
            for(int index = 1; index < array_even.Length; index++)
            {
                array_even[index] = array_even[index - 1] + 2;
            }
            Console.WriteLine("Задание 2 - Все чётные числа от 2 до 20:");
            Console.WriteLine(string.Join(", ", array_even) + '\n');
            
            //Задание 3
            int[] array_multipliers = new int[5] {1, 2, 3, 4, 5};
            int[] numbers = new int[11] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            Console.WriteLine("Задание 3 - Таблица умножения от 1 до 5:");
            
            for (int i_mult = 0; i_mult < array_multipliers.Length; i_mult++)
            {
                Console.WriteLine($"Таблица умножения для {array_multipliers[i_mult]}:");
                for (int i_num = 0; i_num < numbers.Length; i_num++)
                {
                    int result = array_multipliers[i_mult] * numbers[i_num];
                    Console.WriteLine($"{array_multipliers[i_mult]} * {numbers[i_num]} = {result}" + '\n');
                }
            }
            
            //Задание 4
            string password = "qwerty";
            string userInput;
            Console.WriteLine("Задание 4 - Программа для ввода пароля:");
            do
            {
                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();
                
                if (userInput != password)
                {
                    Console.WriteLine("Неверный пароль! Попробуйте снова.");
                }
            } 
            while (userInput != password);
            
            Console.WriteLine("Доступ разрешён!");
        }
    }
}
