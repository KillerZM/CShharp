using System;
using System.Collections.Generic;

namespace DungeonBS.Models{
    public class Items{
        public string Name{get;set;} = "Generic";
        protected int Value{get;set;}
        public string Type {get;set;}
    }

    public class Potions: Items{
        public class Health:Potions{
            public int Healing;
            public Health(){
                Name= "Health Potion";
                Value= 50;
                Type = "H";
                Healing = 30;
            }
        }
        public class Mana:Potions{
            public int Maning;
            public Mana(){
                Name= "Mana Potion";
                Value= 50;
                Type= "M";
                Maning = 50;
            }
        }
    }

    public class Helmets: Items{
        public int Armoring;
        public int MagicResistance;
        public int AddHealth;

        public class IronHelmet:Helmets{
            public IronHelmet(){
                Name = "Iron Helmet";
                Value = 100;
                Type = "Armor";
                Armoring = 20;
                MagicResistance = 0;
                AddHealth = 20;
            }
        }
    }
}