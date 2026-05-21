using System;

namespace class_for_unity_task
{
    public class Unit
    {
        private float _health;

        public string Name { get; }
        public float Health => _health;
        public int Damage { get; } = 5;
        public float Armor { get; } = 0.6f;

        public Unit() : this("Unknown Unit")
        {
        }

        public Unit(string name)
        {
            Name = name;
            _health = 100f;
        }

        public Unit(string name, float health) : this(name)
        {
            _health = health;
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