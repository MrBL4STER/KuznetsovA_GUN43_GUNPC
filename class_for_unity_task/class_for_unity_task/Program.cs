using System;

namespace class_for_unity_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Подготовка к бою");

            Dungeon dungeon = new Dungeon();

            dungeon.ShowRooms();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}