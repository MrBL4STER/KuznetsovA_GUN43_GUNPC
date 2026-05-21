using System;
using System.Collections.Generic;
using System.Text;

namespace class_for_unity_task
{
    public class Helm
    {
        public string Name { get; }
        public float Armor { get; set; }

        public Helm(string name = "Helm")
        {
            Name = name;
        }

        public Helm(float armor, string name = "Helm") : this(name)
        {
            Armor = armor;
        }
    }

    public class Shell
    {
        public string Name { get; }
        public float Armor { get; set; }

        public Shell(string name = "Shell")
        {
            Name = name;
        }

        public Shell(float armor, string name = "Shell") : this(name)
        {
            Armor = armor;
        }
    }

    public class Boots
    {
        public string Name { get; }
        public float Armor { get; set; }

        public Boots(string name = "Boots")
        {
            Name = name;
        }

        public Boots(float armor, string name = "Boots") : this(name)
        {
            Armor = armor;
        }
    }
}