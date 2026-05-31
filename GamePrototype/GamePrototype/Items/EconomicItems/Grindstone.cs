using System;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        public uint RepairAmount { get; private set; }

        public override bool Stackable => true;

        public Grindstone(string name, uint repairAmount = 25) : base(name)
        {
            RepairAmount = repairAmount;
        }

        public bool UseOnWeapon(EquipItem weapon)
        {
            if (weapon == null)
            {
                Console.WriteLine("No weapon specified!");
                return false;
            }

            if (weapon.Slot != EquipSlot.Weapon && weapon.Slot != EquipSlot.RangeWeapon)
            {
                Console.WriteLine($"Grindstone can only be used on weapons! {weapon.Name} is a {weapon.Slot}.");
                return false;
            }

            if (weapon.IsBroken)
            {
                Console.WriteLine($"Cannot sharpen {weapon.Name} - it's broken!");
                return false;
            }

            uint oldDurability = weapon.Durability;
            weapon.Repair(RepairAmount);

            Console.WriteLine($"Used Grindstone on {weapon.Name}. Durability: {oldDurability} -> {weapon.Durability}");
            return true;
        }
    }
}