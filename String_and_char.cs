using System;
using System.Text;

namespace StringAndChar
{
    internal class String_and_char
    {
        // Задание 1: Конкатенация двух строк
        public static string ConcatenateStrings(string str1, string str2)
        {
            return str1 + str2;
        }

        // Задание 2: Приветствие пользователя с возрастом
        public static string GreetUser(string name, int age)
        {
            return $"Hello, {name}!\nYou are {age} years old.\n";
        }

        // Задание 3: Анализ строки (длина, верхний регистр, нижний регистр)
        public static string AnalyzeString(string input)
        {
            int length = input.Length;
            string upperCase = input.ToUpper();
            string lowerCase = input.ToLower();

            return $"Количество символов: {length}\n" +
                   $"Строка в верхнем регистре: {upperCase}\n" +
                   $"Строка в нижнем регистре: {lowerCase}";
        }

        // Задание 4: Первые 5 символов строки
        public static string GetFirstFiveCharacters(string input)
        {
            return input.Substring(0, 5);
        }

        // Задание 5: Объединение массива строк через пробел с использованием StringBuilder
        public static StringBuilder JoinStringsWithSpace(string[] words)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                result.Append(words[i]);
                if (i < words.Length - 1)
                {
                    result.Append(" ");
                }
            }

            return result;
        }

        // Задание 6: Замена слов в строке
        public static string ReplaceWords(string inputString, string wordToReplace, string replacementWord)
        {
            return inputString.Replace(wordToReplace, replacementWord);
        }

        static void Main(string[] args)
        {
            // Проверка Задания 1
            Console.WriteLine("=== Задание 1: Конкатенация строк ===");
            string concatResult = ConcatenateStrings("Hello, ", "World!");
            Console.WriteLine($"Результат конкатенации: {concatResult}");
            Console.WriteLine();

            // Проверка Задания 2
            Console.WriteLine("=== Задание 2: Приветствие пользователя ===");
            string greeting = GreetUser("Анна", 25);
            Console.Write(greeting);
            Console.WriteLine();

            // Проверка Задания 3
            Console.WriteLine("=== Задание 3: Анализ строки ===");
            Console.WriteLine("Строка: \"C# Programming\"");
            string analysis = AnalyzeString("C# Programming");
            Console.WriteLine(analysis);
            Console.WriteLine();

            // Проверка Задания 4
            Console.WriteLine("=== Задание 4: Первые 5 символов ===");
            string firstFive = GetFirstFiveCharacters("HelloWorld");
            Console.WriteLine($"'HelloWorld' -> '{firstFive}'");
            Console.WriteLine();

            // Проверка Задания 5
            Console.WriteLine("=== Задание 5: Объединение массива строк ===");
            string[] words = { "Это", "пример", "предложения", "из", "массива" };
            StringBuilder joinedString = JoinStringsWithSpace(words);
            Console.WriteLine($"Результат: {joinedString.ToString()}");
            Console.WriteLine();

            // Проверка Задания 6
            Console.WriteLine("=== Задание 6: Замена слов ===");
            string replaceResult1 = ReplaceWords("Hello world", "world", "universe");
            string replaceResult2 = ReplaceWords("C# is great. C# is powerful.", "C#", "Python");
            Console.WriteLine($"Замена 'world' на 'universe': {replaceResult1}");
            Console.WriteLine($"Замена 'C#' на 'Python': {replaceResult2}");
        }
    }
}