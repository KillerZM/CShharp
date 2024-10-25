using System;
using Juego_futbol;

namespace Juego_futbol{
    public class Jugador{
        
            public int numero;
            public bool balon;
            public string nombre;
            public int posX;

            public void pasar(Jugador Receptor){
                this.balon = false;
                Receptor.balon = true;

                Console.WriteLine("\n !!! --> El jugador "+ this.nombre + " Pasó el balon a "+ Receptor.nombre);
            }
            public void Robar(Jugador Victima){
                this.balon = true;
                Victima.balon = false;

                Console.WriteLine("\n !!! --> El jugador "+ this.nombre + " Robó el balon a "+ Victima.nombre);
            }

            public void Posicion_Balon(){
                foreach(Jugador jugador in jugadores){
                    if(this.balon==true){
                    Console.WriteLine("\n\t !!! --> El jugador "+ this.nombre + "Tiene el balon");
                    break;
                    }
                }

            }
            

            public Jugador(string name, int numero, bool balon,int pos){
                this.nombre = name;
                this.numero = numero;
                this.balon = balon;
                this.posX = pos;

                jugadores.Add(this);
            }
    }
    public class Partido{
        public static void Main(){
            List<Jugador> jugadores = new List<Jugador>();
            Jugador Jugador1= new Jugador("Messi",10,false,4);

            

            for(int i=0;i<10;i++){
                string Nom="";
                int Num=0;
                int PosI=0;
                Console.WriteLine("\n\tIngrese el nombre del jugador "+ (i+1) +": ");
                Nom = Console.ReadLine();
                Console.WriteLine("\n\tIngrese el numero del jugador "+ (i+1) +": ");
                Num = Console.ReadLine();
                Console.WriteLine("\n\t 1.Defensa.  2.Medio.   3.Deltantero");
                Console.WriteLine("\n\tIngrese posicion de 1-3 del jugador "+ (i+1) +": ");
                PosI = Console.ReadLine();

                Jugador *Nom = new Jugador(Nom,Num,false,PosI);
            }
        }
    }
}
