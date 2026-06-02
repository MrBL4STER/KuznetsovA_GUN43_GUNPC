using System.Linq;
using GamePrototype.Items;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {

        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();
        private EquipSlot _activeWeaponSlot = EquipSlot.Weapon;

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public void SwitchWeapon()
        {
            bool hasMelee = _equipment.ContainsKey(EquipSlot.Weapon) && !_equipment[EquipSlot.Weapon].IsBroken;
            bool hasRanged = _equipment.ContainsKey(EquipSlot.RangeWeapon) && !_equipment[EquipSlot.RangeWeapon].IsBroken;

            if (hasMelee && hasRanged)
            {
                _activeWeaponSlot = (_activeWeaponSlot == EquipSlot.Weapon) ? EquipSlot.RangeWeapon : EquipSlot.Weapon;
                string weaponName = _equipment[_activeWeaponSlot].Name;
                Console.WriteLine($"Switched to {weaponName}!");
            }
            else if (hasMelee)
            {
                _activeWeaponSlot = EquipSlot.Weapon;
                Console.WriteLine($"Only melee weapon available: {_equipment[EquipSlot.Weapon].Name}");
            }
            else if (hasRanged)
            {
                _activeWeaponSlot = EquipSlot.RangeWeapon;
                Console.WriteLine($"Only ranged weapon available: {_equipment[EquipSlot.RangeWeapon].Name}");
            }
            else
            {
                Console.WriteLine("No weapons equipped!");
            }
        }

        public EquipItem GetActiveWeapon()
        {
            return _equipment.TryGetValue(_activeWeaponSlot, out var weapon) && !weapon.IsBroken ? weapon : null;
        }

        public string GetActiveWeaponName()
        {
            var weapon = GetActiveWeapon();
            return weapon != null ? weapon.Name : "Fists";
        }

        public string GetActiveWeaponType()
        {
            var weapon = GetActiveWeapon();
            if (weapon == null) return "None";
            return weapon is Weapon ? "Melee" : "Ranged";
        }

        public override uint GetUnitDamage()
        {
            var weapon = GetActiveWeapon();
            if (weapon != null)
            {
                if (weapon is Weapon meleeWeapon)
                {
                    return BaseDamage + meleeWeapon.Damage;
                }
                else if (weapon is RangeWeapon rangeWeapon)
                {
                    return BaseDamage + rangeWeapon.Damage;
                }
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = GetInventoryItems();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    if (economicItem is Grindstone)
                    {
                        continue;
                    }
                    UseEconomicItem(economicItem);
                    RemoveItemFromInventory(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (_equipment.ContainsKey(equipItem.Slot))
                {
                    var oldItem = _equipment[equipItem.Slot];
                    Console.WriteLine($"Equipment replaced: {oldItem.Name} -> {equipItem.Name} in slot {equipItem.Slot}");
                    _equipment[equipItem.Slot] = equipItem;
                    base.AddItemToInventory(oldItem);
                }
                else
                {
                    _equipment.Add(equipItem.Slot, equipItem);
                    Console.WriteLine($"Equipped: {equipItem.Name} in slot {equipItem.Slot}");
                }
                return;
            }
            base.AddItemToInventory(item);
        }

        public EquipItem GetEquippedItem(EquipSlot slot)
        {
            return _equipment.ContainsKey(slot) ? _equipment[slot] : null;
        }

        public bool UseGrindstone()
        {
            var grindstone = GetInventoryItems().OfType<Grindstone>().FirstOrDefault();

            if (grindstone == null)
            {
                Console.WriteLine("No grindstone in inventory!");
                return false;
            }

            if (!_equipment.TryGetValue(EquipSlot.Weapon, out var meleeWeapon) || meleeWeapon.IsBroken)
            {
                Console.WriteLine("No melee weapon (sword) equipped or it's broken! Grindstone can only be used on melee weapons!");
                return false;
            }

            if (grindstone.UseOnWeapon(meleeWeapon))
            {
                if (!grindstone.TryDecreaseAmount())
                {
                    RemoveItemFromInventory(grindstone);
                }
                Console.WriteLine($"Grindstone used! {grindstone.Amount} remaining.");
                return true;
            }

            return false;
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                uint oldHealth = Health;
                Health = Math.Min(Health + healthPotion.HealthRestore, MaxHealth);
                Console.WriteLine($"Used Health Potion! Health restored: {oldHealth} -> {Health}");
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            uint reducedDamage = damage;

            if (_equipment.TryGetValue(EquipSlot.Armour, out var armourItem) && armourItem is Armour armour && !armourItem.IsBroken)
            {
                reducedDamage -= (uint)(damage * (armour.Defence / 100f));
                Console.WriteLine($"{armour.Name} reduced damage by {armour.Defence}%");
            }

            ReduceEquipmentDurability();
            return reducedDamage;
        }

        private void ReduceEquipmentDurability()
        {
            var slotsToReduce = new[] { EquipSlot.Armour, EquipSlot.Helmet };

            foreach (var slot in slotsToReduce)
            {
                if (_equipment.TryGetValue(slot, out var item) && !item.IsBroken)
                {
                    uint oldDurability = item.Durability;
                    item.ReduceDurability(1);

                    if (item.IsBroken)
                    {
                        Console.WriteLine($"{item.Name} has been destroyed and unequipped!");
                        _equipment.Remove(slot);
                    }
                    else if (oldDurability != item.Durability)
                    {
                        Console.WriteLine($"{item.Name} durability: {oldDurability} -> {item.Durability}");
                    }
                }
            }
        }

        public void ReduceActiveWeaponDurability()
        {
            var weapon = GetActiveWeapon();
            if (weapon != null && !weapon.IsBroken)
            {
                uint oldDurability = weapon.Durability;
                weapon.ReduceDurability(1);

                string atack = weapon is Weapon ? "Sword" : "Bow";
                Console.WriteLine($"{atack} {weapon.Name} durability: {oldDurability} -> {weapon.Durability}");

                if (weapon.IsBroken)
                {
                    Console.WriteLine($"{weapon.Name} has been destroyed!");
                    _equipment.Remove(_activeWeaponSlot);

                    if (_equipment.ContainsKey(EquipSlot.Weapon) && !_equipment[EquipSlot.Weapon].IsBroken)
                    {
                        _activeWeaponSlot = EquipSlot.Weapon;
                        Console.WriteLine($"Switched to {_equipment[EquipSlot.Weapon].Name}");
                    }
                    else if (_equipment.ContainsKey(EquipSlot.RangeWeapon) && !_equipment[EquipSlot.RangeWeapon].IsBroken)
                    {
                        _activeWeaponSlot = EquipSlot.RangeWeapon;
                        Console.WriteLine($"Switched to {_equipment[EquipSlot.RangeWeapon].Name}");
                    }
                }
            }
        }

        public void ReduceWeaponDurability()
        {
            ReduceActiveWeaponDurability();
        }

        protected override void DamageReceiveHandler()
        {
            if (Health == 0)
            {
                Console.WriteLine($"{Name} has died!");
            }
        }

        public void ShowEquipment()
        {
            Console.WriteLine("\n=== CURRENT EQUIPMENT ===");
            foreach (EquipSlot slot in Enum.GetValues(typeof(EquipSlot)))
            {
                if (_equipment.TryGetValue(slot, out var item))
                {
                    string status = item.IsBroken ? "BROKEN" : $"Durability: {item.Durability}";
                    string active = "";
                    if (slot == _activeWeaponSlot && (slot == EquipSlot.Weapon || slot == EquipSlot.RangeWeapon))
                    {
                        active = " ACTIVE";
                    }
                    Console.WriteLine($"{slot,12}: {item.Name,-15} [{status}]{active}");
                }
                else
                {
                    Console.WriteLine($"{slot,12}: Empty");
                }
            }
            Console.WriteLine($"\nActive weapon: {GetActiveWeaponName()} ({GetActiveWeaponType()})");
            Console.WriteLine("=============================\n");
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"{Name}");
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine($"Base Damage: {BaseDamage}");
            builder.AppendLine($"Total Damage: {GetUnitDamage()}");
            builder.AppendLine($"Active weapon: {GetActiveWeaponName()}");
            builder.AppendLine("\nEquipment:");
            foreach (EquipSlot slot in Enum.GetValues(typeof(EquipSlot)))
            {
                if (_equipment.TryGetValue(slot, out var item))
                {
                    string durability = item.IsBroken ? "BROKEN" : $"Durability: {item.Durability}";
                    builder.AppendLine($"{slot}: {item.Name} ({durability})");
                }
                else
                {
                    builder.AppendLine($"{slot}: Empty");
                }
            }
            builder.AppendLine("\nInventory:");
            var items = GetInventoryItems();
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}