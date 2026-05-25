using System;

namespace class_for_unity_task
{
    public struct Room
    {
        public Unit Unit { get; private set; }
        public Weapon Weapon { get; private set; }

        public Room(Unit unit, Weapon weapon)
        {
            Unit = unit;
            Weapon = weapon;
        }
    }
}