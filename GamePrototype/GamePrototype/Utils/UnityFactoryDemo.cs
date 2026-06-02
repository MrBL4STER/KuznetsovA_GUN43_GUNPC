using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class UnityFactoryDemo
    {
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);

            var sword = new Weapon(10, 15, "Iron Sword");
            var armour = new Armour(15, 20, "Leather Armour");
            var helmet = new Helmet(10, 15, "Iron Helmet");
            var bow = new RangeWeapon(8, 10, 12, "Wooden Bow");
            var grindstone = new Grindstone("Grindstone", 20);
            var healthPotion = new HealthPotion("Health Potion");

            player.AddItemToInventory(sword);
            player.AddItemToInventory(armour);
            player.AddItemToInventory(helmet);
            player.AddItemToInventory(bow);
            player.AddItemToInventory(grindstone);
            player.AddItemToInventory(healthPotion);

            return player;
        }

        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);
    }
}