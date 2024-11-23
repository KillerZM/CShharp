using System;
using DungeonBS.Models;

namespace DungeonBS.Abilities
{
    public class Skills
    {
        public string Nombre { get; set; }
        public int CostoMana { get; set; }
        public Action<Jugadores, Monsters> UsarHabilidad { get; set; }

        public void Usar(Jugadores jugador, Monsters objetivo)
        {
            if (jugador.Mana >= CostoMana)
            {
                jugador.Mana -= CostoMana;
                UsarHabilidad(jugador, objetivo);
            }
            else
            {
                Console.WriteLine($"{jugador.Nick} no tiene suficiente mana para usar {Nombre}.");
            }
        }
    }

    // Ejemplo de habilidades específicas
    public class GolpeCritico : Skills
    {
        public GolpeCritico()
        {
            Nombre = "Golpe Crítico";
            CostoMana = 10;
            UsarHabilidad = (jugador, objetivo) =>
            {
                int dano = jugador.Damage * 2;
                objetivo.RecibirDmg(dano, jugador);
                Console.WriteLine($"{jugador.Nick} usa {Nombre} e inflige {dano} de daño a {objetivo.Nombre}.");
            };
        }
    }

    public class Coraje : Skills
    {
        public Coraje()
        {
            Nombre = "Coraje";
            CostoMana = 20;
            UsarHabilidad = (jugador, objetivo) =>
            {
                jugador.Damage += 5;
                jugador.Salud += 10; // Recupera un poco de salud
                Console.WriteLine($"{jugador.Nick} usa {Nombre}, aumentando su daño y recuperando salud.");
            };
        }
    }

    public class BolaDeFuego : Skills
    {
        public BolaDeFuego()
        {
            Nombre = "Bola de Fuego";
            CostoMana = 15;
            UsarHabilidad = (jugador, objetivo) =>
            {
                int dano = jugador.MagicDamage * 2; // Basado en el daño mágico del jugador
                objetivo.RecibirDmg(dano, jugador);
                Console.WriteLine($"{jugador.Nick} usa {Nombre}, inflige {dano} de daño a {objetivo.Nombre}.");
            };
        }
    }
}
