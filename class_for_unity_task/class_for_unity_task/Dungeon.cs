using System;

namespace class_for_unity_task
{
    public class Dungeon
    {
        private Room[] _rooms;

        public Dungeon()
        {
            _rooms = new Room[]
            {
                new Room(new Unit("Гоблин", 50, 5, 15), new Weapon("Кинжал гоблина", 5, 15)),
                new Room(new Unit("Орк-берсерк", 120, 15, 30), new Weapon("Двуручный топор", 15, 30)),
                new Room(new Unit("Скелет-лучник", 70, 8, 20), new Weapon("Лук скелета", 8, 20))
            };
        }

        public void ShowRooms()
        {
            for (int i = 0; i < _rooms.Length; i++)
            {
                var room = _rooms[i];
                Console.WriteLine($"Unit of room {i + 1}: {room.Unit.Name}");
                Console.WriteLine($"Weapon of room {i + 1}: {room.Weapon.Name}");
                Console.WriteLine("---");
            }
        }
    }
}