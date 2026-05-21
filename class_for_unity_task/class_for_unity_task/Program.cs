using System;

namespace class_for_unity_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Подготовка к бою");

            Console.Write("Введите имя бойца: ");
            string name = Console.ReadLine();

            int health;
            while (true)
            {
                Console.Write("Введите начальное здоровье бойца (10-100): ");
                if (int.TryParse(Console.ReadLine(), out health) && health >= 10 && health <= 100)
                    break;
                Console.WriteLine("Ошибка! Введите целое число от 10 до 100.");
            }

            float helmArmor;
            while (true)
            {
                Console.Write("Введите значение брони шлема от 0 до 1: ");
                if (float.TryParse(Console.ReadLine(), out helmArmor) && helmArmor >= 0 && helmArmor <= 1)
                    break;
                Console.WriteLine("Ошибка! Введите число от 0 до 1.");
            }

            float shellArmor;
            while (true)
            {
                Console.Write("Введите значение брони кирасы от 0 до 1: ");
                if (float.TryParse(Console.ReadLine(), out shellArmor) && shellArmor >= 0 && shellArmor <= 1)
                    break;
                Console.WriteLine("Ошибка! Введите число от 0 до 1.");
            }

            float bootsArmor;
            while (true)
            {
                Console.Write("Введите значение брони сапог от 0 до 1: ");
                if (float.TryParse(Console.ReadLine(), out bootsArmor) && bootsArmor >= 0 && bootsArmor <= 1)
                    break;
                Console.WriteLine("Ошибка! Введите число от 0 до 1.");
            }

            int minDamage;
            while (true)
            {
                Console.Write("Укажите минимальный урон оружия (0-20): ");
                if (int.TryParse(Console.ReadLine(), out minDamage) && minDamage >= 0 && minDamage <= 20)
                    break;
                Console.WriteLine("Ошибка! Введите целое число от 0 до 20.");
            }

            int maxDamage;
            while (true)
            {
                Console.Write("Укажите максимальный урон оружия (20-40): ");
                if (int.TryParse(Console.ReadLine(), out maxDamage) && maxDamage >= 20 && maxDamage <= 40)
                    break;
                Console.WriteLine("Ошибка! Введите целое число от 20 до 40.");
            }

            Helm helm = new Helm(helmArmor);
            Shell shell = new Shell(shellArmor);
            Boots boots = new Boots(bootsArmor);
            Weapon weapon = new Weapon("Меч игрока", minDamage, maxDamage);

            Unit player = new Unit(name, health);

            float totalArmor = helm.Armor + shell.Armor + boots.Armor;
            totalArmor = (float)Math.Round(totalArmor, 2);

            float realHealth = health * (1f + totalArmor);
            realHealth = (float)Math.Round(realHealth, 2);

            Console.WriteLine($"\nОбщий показатель брони равен: {totalArmor}");
            Console.WriteLine($"Фактическое значение здоровья равно: {realHealth}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}