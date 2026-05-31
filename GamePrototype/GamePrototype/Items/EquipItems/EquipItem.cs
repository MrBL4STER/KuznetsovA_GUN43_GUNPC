using System;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public abstract class EquipItem : Item
    {
        private uint _durability;
        private uint _maxDurability;

        public uint Durability
        {
            get => _durability;
            protected set => _durability = Math.Min(value, _maxDurability);
        }

        public override bool Stackable => false;
        public abstract EquipSlot Slot { get; }
        public bool IsBroken => _durability == 0;

        protected EquipItem(uint maxDurability, string name) : base(name)
        {
            _maxDurability = maxDurability;
            _durability = maxDurability;
        }

        public void ReduceDurability(uint delta)
        {
            if (delta >= _durability)
                _durability = 0;
            else
                _durability -= delta;
        }

        public void Repair(uint delta)
        {
            _durability = Math.Min(_durability + delta, _maxDurability);
        }
    }
}