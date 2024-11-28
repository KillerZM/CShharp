using System;
using System.Collections.Generic;
using DungeonBS.Models;
using DungeonBS.Animations;

namespace DungeonBS.Controllers
{
    public class GameController
    {
        bool backmenu = true;
        private Jugadores player;
        private int numeroOleada = 0;
        private int nivelMonstruos = 1;

        public void IniciarJuego()
        {
            ConfigurarJugador(); // Crear jugador antes del menú

            while (true)
            {
                Console.WriteLine("\n *** Menú Principal ***");
                Console.WriteLine($"Jugador: {player.Nick} | Nivel: {player.Lvl}");
                Console.WriteLine("1. Jugar");
                Console.WriteLine("2. Mostrar Inventario");
                Console.WriteLine("3. Mercado");
                Console.WriteLine("4. Salir");
                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        backmenu = false;
                        IniciarOleadas();
                        break;
                    case "2":
                        MostrarInventario();
                        break;
                    case "3": 
                        //Logica para entrar al merdcado
                        break;
                    case "4":
                        Console.WriteLine("Gracias por jugar. ¡Hasta pronto!");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        private void ConfigurarJugador()
        {
            int UwU = 0;
            string Nombre = "Hero";
            Animation.Logo();
            while (UwU != 1)
            {
                try
                {
                    Console.WriteLine("\n !!! -> Ingrese el nombre del jugador.");
                    Nombre = Console.ReadLine();
                    if (string.IsNullOrEmpty(Nombre))
                    {
                        Nombre = "Hero";
                    }
                    UwU = 1;
                }
                catch (FormatException)
                {
                    UwU = 0;
                    Console.WriteLine("El Nombre debe ser Letras y Números, evita dejarlo vacío.");
                }
            }
            player = new Jugadores(Nombre);
        }

        private void IniciarOleadas()
        {
            while (backmenu == false)
            {
                numeroOleada++;
                if (numeroOleada % 2 == 0)
                {
                    nivelMonstruos++;
                }

                Console.WriteLine($"\nOleada {numeroOleada} - Nivel de Monstruos {nivelMonstruos}");

                List<Monsters> monstruos = GenerarMonstruos(nivelMonstruos);

                // Llamada al sistema de combate con la lista de monstruos de la oleada actual
                SistemaDeCombate(monstruos);

                if (!player.Estado)
                {
                    Console.WriteLine("¡Has sido derrotado!");
                    break;
                }

                // Si el jugador ha sobrevivido a la oleada, mostrar mensaje y continuar
                if (numeroOleada != 0)
                    Console.WriteLine($"¡Has sobrevivido a la oleada {numeroOleada}!");
            }
        }

        private List<Monsters> GenerarMonstruos(int nivel)
        {
            // Generar una lista de monstruos basada en el nivel
            List<Monsters> monstruos = new List<Monsters>
            {
                new Lobo("Lobo", nivel) //Añadiremos al menos un Lobo jaja
            };

            int cantidadMonstruos = nivel / 2; // Cantidad de monstruos a generar
            Random random = new Random();
            //Generara Dragones cada 3 niveles.
            if(nivel%3 == 0)
            {
                new Dragon("Dragon", 1+nivel/3);
            }
            while (cantidadMonstruos > 0)
            {
                int tipoMonstruo = random.Next(1, 4); // Generar un número aleatorio entre 1 y 3
                switch (tipoMonstruo)
                {
                    case 1:
                        monstruos.Add(new Lobo("Lobo", nivel));
                        break;
                    case 2:
                        monstruos.Add(new Golem("Golem", nivel));
                        break;
                    case 3:
                        monstruos.Add(new Lobo("Lobo", nivel));
                         monstruos.Add(new Golem("Golem", nivel));
                        break;
                }
                cantidadMonstruos--;
            }

            return monstruos;
        }

        private void SistemaDeCombate(List<Monsters> monstruos)
        {
                //Declara una variable temporal para detectar si el jugador ha sido derrotado durante un turno de monstruos
            int VidasAux = player.Vidas;

            if (!player.Estado)
            {
                Console.WriteLine("¡Has sido derrotado! No tienes más vidas.");
                return;
            }

            while (monstruos.Count > 0)
            {
                // Mostrar monstruos en la oleada
                Console.WriteLine("\n*--__ Monstruos en la Oleada __--*");
                for (int i = 0; i < monstruos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {monstruos[i].Nombre} - Salud [♥]: {monstruos[i].Salud}");
                }

                int opcion = 0;
                bool correct = false;
                while (!correct)
                {
                    try
                    {
                        Console.WriteLine($"\nTurno del Jugador [{player.Salud} ♥]: ");
                        Console.WriteLine("1. Atacar");
                        Console.WriteLine("2. Usar Habilidad");
                        Console.WriteLine("3. Usar Poción");
                        opcion = int.Parse(Console.ReadLine());
                        if (opcion == 1 || opcion == 2 || opcion == 3)
                        {
                            correct = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("!!! -> Ingrese por favor, una opción válida.");
                    }
                }

                switch (opcion)
                {
                    case 1: // Atacar
                        Console.WriteLine("Seleccione el monstruo para atacar:");
                        for (int i = 0; i < monstruos.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {monstruos[i].Nombre} - Salud [♥]: {monstruos[i].Salud}");
                        }
                        bool temp = false;
                        while (!temp)
                        {
                            try
                            {
                                int indiceMonstruo = Convert.ToInt32(Console.ReadLine()) - 1;
                                if (indiceMonstruo >= 0 && indiceMonstruo < monstruos.Count)
                                {
                                    // Animación basada en el tipo de monstruo
                                    if (monstruos[indiceMonstruo] is Lobo) Animation.AnLobo();
                                    else if (monstruos[indiceMonstruo] is Golem) Animation.AnGolem();
                                    else if (monstruos[indiceMonstruo] is Dragon) Animation.AnDragon();

                                    player.Atacar(monstruos[indiceMonstruo]);
                                    temp = true;
                                }
                                else
                                {
                                    Console.WriteLine("!!! -> Índice de monstruo no válido.");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("!!! -> Ingrese por favor, una opción válida.");
                            }
                        }
                        break;

                    case 2: // Usar habilidad
                        if (player.Habilidades == null || player.Habilidades.Count == 0)
                        {
                            Console.WriteLine("\n !!! -> No tienes habilidades para usar.");
                            break;
                        }
                        Console.WriteLine("Seleccione la habilidad:");
                        for (int i = 0; i < player.Habilidades.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {player.Habilidades[i].Nombre}");
                        }
                        bool habilidadSeleccionada = false;
                        while (!habilidadSeleccionada)
                        {
                            try
                            {
                                int indiceHabilidad = Convert.ToInt32(Console.ReadLine()) - 1;
                                if (indiceHabilidad >= 0 && indiceHabilidad < player.Habilidades.Count)
                                {
                                    Console.WriteLine("Seleccione el monstruo para usar la habilidad:");
                                    for (int j = 0; j < monstruos.Count; j++)
                                    {
                                        Console.WriteLine($"{j + 1}. {monstruos[j].Nombre} - Salud [♥]: {monstruos[j].Salud}");
                                    }
                                    bool monstruoSeleccionado = false;
                                    while (!monstruoSeleccionado)
                                    {
                                        try
                                        {
                                            int indiceMonstruo = Convert.ToInt32(Console.ReadLine()) - 1;
                                            if (indiceMonstruo >= 0 && indiceMonstruo < monstruos.Count)
                                            {
                                                player.UsarHabilidad(indiceHabilidad, monstruos[indiceMonstruo]);
                                                habilidadSeleccionada = true;
                                                monstruoSeleccionado = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("!!! -> Índice de monstruo no válido.");
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("!!! -> Ingrese por favor, una opción válida.");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("!!! -> Índice de habilidad no válido.");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("!!! -> Ingrese por favor, una opción válida.");
                            }
                        }
                        break;

                    case 3: // Usar poción
                        Console.WriteLine("Usar poción aún no está implementado.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }


                // Turno de los monstruos
                foreach (var monstruo in monstruos)
                {
                    monstruo.Attack(player);

                    // Verificar si el jugador ha muerto después del ataque de cada monstruo
                    if (player.Vidas < VidasAux || player.Salud <= 0)
                    {
                        if (player.Vidas <= 0)
                        {
                            Console.WriteLine("¡Has sido derrotado! No tienes más vidas.");
                            Console.WriteLine("¡Gracias por jugar!");
                            Console.WriteLine("GAME OVER!");

                            // Reiniciar oleadas si no tiene más vidas
                            numeroOleada = 0;
                            nivelMonstruos = 1;
                            backmenu = true;
                            ConfigurarJugador();
                            return;// Salir de combate para empezar uno nuevo.
                        }
                        else
                        {
                            Console.WriteLine("¡Has sido derrotado! Pero aún tienes vidas restantes.");
                            
                            // Reiniciar oleadas y regresar al menú principal
                            monstruos.Clear(); // Eliminar monstruos actuales
                            // Se tiene pensado que no se pierda el progreso de las oleadas si el jugador tiene vidas restantes
                            numeroOleada = 0;  // Reiniciar contador de oleadas
                            nivelMonstruos = 1; // Reiniciar nivel
                            backmenu = true;
                            return; // Salir del combate y regresar al menú
                        }
                    }
                }

                // Remover monstruos muertos
                monstruos.RemoveAll(m => !m.Estado);
            }
        }


        private void MostrarInventario()
        {
            Console.WriteLine("\n*** Inventario y Equipamiento ***");

            Console.WriteLine("--__Pociones__--");
            // Lógica para contar y mostrar pociones
            Console.WriteLine($"Pociones de Salud: {player.Inventario.FindAll(x => x is HealthPotion).Count}");
            Console.WriteLine($"Pociones de Mana: {player.Inventario.FindAll(x => x is ManaPotion).Count}");

            // Mostrar equipamiento
            Console.WriteLine("__--Equipamiento--__");
            foreach (var item in player.Equipamiento)
            {
                Console.WriteLine($"{item.Key}: {(item.Value != null ? item.Value.Name : "Ninguno")}");
            }

            Console.WriteLine("--__Objetos__--");
            // Mostrar todos los objetos del inventario
            foreach (var item in player.Inventario)
            {
                if (item is HealthPotion || item is ManaPotion)
                {
                    // Evitar mostrar pociones aquí porque ya las contamos arriba
                }
                else
                {
                    Console.WriteLine($"{item.Name}");
                }
            }
        }
    }
}
