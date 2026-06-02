using GamePrototype.Dungeon;

namespace GamePrototype.Builders
{
    public abstract class DungeonBuilder
    {
        protected DungeonRoom _dungeon;

        public DungeonRoom GetDungeon() => _dungeon;

        public abstract void BuildEnterRoom();
        public abstract void BuildMonsterRoom();
        public abstract void BuildLootRoom();
        public abstract void BuildFinalRoom();
        public abstract void ConnectRooms();
    }
}