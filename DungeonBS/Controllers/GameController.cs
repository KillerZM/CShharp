using System;
using System.Collections.Generic;
using DungeonBS.Models;
using DungeonBS.Animations;

namespace DungeonBS.Controllers
{
    public class GameController
    {
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
                Console.WriteLine("3. Salir");
                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IniciarOleadas();
                        break;
                    case "2":
                        MostrarInventario();
                        break;
                    case "3":
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
            while (true)
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
            if (!player.Estado)
                {
                    Console.WriteLine("¡Has sido derrotado!, No tienes mas vidas");
                    return;
                }

            while (monstruos.Count > 0)
            {   
                Console.WriteLine("\n*--__ Monstruos en la Oleada __--*");
                for (int i = 0; i < monstruos.Count; i++){
                    Console.WriteLine($"{i + 1}. {monstruos[i].Nombre} - Salud [♥]: {monstruos[i].Salud}");
                }  
                bool correct=false;
                int opcion=0;
                while(correct==false){
                    try{
                    Console.WriteLine($"\nTurno del Jugador [{player.Salud} ♥]: ");
                    Console.WriteLine("1. Atacar");
                    Console.WriteLine("2. Usar Habilidad");
                    Console.WriteLine("3. Usar Poción");
                    opcion = int.Parse(Console.ReadLine());
                    if(opcion==1 || opcion==2 || opcion==3){
                        correct=true;
                    }
                    }catch(FormatException){
                        Console.WriteLine("!!! -> Ingrese Por favor, una opcion valida.");
                    }
                }
                switch (opcion)
                {
                    case 1:
                        
                        Console.WriteLine("Seleccione el monstruo para atacar:");
                         for (int i = 0; i < monstruos.Count; i++){
                             Console.WriteLine($"{i + 1}. {monstruos[i].Nombre} - Salud [♥]: {monstruos[i].Salud}");
                            }
                            bool temp=false;
                            while(temp==false){
                                try{
                                    int indiceMonstruo = Convert.ToInt32(Console.ReadLine()) - 1; 
                                    if (indiceMonstruo >= 0 && indiceMonstruo < monstruos.Count){
                                        if(monstruos[indiceMonstruo] is Lobo){
                                            Animation.AnLobo();
                                            }else if(monstruos[indiceMonstruo] is Golem){
                                            Animation.AnGolem();
                                            }else if(monstruos[indiceMonstruo] is Dragon){
                                            Animation.AnDragon();}
                                        player.Atacar(monstruos[indiceMonstruo]); 
                                        
                                            
                                        temp=true;
                                    }else{
                                    Console.WriteLine("!!! -> Índice de monstruo no válido.");
                                    temp=false;
                                    }
                                    
                                }catch(FormatException){
                                    Console.WriteLine("!!! -> Ingrese Por favor, una opcion valida.");
                                }
                            }
                            break;
                    case 2:
                        //(Si no tiene habilidades, mostrar mensaje y volver al menú)
                        if ( player.Habilidades == null|| player.Habilidades.Count == 0){
                            Console.WriteLine("\n !!! -> No tienes habilidades para usar.");
                            break;
                        }
                        // Lógica para usar habilidad
                        Console.WriteLine("Seleccione la habilidad:");
                        for (int i = 0; i < player.Habilidades.Count; i++){
                            Console.WriteLine($"{i + 1}. {player.Habilidades[i].Nombre}");
                            }
                        bool habilidadSeleccionada = false;
                        while (!habilidadSeleccionada){
                        try{
                            int indiceHabilidad = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (indiceHabilidad >= 0 && indiceHabilidad < player.Habilidades.Count)
                            {
                                Console.WriteLine("Seleccione el monstruo para usar la habilidad:");
                                for (int j = 0; j < monstruos.Count; j++)
                                {
                                    Console.WriteLine($"{j + 1}. {monstruos[j].Nombre} - Salud: {monstruos[j].Salud}");
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
                                            monstruoSeleccionado = true;
                                            habilidadSeleccionada = true;
                                            break; // Salir del while interno de seleccionar monstruo
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
                                break; // Salir del while externo de seleccionar habilidad
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
                    case 3:
                        // Lógica para usar poción
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
                    if (!player.Estado){
                        if (player.Vidas <= 0){
                        Console.WriteLine("¡Has sido derrotado! No tienes más vidas.");
                        Console.WriteLine("¡Gracias por jugar!");
                        Console.WriteLine("GAME OVER!");
                        
                        // Reiniciar oleadas si no tiene más vidas
                        numeroOleada = 0;
                        nivelMonstruos = 1;
                        ConfigurarJugador();
                        return;
                    }else{
                        Console.WriteLine("¡Has sido derrotado! Pero aún tienes vidas restantes.");
                        // Revivir al jugador y volver al menú sin reiniciar oleadas
                        player.PerderVida();
                        return;
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
