using System;
using System.Reflection.Emit;
using DungeonBS.Models;

namespace DungeonBS.Controllers
{
    public class GameController
    {
        public void IniciarJuego()
        {
            int UwU = 0;
            string Nombre = "Hero";
            while(UwU != 1){

            
            try{
            Console.WriteLine("\n !!! -> Ingrese el nombre del jugador.");
            Nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(Nombre))
            {
                Nombre = "Hero";
            }
            UwU =1;
            }
            catch(FormatException){
                UwU =0;
                Console.WriteLine($"El Nombre debe ser Letras y Numeros, evita dejarlo vacio");
            }
        }
            Jugadores player = new Jugadores(Nombre);
            Monsters lobo = new Lobo("Lobo");
            Monsters golem = new Golem("Golem");

            // Aquí se puede agregar un menú
            player.Atacar(lobo);
            lobo.Attack(player);
            golem.Attack(player);
        }
    }
}
