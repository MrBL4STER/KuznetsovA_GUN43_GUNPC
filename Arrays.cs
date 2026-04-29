using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Задание 1
            int[] array = new int[8];
            array[0] = 0;
            array[1] = 1;
            for (int i = 2; i < array.Length; i++)
            {
                array[i] = array[i - 1] + array[i - 2];
            }
            Console.WriteLine("Задание 1 - Числа Фибоначчи:");
            Console.WriteLine(string.Join(", ", array) + '\n');
            
            //Задание 2
            string[] months = { "January", "February", "March", "April", "May","June", "July", "August", "September", "October", "November", "December" };
            Console.WriteLine("Задание 2 - Месяцы:");
            Console.WriteLine(string.Join(", ", months) + '\n');

            //Задание 3
            int[,] matrix = new int[3,3];
            for (int i = 0; i < 3; i++)
            {
                int power = i + 1;
                matrix[i, 0] = (int)Math.Pow(2, power);
                matrix[i, 1] = (int)Math.Pow(3, power);
                matrix[i, 2] = (int)Math.Pow(4, power);
            }
            
            Console.WriteLine("Задание 3 - Матрица 3x3 (степени чисел 2,3,4):");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{matrix[i, j],8}");
                }
                Console.WriteLine();
             }
            Console.WriteLine();

            //Задание 4
            double[][] jaggedArray = new double[3][];
            jaggedArray[0] = new double[] {1, 2, 3, 4, 5 };
            jaggedArray[1] = new double[] { Math.E, Math.PI };
            jaggedArray[2] = new double[] { Math.Log10(1), Math.Log10(10), Math.Log10(100), Math.Log10(1000) };
            Console.WriteLine("Задание 4 - Jagged array:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.WriteLine($"Массив {i + 1}: {string.Join(", ", jaggedArray[i])}");
            }
            Console.WriteLine();

            // Массивы для заданий 5 и 6
            int[] array1 = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };
            
            // Задание 5
            Array.Copy(array1, 0, array2, 0, 3);
            Console.WriteLine("Задание 5 - После копирования первых 3 элементов:");
            Console.WriteLine($"array2: {string.Join(", ", array2)} " + '\n');

            // Задание 6
            int newSize = array1.Length * 2;
            Array.Resize(ref array1, newSize);
            Console.WriteLine("Задание 6 - После изменения размера:");
            Console.WriteLine($"Новый размер array1: {array1.Length}");
            Console.WriteLine($"array1: {string.Join(", ", array1)}");
        }
    }
}