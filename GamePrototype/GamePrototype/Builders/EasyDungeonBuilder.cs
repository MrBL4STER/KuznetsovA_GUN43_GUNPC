using GamePrototype.Dungeon;
using GamePrototype.Factories;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;

namespace GamePrototype.Builders
{
    public class EasyDungeonBuilder : DungeonBuilder
    {
        private IUnitFactory _unitFactory;

        public EasyDungeonBuilder(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public override void BuildEnterRoom()
        {
            _dungeon = new DungeonRoom("Enter Room - Easy");
        }

        public override void BuildMonsterRoom()
        {
        }

        public override void BuildLootRoom()
        {
        }

        public override void BuildFinalRoom()
        {
        }

        public override void ConnectRooms()
        {
            var enter = new DungeonRoom("Enter Room");
            var monsterRoom = new DungeonRoom("Goblin Camp", _unitFactory.CreateEnemy());
            var lootRoom = new DungeonRoom("Treasure Room", new Gold());
            var grindstoneRoom = new DungeonRoom("Grindstone Room", new Grindstone("Grindstone", 40));
            var finalRoom = new DungeonRoom("Final Chamber", new Gold());

            enter.TrySetDirection(Direction.Forward, monsterRoom);
            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, grindstoneRoom);
            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            grindstoneRoom.TrySetDirection(Direction.Forward, finalRoom);

            _dungeon = enter;
        }
    }
}