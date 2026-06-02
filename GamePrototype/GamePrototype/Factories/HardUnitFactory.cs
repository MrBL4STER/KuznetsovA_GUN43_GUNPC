using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Factories
{
    public class HardUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 25, 25, 5);

            var sword = new Weapon(8, 12, "Rusty Sword");
            var armour = new Armour(8, 15, "Worn Armour");
            var healthPotion = new HealthPotion("Health Potion");

            player.AddItemToInventory(sword);
            player.AddItemToInventory(armour);
            player.AddItemToInventory(healthPotion);

            return player;
        }

        public Unit CreateEnemy()
        {
            return new Goblin("Strong Goblin", 28, 28, 6);
        }
    }
}