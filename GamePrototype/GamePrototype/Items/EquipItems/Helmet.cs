using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Helmet : EquipItem
    {
        public uint Defence { get; private set; }
        public override EquipSlot Slot => EquipSlot.Helmet;

        public Helmet(uint defence, uint durability, string name) : base(durability, name)
        {
            Defence = defence;
        }
    }
}