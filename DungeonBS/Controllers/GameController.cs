using System;
using System.Collections.Generic;
using DungeonBS.Models;
using DungeonBS.Animations;
using DungeonBS.Utilities;

namespace DungeonBS.Controllers
{
    public class GameController
    {
        bool backmenu = true;
        private Jugadores player;
        private int numeroOleada = 0;
        private int nivelMonstruos = 0;

        public void IniciarJuego()
        {
            ConfigurarJugador(); // Crear jugador antes del menú

            while (true)
            {
                Console.WriteLine("\n\n\n *** 𝓜𝓮𝓷ú 𝓟𝓻𝓲𝓷𝓬𝓲𝓹𝓪𝓵 ***");
                Console.WriteLine($"Jugador: {player.Nick} | Nivel: {player.Lvl} ");
                Console.WriteLine($"Salud: {player.Salud} ♥ | Vidas: {player.Vidas} ♥ | Oro: {player.getOro()} ¤");
                Console.WriteLine("1. Jugar");
                Console.WriteLine("2. Mostrar Inventario");
                Console.WriteLine("3. Mercado");
                Console.WriteLine("4. Salir");
                Console.WriteLine("\n6. Informacion adicional del juego.");
                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        backmenu = false;
                        IniciarOleadas();
                        break;
                    case "2":
                        Console.Clear();
                        MostrarInventario();
                        break;
                    case "3": 
                        Console.Clear();
                        Market market = new Market();
                        market.MostrarMercado(player);
                        break;
                    case "4":
                        Console.Clear();
                        Animation.Logo();
                        Console.WriteLine("Gracias por jugar. ¡Hasta pronto!");
                        return;
                    case "6":
                        Console.Clear();
                        InformacionAdicional();
                        break;
                        
                    default:
                        try{}catch(Exception){} //Evita que el programa se cierre si se ingresa un valor no numérico.
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
            if(numeroOleada%3 == 0)
            {
                var Boss =new Dragon("Dragon", 1+nivel/3);
                monstruos.Add(Boss);
            }
            while (cantidadMonstruos > 0)
            {
                int tipoMonstruo = random.Next(1, 5); // Generar un número aleatorio entre 1 y 3
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
                    case 4:
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
                        
                        int pocionesSalud = player.Inventario.FindAll(x => x is HealthPotion).Count;
                        int pocionesMana = player.Inventario.FindAll(x => x is ManaPotion).Count;

                        if (pocionesSalud == 0 && pocionesMana == 0)
                        {
                            Console.WriteLine("No tienes pociones disponibles.");
                            break;
                        }

                        Console.WriteLine($"Pociones de Salud: {pocionesSalud}");
                        Console.WriteLine($"Pociones de Mana: {pocionesMana}");

                        bool seleccionValida = false;
                        while (!seleccionValida)
                        {
                            Console.WriteLine("Selecciona la poción que deseas usar:");
                            Console.WriteLine("1. Poción de Salud");
                            Console.WriteLine("2. Poción de Mana");
                            Console.WriteLine("3. Cancelar");
                            Console.WriteLine("Seleccione una opción:");

                            try
                            {
                                int seleccion = Convert.ToInt32(Console.ReadLine());
                                switch (seleccion)
                                {
                                    case 1:
                                        if (pocionesSalud > 0)
                                        {
                                            Items pocionSalud = player.Inventario.Find(x => x is HealthPotion);
                                            player.UsarPocion(pocionSalud);
                                            seleccionValida = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("No tienes pociones de salud disponibles.");
                                        }
                                        break;
                                    case 2:
                                        if (pocionesMana > 0)
                                        {
                                            Items pocionMana = player.Inventario.Find(x => x is ManaPotion);
                                            player.UsarPocion(pocionMana);
                                            seleccionValida = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("No tienes pociones de mana disponibles.");
                                        }
                                        break;
                                    case 3:
                                        seleccionValida = true;
                                        Console.WriteLine("Operación cancelada.");
                                        break;
                                    default:
                                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Opción no válida. Intente de nuevo.");
                            }
                        Console.WriteLine("¿Quieres usar otra poción?");
                        Console.WriteLine("1. Sí, usar otra poción");
                        Console.WriteLine("2. No, salir");
                        Console.Write("Seleccione una opción: ");
                        int opcionPocion = int.Parse(Console.ReadLine());
                        if (opcionPocion == 2)
                        {
                            seleccionValida = true;

                        } else{
                            seleccionValida = false;}
                        }
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Random ataque = new Random();
                // Turno de los monstruos
                foreach (var monstruo in monstruos)
                {
                    int opcionAtaque = ataque.Next(1, 11); // Generar un número aleatorio entre 1 y 10
                    // Lógica para que los monstruos no ataquen si el jugador ha muerto
                    if (!player.Estado)
                    {
                        break;
                    }

                    if(opcionAtaque != 1 && opcionAtaque != 5 && opcionAtaque != 10){
                        monstruo.Attack(player);
                    }else{}

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
                            //numeroOleada = 0;  // Reiniciar contador de oleadas
                            //nivelMonstruos = 1; // Reiniciar nivel
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
            Console.WriteLine($"Jugador: {player.Nick} | Nivel: {player.Lvl}");
            Console.WriteLine("\n--__Pociones__--");
            // Lógica para contar y mostrar pociones
            Console.WriteLine($"Pociones de Salud: {player.Inventario.FindAll(x => x is HealthPotion).Count}");
            Console.WriteLine($"Pociones de Mana: {player.Inventario.FindAll(x => x is ManaPotion).Count}");

            // Mostrar equipamiento
            Console.WriteLine("\n__--Equipamiento--__");
            foreach (var item in player.Equipamiento)
            {
                Console.WriteLine($"{item.Key}: {(item.Value != null ? item.Value.Name : "Ninguno")}");
            }

            Console.WriteLine("\n--__Objetos__--");
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
            bool auxinventory = true;
            int opcion = 0;
            while (auxinventory)
            {
                try
                {
                    Console.WriteLine($"\n1. Equipar objeto");
                    Console.WriteLine("2. Desequipar objeto");
                    Console.WriteLine("3. Salir");
                    Console.Write("\nSeleccione una opción: ");
                    opcion = int.Parse(Console.ReadLine());
                    auxinventory = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    auxinventory = true;
                }
            }

            // Lógica para equipar y desequipar objetos
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Selecciona el objeto que deseas equipar:");
                    for (int i = 0; i < player.Inventario.Count; i++)
                    {
                        if (!(player.Inventario[i] is HealthPotion) && !(player.Inventario[i] is ManaPotion))
                        {
                            Console.WriteLine($"{i + 1}.- {player.Inventario[i].Name}");
                        }
                    }
                    Console.WriteLine($"{player.Inventario.Count + 1}.- Cancelar");

                    int opcionEquipar = 0;
                    try
                    {
                        opcionEquipar = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                    }

                    if (opcionEquipar == player.Inventario.Count + 1)
                    {
                        Console.WriteLine("Operación cancelada.");
                        break;
                    }

                    if (opcionEquipar > 0 && opcionEquipar <= player.Inventario.Count)
                    {
                        Items item = player.Inventario[opcionEquipar - 1];
                        player.Equipar(item);
                    }
                    else
                    {
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                    }
                    break;

                case 2:
                Console.WriteLine("Selecciona el objeto que deseas desequipar:");
                int index = 1;
                var equipKeys = player.Equipamiento.Keys.ToList();
                foreach (var key in equipKeys)
                {
                    var item = player.Equipamiento[key];
                    Console.WriteLine($"{index}.- {key}: {(item != null ? item.Name : "Ninguno")}");
                    index++;
                }
                Console.WriteLine($"{index}.- Cancelar");

                int opcionDesequipar = 0;
                try
                {
                    opcionDesequipar = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
                }

                if (opcionDesequipar == index)
                {
                    Console.WriteLine("Operación cancelada.");
                    break;
                }

                if (opcionDesequipar > 0 && opcionDesequipar < index)
                {
                    string tipoEquipamiento = equipKeys[opcionDesequipar - 1];
                    player.Desequipar(tipoEquipamiento);
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                }
                break;

                case 3:
                    Console.WriteLine("Saliendo del inventario.");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    

        private void InformacionAdicional()
        {
            Console.Clear();
            Animation.Logo();
            Console.WriteLine("\n*** Información Adicional ***");
            Console.WriteLine("DungeonBS es un juego de rol por turnos en el que el jugador debe enfrentarse a oleadas de monstruos.");
            Console.WriteLine("El jugador puede atacar, usar habilidades y pociones para sobrevivir.");
            Console.WriteLine("El objetivo es llegar lo más lejos posible y derrotar a los monstruos más poderosos.");
            Console.WriteLine("¡Buena suerte!");

            try
            {
                Console.WriteLine("\n(Presiona cualquier tecla para continuar)");
                Console.ReadKey(); // Espera a que el usuario presione una tecla
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\n(Presiona Enter para continuar)");
                Console.ReadLine(); // Espera a que el usuario presione Enter
            }

            Console.Clear(); // Limpia la consola después de que el usuario presione una tecla
            Console.WriteLine($"__--Informacion de objetos y monstruos.--__");
            Console.WriteLine("1. Las espadas tienen una habilidad especial, al portar una el jugador puede realizar un ataque especial.");
            Console.WriteLine("Este ataque es un golpe crítico que inflige daño adicional al monstruo pero ocurre de manera aleatoria.\n");
            Console.WriteLine("2. Los escudos aumentan la defensa del jugador.");
            Console.WriteLine("Al portar un escudo, el jugador recibe menos daño de los ataques de los monstruos.\n");
            Console.WriteLine("3. Las armaduras aumentan la defensa, salud y la resistencia mágica del jugador.");
            Console.WriteLine("Al portar una armadura, el jugador recibe menos daño de los ataques de los monstruos y tiene más salud.\n");
            Console.WriteLine("4. Las pociones de salud y mana restauran la salud y el mana del jugador respectivamente.");
            Console.WriteLine("Las pociones de salud restauran [30] puntos de salud y las pociones de mana restauran [20] puntos de mana.\n");
            Console.WriteLine("5. Los monstruos tienen diferentes niveles de dificultad y habilidades.");
            Console.WriteLine("Los monstruos más poderosos tienen más salud y hacen más daño.\n");
            Console.WriteLine("6. El jugador puede ganar oro al derrotar monstruos y usarlo para comprar objetos en el mercado.");
            Console.WriteLine("El oro se utiliza para comprar pociones, armas, armaduras y escudos.\n");
            Console.WriteLine("7. El jugador puede equipar objetos para aumentar sus estadísticas y mejorar su rendimiento en combate.");
            Console.WriteLine("El jugador puede equipar una espada, un escudo, una armadura y una poción de salud y mana.\n");
            Console.WriteLine("8. El jugador puede usar habilidades especiales para infligir daño adicional a los monstruos.");
            Console.WriteLine("Las habilidades tienen un costo de mana alto.\n");
            Console.WriteLine("9. [DRAGONES]-> Los dragones son criaturas míticas, estos tienen demasiada salud y bastante daño.");
            Console.WriteLine("Los dragones tienen una habilidad especial que les permite lanzar un aliento de fuego que inflige daño a el jugador aumentado.");
            Console.WriteLine("Los dragones tienen una probabilidad de [30%] de lanzar su aliento de fuego.\n");
            Console.WriteLine("Los dragones al ser derrotados ademas de oro, dejan consigo su alma, la cual puede ser extraida por el jugador.");
            Console.WriteLine("El jugador tiene una probabilidad de [50%] de extraer el alma del dragon, si lo logra, obtiene una vida extra o quizá mas.\n");
            Console.WriteLine("10. [GOLEM]-> Los golems son criaturas de piedra, estos tienen mucha salud y resistencia. Pero poco daño.");
            Console.WriteLine("11. [LOBO]-> Los lobos son criaturas de la noche, estos tienen poca salud, pero bastante daño.");
            Console.WriteLine("\n(Presiona cualquier tecla para continuar)");

            try
            {
                Console.ReadKey(); // Espera a que el usuario presione una tecla antes de salir del método
            }
            catch (InvalidOperationException)
            {
                Console.ReadLine(); // Espera a que el usuario presione Enter antes de salir del método
            }
        }
    }
}