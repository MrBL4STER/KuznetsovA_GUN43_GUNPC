using GamePrototype.Dungeon;
using GamePrototype.Factories;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Builders
{
    public class HardDungeonBuilder : DungeonBuilder
    {
        private IUnitFactory _unitFactory;

        public HardDungeonBuilder(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public override void BuildEnterRoom()
        {
            _dungeon = new DungeonRoom("Dark Entrance - Hard");
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
            var enter = new DungeonRoom("Dark Entrance");
            var monsterRoom1 = new DungeonRoom("Orc Outpost", _unitFactory.CreateEnemy());
            var monsterRoom2 = new DungeonRoom("Dark Cave", _unitFactory.CreateEnemy());
            var monsterRoom3 = new DungeonRoom("Troll Bridge", _unitFactory.CreateEnemy());
            var lootRoom = new DungeonRoom("Small Chest", new Gold());
            var finalRoom = new DungeonRoom("Boss Chamber", _unitFactory.CreateEnemy());

            enter.TrySetDirection(Direction.Forward, monsterRoom1);
            monsterRoom1.TrySetDirection(Direction.Forward, monsterRoom2);
            monsterRoom1.TrySetDirection(Direction.Right, lootRoom);
            monsterRoom2.TrySetDirection(Direction.Forward, monsterRoom3);
            monsterRoom2.TrySetDirection(Direction.Left, finalRoom);
            monsterRoom3.TrySetDirection(Direction.Forward, finalRoom);
            lootRoom.TrySetDirection(Direction.Forward, finalRoom);

            _dungeon = enter;
        }
    }
}