using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;

namespace GamePrototype.Utils
{
    public static class DungeonBuilder
    {
        public static DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", UnityFactoryDemo.CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootGrindstoneRoom = new DungeonRoom("Grindstone Room", new Grindstone("Grindstone", 25));
            var lootHelmetRoom = new DungeonRoom("Helmet Room", new Helmet(12, 20, "Steel Helmet"));
            var finalRoom = new DungeonRoom("Final", new Gold());

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootGrindstoneRoom);
            emptyRoom.TrySetDirection(Direction.Right, lootHelmetRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootGrindstoneRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootHelmetRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
}