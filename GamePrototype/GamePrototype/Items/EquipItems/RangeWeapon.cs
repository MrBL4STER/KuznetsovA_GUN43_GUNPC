using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class RangeWeapon : EquipItem
    {
        public uint Damage { get; private set; }
        public uint Range { get; private set; }
        public override EquipSlot Slot => EquipSlot.RangeWeapon;

        public RangeWeapon(uint damage, uint range, uint durability, string name) : base(durability, name)
        {
            Damage = damage;
            Range = range;
        }
    }
}