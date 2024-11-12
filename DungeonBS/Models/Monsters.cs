using System;

namespace DungeonBS.Models{
    public class Monsters
    {
        public string Nombre{get;protected set;}
        public int Salud{get;protected set;}
        public int Damage{get;protected set;}
        public int MagicDamage{get; protected set;}
        public bool Estado{get;protected set;}
        public int Lvl {get; protected set;}

        public void Attack(Jugadores Player)
        {
            if (Estado != true)
            {
                Console.WriteLine($"\n !!! -> Este monstruo ya está muerto. ({Nombre})");
                return;
            }

            if (Player == null)
            {
                Console.WriteLine("\n !!! -> No existe el jugador objetivo.");
                return;
            }
            if (Player.Salud <= Damage)
            {
                Console.WriteLine("\n !!! -> " + Nombre + " atacó a " + Player.Nick + " y le quitó una vida.");
                Player.PerderVida();
            }
            else
            {
                Console.WriteLine("\n !!! -> " + Nombre + " atacó a " + Player.Nick + " y redujo su salud en " + Damage + " pts");
                Player.PerderSalud(Damage);
            }
        }

        public void RecibirDmg(int dmg, Jugadores Player)
        {
            if (Estado != true)
            {
                Console.WriteLine($"\n !!! -> Este monstruo ya está muerto. ({Nombre})");
                return;
            }

            Console.WriteLine("\n !!! -> " + Nombre + " recibió [" + dmg + "] de daño");
            if (dmg >= Salud)
            {
                Console.WriteLine("\n !!! -> " + Nombre + " murió.");
                Player.SubirEXP(this.Salud / 2);
                Salud = 0;
                Estado = false;
            }
            else
            {
                Salud -= dmg;
            }
        }
    }

    public class Lobo : Monsters
    {
        public Lobo(string Name)
        {
            Nombre = Name;
            Salud = 40;
            Damage = 40;
            Estado = true;
        }
    }

    public class Golem : Monsters
    {
        public Golem(string Name)
        {
            Nombre = Name;
            Salud = 80;
            Damage = 20;
            Estado = true;
        }
    }

    public class Dragon : Monsters
    {
        public Dragon(string Name)
        {
            Nombre = Name;
            Salud = 180;
            Damage = 50;
            Estado = true;
        }

        public void AlientoDeFuego(Jugadores Player)
        {
            int Quemadura = (Damage * 2);
            if (Estado != true)
            {
                Console.WriteLine($"\n !!! -> Este monstruo ya está muerto. ({Nombre})");
                return;
            }

            if (Player == null)
            {
                Console.WriteLine("\n !!! -> No existe el jugador objetivo.");
                return;
            }
            if (Player.Salud <= Quemadura)
            {
                Console.WriteLine("\n !!! -> " + Nombre + " quemó con Aliento de Fuego a " + Player.Nick + " y le quitó una vida.");
                Player.PerderVida();
            }
            else
            {
                Console.WriteLine("\n !!! -> " + Nombre + " quemó con Aliento de Fuego a " + Player.Nick + " y redujo su salud en " + Quemadura + " pts");
                Player.PerderSalud(Quemadura);
            }
        }
    }
}
