using System;

namespace class_for_unity_task
{
    public class Weapon
    {
        public string Name { get; }
        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public float Durability { get; } = 1f;

        public Weapon(string name)
        {
            Name = name;
        }

        public Weapon(string name, int minDamage, int maxDamage) : this(name)
        {
            SetDamageParams(minDamage, maxDamage);
        }

        public void SetDamageParams(int minDamage, int maxDamage)
        {
            if (minDamage > maxDamage)
            {
                Console.WriteLine($"Ошибка: для оружия '{Name}' minDamage ({minDamage}) больше maxDamage ({maxDamage}). Числа swapped.");
                int temp = minDamage;
                minDamage = maxDamage;
                maxDamage = temp;
            }

            if (minDamage < 1)
            {
                Console.WriteLine($"Предупреждение: для оружия '{Name}' minDamage ({minDamage}) меньше 1. Принудительно установлено значение 1.");
                minDamage = 1;
            }

            if (maxDamage <= 1)
            {
                Console.WriteLine($"Предупреждение: для оружия '{Name}' maxDamage ({maxDamage}) меньше или равен 1. Принудительно установлено значение 10.");
                maxDamage = 10;
            }

            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public int GetDamage()
        {
            return (MinDamage + MaxDamage) / 2;
        }
    }
}