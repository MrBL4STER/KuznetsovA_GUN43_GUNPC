using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Factories
{
    public class EasyUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 40, 40, 8);

            var sword = new Weapon(15, 25, "Iron Sword");
            var armour = new Armour(15, 25, "Steel Armour");
            var helmet = new Helmet(12, 20, "Iron Helmet");
            var bow = new RangeWeapon(10, 12, 20, "Strong Bow");
            var healthPotion = new HealthPotion("Health Potion");
            var grindstone = new Grindstone("Grindstone", 35);

            player.AddItemToInventory(sword);
            player.AddItemToInventory(armour);
            player.AddItemToInventory(helmet);
            player.AddItemToInventory(bow);
            player.AddItemToInventory(healthPotion);
            player.AddItemToInventory(grindstone);

            return player;
        }

        public Unit CreateEnemy()
        {
            return new Goblin("Weak Goblin", 14, 14, 2);
        }
    }
}