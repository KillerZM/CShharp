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
        public int Gold{get;protected set;}

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
            Random Random = new Random();
            int num = Random.Next(1,6);
            if(this.Nombre=="Dragon" && (num >= 4)){
                ((Dragon)this).AlientoDeFuego(Player);
            }else
            {
                Console.WriteLine("\n !!! -> " + Nombre + " atacó a " + Player.Nick);

                
                int newDmg= Damage/ Random.Next(1,4);
                Player.PerderSalud(newDmg);
            }
        }

        public virtual void DarExp(Jugadores Player)
        {
            if (Estado != true)
            {
                Console.WriteLine($"\n !!! -> Este monstruo ya está muerto. ({Nombre})");
                return;
            }
            if(this.Nombre == "Lobo")
                Player.SubirEXP(((50) / 4));
            else if(this.Nombre == "Golem")
                Player.SubirEXP((50) / 3);
            else if(this.Nombre == "Dragon")
            Player.SubirEXP((60+(5*Lvl) / 2));
        }

        public void RecibirDmg(int dmg, Jugadores Player)
        {
            if (Estado != true)
            {
                Console.WriteLine($"\n !!! -> Este monstruo ya está muerto. ({Nombre})");
                return;
            }
            if (Lvl==0){Lvl=1;}
            int newDmg = dmg - ((Lvl) * 3);
            Console.WriteLine("\n !!! -> " + Nombre + " recibió [" + newDmg + "] de daño");
            if (newDmg >= Salud)
            {
                if(this.Nombre=="Dragon"){
                    Random Random = new Random();
                    int vidasextra = Random.Next(0,3);
                    Console.WriteLine("\n !!! -> El "+ Nombre + " murió y dejo consigo su alma... Intentaras extraerla...(presiona para continuar)");
                    Player.GanarOro(Gold);
                    Console.ReadKey();
                    if(vidasextra<=0){
                        Console.WriteLine("\n !!! -> :( No se pudo obtener el alma del "+ Nombre);
                    }else{
                        Console.WriteLine("\n !!! -> :) Se obtuvo el alma del "+ Nombre + $",{Player.Nick}(Presiona para consolidar el alma)");
                        Console.ReadKey();
                        Player.SubirEXP(10*vidasextra);
                        Player.GanarVidas(vidasextra);
                    }
                    DarExp(Player);
                    Salud = 0;
                    Estado = false;
                    return;
                }else{
                    Console.WriteLine("\n !!! -> " + Nombre + " murió.");
                    DarExp(Player);
                    Salud = 0;
                    Estado = false;
                    Player.GanarOro(Gold);
                }
            }
            else
            {
                Salud -= newDmg;
            }
        }
    }

    public class Lobo : Monsters
    {
        public Lobo(string Name,int Lvl)
        {
            if(Lvl < 1)
                Lvl = 1;
            Nombre = Name;
            Salud = 20+(5*Lvl);
            Damage = 25+(10*Lvl);
            Estado = true;
            Gold = 10 + (5 * Lvl);
            this.Lvl = Lvl;
        }
    }

    public class Golem : Monsters
    {
        public Golem(string Name,int Lvl)
        {
            Nombre = Name;
            Salud = 85 + (15 * Lvl);
            Damage = 10+(5*Lvl);
            Estado = true;
            Gold = 20 + (8 * Lvl);
            this.Lvl = Lvl;
        }

    }

    public class Dragon : Monsters
    {
        public Dragon(string Name,int Lvl)
        {
            Nombre = Name;
            Salud = 100+(20*Lvl);
            Damage = 40+(12*Lvl);
            Estado = true;
            Gold = 50 + (10 * Lvl);
            this.Lvl = Lvl;
        }


        public void AlientoDeFuego(Jugadores Player)
        {
            int Quemadura = (Damage * 2)/((Player.MagicResistance+1)/100);
            if (Estado != true)
            {
                Console.WriteLine($"\n !!! -> Dragon ya está muerto. ({Nombre})");
                return;
            }

            if (Player == null)
            {
                Console.WriteLine("\n !!! -> Dragon no encontro el jugador objetivo.");
                return;
            }
            else
            {
                Console.WriteLine("\n !!! -> " + Nombre + " quemó con Aliento de Fuego a " + Player.Nick);
                Player.PerderSalud(Quemadura);
            }
        }
    }
}
