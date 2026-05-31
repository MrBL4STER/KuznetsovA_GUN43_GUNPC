using System;
using System.Collections.Generic;
using System.Text;

namespace GamePrototype.Items.EconomicItems
{
    public abstract class EconomicItem : Item
    {
        protected EconomicItem(string name) : base(name) 
        {
        }
    }
}
