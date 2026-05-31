using GamePrototype.Units;

namespace GamePrototype.Combat
{
    public sealed class CombatManager
    {
        private readonly Random _random = new();

        public Unit StartCombat(Unit player, Unit enemy)
        {
            if (player is Player playerInstance)
            {
                ChooseWeapon(playerInstance);
            }
            return PlayCombatRoutine(player, enemy);
        }

        private void ChooseWeapon(Player player)
        {
            Console.WriteLine("\nPREPARE FOR BATTLE");
            Console.WriteLine($"Current weapon: {player.GetActiveWeaponName()} ({player.GetActiveWeaponType()})");
            Console.WriteLine("\nChoose your weapon:");
            Console.WriteLine("1 - Melee weapon (Sword)");
            Console.WriteLine("2 - Ranged weapon (Bow)");
            Console.WriteLine("3 - Switch weapon");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (player.GetActiveWeaponType() != "Melee")
                    {
                        player.SwitchWeapon();
                    }
                    Console.WriteLine($"Fighting with: {player.GetActiveWeaponName()} (Melee)");
                    break;
                case "2":
                    if (player.GetActiveWeaponType() != "Ranged")
                    {
                        player.SwitchWeapon();
                    }
                    Console.WriteLine($"Fighting with: {player.GetActiveWeaponName()} (Ranged)");
                    break;
                case "3":
                    player.SwitchWeapon();
                    Console.WriteLine($"Now fighting with: {player.GetActiveWeaponName()}");
                    break;
                default:
                    Console.WriteLine($"Using current weapon: {player.GetActiveWeaponName()}");
                    break;
            }
            Console.WriteLine();
        }

        private Unit PlayCombatRoutine(Unit player, Unit enemy)
        {
            Console.WriteLine(GetCombatString());
            while (player.Health > 0 && enemy.Health > 0)
            {
                if (Enum.TryParse<RockPaperScissors>(Console.ReadLine(), out var rockPaperScissors))
                {
                    HandleCombatInput(player, enemy, rockPaperScissors);
                }
                else
                {
                    Console.WriteLine(GetCombatString());
                }
            }
            if (player.Health > 0 && enemy.Health == 0)
            {
                return player;
            }
            else if (player.Health == 0 && enemy.Health > 0)
            {
                return enemy;
            }

            return null;
        }

        private string GetCombatString() => $"Type {RockPaperScissors.Rock} = {(int)RockPaperScissors.Rock}" +
            $" or {RockPaperScissors.Paper} = {(int)RockPaperScissors.Paper} " +
            $" or {RockPaperScissors.Scissors} = {(int)RockPaperScissors.Scissors}";

        private void HandleCombatInput(Unit player, Unit enemy, RockPaperScissors rockPaperScissors)
        {
            var enemyInput = (RockPaperScissors)_random.Next(1, 3);
            Console.WriteLine($"Result player = {rockPaperScissors} and enemy = {enemyInput}");
            switch (rockPaperScissors)
            {
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Scissors:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(enemy, player);
                    break;
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Scissors:
                    ApplyDamage(enemy, player);
                    break;
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(enemy, player);
                    break;
                default:
                    Console.WriteLine("Combatants tried to hit, but missed :(");
                    break;
            }
        }

        private void ApplyDamage(Unit attacker, Unit defender)
        {
            defender.ApplyDamage(attacker.GetUnitDamage());
            Console.WriteLine($"{attacker.Name} hits {defender.Name}. {defender.Name} health {defender.Health}/{defender.MaxHealth}");

            if (attacker is Player player)
            {
                player.ReduceActiveWeaponDurability();
            }

            if (defender.Health == 0)
            {
                Console.WriteLine($"{defender.Name} is dead!");
            }
        }
    }
}