using System;

namespace class_for_unity_task
{
    public class Unit
    {
        private float _health;
        private Interval _damageRange;

        public string Name { get; private set; }
        public float Health => _health;
        public float Armor { get; private set; } = 0.6f;

        public float Damage => _damageRange.Get();

        public Unit()
        {
            Name = "Unknown Unit";
            _health = 100f;
            _damageRange = new Interval(0, 5);
        }

        public Unit(string name)
        {
            Name = name;
            _health = 100f;
            _damageRange = new Interval(0, 5);
        }

        public Unit(string name, float health)
        {
            Name = name;
            _health = health;
            _damageRange = new Interval(0, 5);
        }

        public Unit(string name, float health, int minDamage, int maxDamage)
        {
            Name = name;
            _health = health;
            _damageRange = new Interval(minDamage, maxDamage);
        }

        public float GetRealHealth()
        {
            return Health * (1f + Armor);
        }

        public bool SetDamage(float value)
        {
            _health -= value * Armor;

            if (_health <= 0f)
            {
                return true;
            }
            return false;
        }
    }
}