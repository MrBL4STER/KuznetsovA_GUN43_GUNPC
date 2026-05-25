using System;

namespace class_for_unity_task
{
    public class Weapon
    {
        public string Name { get; private set; }

        public Interval DamageRange { get; private set; }

        public float Durability { get; private set; } = 1f;

        public Weapon(string name)
        {
            Name = name;
            DamageRange = new Interval(1, 10);
        }

        public Weapon(string name, int minDamage, int maxDamage)
        {
            Name = name;
            DamageRange = new Interval(minDamage, maxDamage);
        }

        public float GetDamage()
        {
            return (DamageRange.Min + DamageRange.Max) / 2;
        }
    }
}