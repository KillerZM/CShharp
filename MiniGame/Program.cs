using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace minigame{
    public class Jugadores{
        public string Nick;
        public int Vidas;
        public int Lvl;
        public int Exp;
        public int ExpNextLvl;
        public int Salud;
        public bool Estado;

        public Jugadores(string Nombre){
            Nick = Nombre;
            Vidas=2;
            Salud =100;
            Lvl=0;
            Exp=0;
            ExpNextLvl= 100;
            Estado = true;
        }

        /// Metodos
        public void SubirEXP(int Exp){
            if(Exp<=0){
                return;
            }

            if((this.Exp + Exp) > ExpNextLvl){
                //Subida de nivel
                Console.WriteLine("\n -> El jugador: "+ Nick+" Subio de nivel a nivel "+ Lvl+". ");
                this.Exp += Exp;
                this.Exp -= ExpNextLvl;
                Lvl += 1;
                ExpNextLvl = 2*ExpNextLvl;
                SubirEXP(this.Exp);
            }else{
                Console.WriteLine("\n -> El jugador: "+ Nick+" Subio "+ Exp + " de Experiencia. ");
                this.Exp += Exp; 
            } 

        }
        public void Atacar(Monsters Objetivo){
            if(Objetivo == null){
                return;
            }
            Objetivo.RecibirDmg((Lvl+1)*20, this);
        }
        public void PerderSalud(int dmg){
            if(dmg <= 0){
                Salud -= (dmg/ (Lvl+1));
            }else{
                return;
            }
        }
        public void PerderVida(){
            Vidas -= 1;
            if(Vidas <= 0){
                Console.WriteLine("\n !!! -> "+Nick+" Murio.");
                Estado = false;
            }
        }
    }
    public class Monsters{
        public string Nombre;
        public int Salud;
        public int Damage;
        public bool Estado;

        public Monsters(string Tipo){
            if(Tipo=="Golem"){
                Nombre = "Golem";
                Salud = 100;
                Damage = 30;
            }else if(Tipo=="Lobo"){
                Nombre = "Lobo";
                Salud = 50;
                Damage = 50;
            }
            Estado = true;
        }
        //Metodos
        public void Attack(Jugadores Player){
            if(Player==null){
                Console.WriteLine("\n !!! -> No existe el jugador objetivo.");
                return;
            }
            if(Player.Salud <= Damage){
                Console.WriteLine("\n !!! -> "+ Nombre + " Ataco a "+ Player.Nick + "Y le quitó una vida.");
                Player.PerderVida();
                //llamar perdida de vida
            }else{
                Console.WriteLine("\n !!! -> "+ Nombre + " Ataco a "+ Player.Nick + " y redujo su salud en "+ Damage + " pts");
                Player.PerderSalud(Damage);
                //llamar perdida de salud            
            }
        }
        public void RecibirDmg(int dmg, Jugadores Player){
            Console.WriteLine("\n !!! -> "+ Nombre+ " Recibio [" + dmg+"] de daño");
            if(dmg >= Salud){
                Console.WriteLine("\n !!! -> "+Nombre+ "Murio.");
                Player.SubirEXP(this.Salud/2);
                Salud =0;
                Estado = false;
                return;
            }else{
            Salud -= dmg;
            }
        }
    }
    public class Juego(){
        public static void Main(){
            Console.WriteLine("\n !!! -> Ingrese en nombre de jugador.");
            string nombre = Console.ReadLine();
            if(nombre == null){nombre="Nombre generico";}
            Jugadores Alex = new Jugadores(nombre);
            Monsters MLobo = new Monsters("Lobo");
            Monsters MGolem = new Monsters("Golem");

            //aqui va un menu pero pus no alcance :(

            // Alex: Todos ustedes contra mi solo.
            Alex.Atacar(MLobo);
            MLobo.Attack(Alex);
            MGolem.Attack(Alex);

        }
    }
}