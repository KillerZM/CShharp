using System;
using System.Collections.Generic;

namespace DungeonBS.Models
{
    public class Items
    {
        public string Name { get; set; } = "Generic";
        public int Value { get; set; }

        // Usamos un enum para definir el tipo de objeto
        public enum ItemType
        {
            Potion,
            Armor,
            Sword,
            Shield
        }

        // Propiedad para el tipo de objeto
        public ItemType Type { get; set; }
    }

    // Clases para las pociones
    public class HealthPotion : Items
    {
        public int Healing { get; set; }

        public HealthPotion()
        {
            Name = "Health Potion";
            Value = 30;
            Type = ItemType.Potion;
            Healing = 30;
        }
    }

    public class ManaPotion : Items
    {
        public int ManaRestored { get; set; }

        public ManaPotion()
        {
            Name = "Mana Potion";
            Value = 20;
            Type = ItemType.Potion;
            ManaRestored = 50;
        }
    }

    // Clases para las armaduras
    public class Armor : Items
    {
        public int Armoring { get; set; }
        public int MagicResistance { get; set; }
        public int AddHealth { get; set; }
    }

    public class IronArmor : Armor
    {
        public IronArmor()
        {
            Name = "Iron Armor";
            Value = 200;
            Type = ItemType.Armor;
            Armoring = 20;
            MagicResistance = 0;
            AddHealth = 20;
        }
    }

    public class BarionArmor : Armor
    {
        public BarionArmor()
        {
            Name = "Barion Armor";
            Value = 500;
            Type = ItemType.Armor;
            Armoring = 30;
            MagicResistance = 5;
            AddHealth = 30;
        }
    }

    public class DiamondArmor : Armor
    {
        public DiamondArmor()
        {
            Name = "Diamond Armor";
            Value = 800;
            Type = ItemType.Armor;
            Armoring = 40;
            MagicResistance = 10;
            AddHealth = 40;
        }
    }

    // Clases para las espadas
    public class Sword : Items
    {
        public int Dmg { get; set; }
    }

    public class IronSword : Sword
    {
        public IronSword()
        {
            Name = "Iron Sword";
            Value = 100;
            Type = ItemType.Sword;
            Dmg = 5;
        }
    }

    public class BarionSword : Sword
    {
        public BarionSword()
        {
            Name = "Barion Sword";
            Value = 300;
            Type = ItemType.Sword;
            Dmg = 6;
        }
    }

    public class DiamondSword : Sword
    {
        public DiamondSword()
        {
            Name = "Diamond Sword";
            Value = 500;
            Type = ItemType.Sword;
            Dmg = 8;
        }
    }

    // Clases para los escudos
    public class Shield : Items
    {
        public int Armoring { get; set; }
        public int MagicResistance { get; set; }
    }

    public class WoodenShield : Shield
    {
        public WoodenShield()
        {
            Name = "Wooden Shield";
            Value = 50;
            Type = ItemType.Shield;
            Armoring = 10;
            MagicResistance = 10;
        }
    }
}
