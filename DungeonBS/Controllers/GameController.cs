using System;
using DungeonBS.Models;

namespace DungeonBS.Controllers
{
    public class GameController
    {
        public void IniciarJuego()
        {
            Console.WriteLine("\n !!! -> Ingrese el nombre del jugador.");
            string nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                nombre = "Hero";
            }
            Jugadores player = new Jugadores(nombre);
            Monsters lobo = new Lobo("Lobo");
            Monsters golem = new Golem("Golem");

            // Aquí se puede agregar un menú
            player.Atacar(lobo);
            lobo.Attack(player);
            golem.Attack(player);
        }
    }
}
