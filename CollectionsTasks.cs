using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    internal class CollectionsTasks
    {
        // Задание 1: Список строк
        private class ListTask
        {
            public void TaskLoop()
            {
                var list = new List<string> { "Apple", "Banana", "Cherry" };

                Console.WriteLine("Начальный список: " + string.Join(", ", list));

                Console.Write("Введите строку для добавления в конец: ");
                list.Add(Console.ReadLine());
                Console.WriteLine("После добавления: " + string.Join(", ", list));

                Console.Write("Введите строку для вставки в середину: ");
                list.Insert(list.Count / 2, Console.ReadLine());
                Console.WriteLine("Конечный список: " + string.Join(", ", list));

                Console.WriteLine("Нажмите Enter...");
                Console.ReadLine();
            }
        }

        // Задание 2: Словарь студентов
        private class DictionaryTask
        {
            public void TaskLoop()
            {
                var students = new Dictionary<string, int>();

                while (true)
                {
                    Console.Write("Имя студента (или 'search' для поска студента): ");
                    string name = Console.ReadLine();
                    if (name == "search") break;

                    Console.Write("Оценка (2-5): ");
                    if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 2 && grade <= 5)
                    {
                        students[name] = grade;
                        Console.WriteLine($"Добавлен: {name} - {grade}");
                    }
                    else Console.WriteLine("Ошибка! Оценка от 2 до 5.");
                }

                while (true)
                {
                    Console.Write("\nВведите имя для поиска (или 'exit' для выхода из задания): ");
                    string search = Console.ReadLine();

                    if (search == "exit") break;

                    if (students.ContainsKey(search))
                        Console.WriteLine($"{search}: {students[search]}");
                    else
                        Console.WriteLine("Студент не найден");
                }

                Console.WriteLine("Выход из задания...");
            }
        }

        // Задание 3: Двусвязный список
        private class LinkedListTask
        {
            class Node
            {
                public string Data;
                public Node Next, Prev;
                public Node(string data) => Data = data;
            }

            public void TaskLoop()
            {
                Node head = null, tail = null;
                int count;

                do Console.Write("Кол-во элементов (3-6): ");
                while (!int.TryParse(Console.ReadLine(), out count) || count < 3 || count > 6);

                for (int i = 0; i < count; i++)
                {
                    Console.Write($"Элемент {i + 1}: ");
                    var node = new Node(Console.ReadLine());
                    if (head == null) head = tail = node;
                    else { tail.Next = node; node.Prev = tail; tail = node; }
                }

                Console.Write("Прямой: ");
                for (var n = head; n != null; n = n.Next)
                    Console.Write(n.Data + (n.Next != null ? " <-> " : "\n"));

                // Обратный порядок
                Console.Write("Обратный: ");
                for (var n = tail; n != null; n = n.Prev)
                    Console.Write(n.Data + (n.Prev != null ? " <-> " : "\n"));

                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Список | 2 - Словарь | 3 - Двусвязный список | 0 - Выход");
                Console.Write("Выбор: ");

                switch (Console.ReadLine())
                {
                    case "1": new ListTask().TaskLoop(); break;
                    case "2": new DictionaryTask().TaskLoop(); break;
                    case "3": new LinkedListTask().TaskLoop(); break;
                    case "0": return;
                    default: Console.WriteLine("Ошибка!"); Console.ReadLine(); break;
                }
            }
        }
    }
}