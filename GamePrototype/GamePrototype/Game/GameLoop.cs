using GamePrototype.Builders;
using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Factories;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        private Difficulty _difficulty;

        public void StartGame()
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");

            ChooseDifficulty();

            IUnitFactory unitFactory = _difficulty == Difficulty.Easy
                ? new EasyUnitFactory()
                : new HardUnitFactory();

            DungeonBuilder dungeonBuilder = _difficulty == Difficulty.Easy
                ? new EasyDungeonBuilder(unitFactory)
                : new HardDungeonBuilder(unitFactory);

            DungeonDirector director = new DungeonDirector(dungeonBuilder);
            _dungeon = director.ConstructDungeon();

            Console.WriteLine("Enter your name");
            _player = unitFactory.CreatePlayer(Console.ReadLine());
            Console.WriteLine($"Hello {_player.Name}");
            Console.WriteLine($"Difficulty: {_difficulty}");

            if (_player is Player player)
            {
                player.ShowEquipment();
            }
        }

        private void ChooseDifficulty()
        {
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1 - Easy");
            Console.WriteLine("2 - Hard");

            var input = Console.ReadLine();

            if (input == "1")
            {
                _difficulty = Difficulty.Easy;
                Console.WriteLine("Easy mode selected. You start with better equipment and weaker enemies.");
            }
            else
            {
                _difficulty = Difficulty.Hard;
                Console.WriteLine("Hard mode selected. You start with weaker equipment and stronger enemies.");
            }
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;

            while (currentRoom.IsFinal == false)
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success)
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true)
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction))
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong direction");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null)
            {
                _player.AddItemToInventory(currentRoom.Loot);
                Console.WriteLine($"Found: {currentRoom.Loot.Name}");
            }
            if (currentRoom.Enemy != null)
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                    HandlePlayerActions();
                }
                else
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void HandlePlayerActions()
        {
            if (_player is not Player player) return;

            bool hasGrindstone = false;
            foreach (var item in _player.GetInventoryItems())
            {
                if (item is Grindstone)
                {
                    hasGrindstone = true;
                    break;
                }
            }

            if (!hasGrindstone) return;

            var meleeWeapon = player.GetEquippedItem(EquipSlot.Weapon);
            if (meleeWeapon == null || meleeWeapon.IsBroken)
            {
                Console.WriteLine("\nYou have a grindstone, but no melee weapon (sword) to repair!");
                return;
            }

            Console.WriteLine("\nYou have a grindstone! Do you want to use it to repair your MELEE weapon (sword)? (y/n)");
            var input = Console.ReadLine();

            if (input?.ToLower() == "y")
            {
                bool result = player.UseGrindstone();
                if (result)
                {
                    Console.WriteLine("Sword repaired successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to use grindstone.");
                }
            }
            else if (input?.ToLower() == "n")
            {
                Console.WriteLine("Grindstone saved for later use.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.WriteLine($"{room.Key} - {(int)room.Key}\t");
            }
        }
        #endregion
    }
}