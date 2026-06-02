using GamePrototype.Units;

namespace GamePrototype.Factories
{
    public interface IUnitFactory
    {
        Unit CreatePlayer(string name);
        Unit CreateEnemy();
    }
}