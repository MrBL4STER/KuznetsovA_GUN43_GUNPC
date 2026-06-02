using GamePrototype.Builders;
using GamePrototype.Dungeon;

namespace GamePrototype.Utils
{
    public class DungeonDirector
    {
        private DungeonBuilder _builder;

        public DungeonDirector(DungeonBuilder builder)
        {
            _builder = builder;
        }

        public DungeonRoom ConstructDungeon()
        {
            _builder.ConnectRooms();
            return _builder.GetDungeon();
        }
    }
}