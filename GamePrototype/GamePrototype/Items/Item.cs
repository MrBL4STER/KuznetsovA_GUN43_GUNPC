using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text;

namespace GamePrototype.Items
{
    public abstract class Item
    {
        public abstract bool Stackable { get; }

        public virtual uint Amount { get; protected set; }

        public string Name { get; }

        protected Item(string name) 
        {
            Name = name;
            Amount = 1;
        }

        public bool TryStack(Item item)
        {
            if (!Stackable)
            {
                return false;
            }
            Amount++;
            return true;
        }
    }
}
