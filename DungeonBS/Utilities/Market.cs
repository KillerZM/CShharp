using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DungeonBS.Models;

namespace DungeonBS.Utilities
{
    public class Market
    {
        public void MostrarMercado(Jugadores player){
            bool correct = false;
            int opcion = 0;
            // el bucle se ejecutará hasta que el usuario seleccione una opcion correcta
            while(!correct){
                try{
                Console.Clear();
                Console.WriteLine("Bienvenido al mercado, aquí puedes comprar y vender objetos");
                Console.WriteLine("Tu oro actual es: " + player.Gold);
                Console.WriteLine("¿Qué deseas hacer?");
                Console.WriteLine("1.- Comprar");
                Console.WriteLine("2.- Vender");
                Console.WriteLine("3.- Salir");
                Console.Write("Opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());
                correct = true;
                }
                catch(Exception){
                    correct = false;
                    Console.WriteLine("Opción no válida, intenta de nuevo.");
                    Console.WriteLine("Presiona cualquier tecla para continuar.");
                    Console.ReadKey();
                }
            }
                switch (opcion){
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        Console.WriteLine("Presiona cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            

        }
    

        public void ComprarMarket(Jugadores player){
            int auxopc=0;
            bool auxcorrect = false;
            while(!auxcorrect){
                try{
                Console.Clear();
                Console.WriteLine("--__ Mercado __--");
                Console.WriteLine("__--Comprando en el mercado--__");
                Console.WriteLine($"1.- Comprar Pocion de vida [30 oro]");
                Console.WriteLine($"2.- Comprar Pocion de mana [20 oro]");
                Console.WriteLine($"3.- Comprar Armas.");
                Console.WriteLine($"4.- Comprar Armadura.");
                Console.Write("Opción: ");
                auxopc = Convert.ToInt32(Console.ReadLine());
                auxcorrect = true;
                }
                catch(Exception){
                    auxcorrect = false;
                    Console.WriteLine("Opción no válida, intenta de nuevo.");
                    Console.WriteLine("Presiona cualquier tecla para continuar.");
                    Console.ReadKey();
                }
            }
            // Se ejecutara hasta que el usuario ya no quiera comprar mas objetos
            bool ContinueX= true;
            while(ContinueX){
                switch (auxopc){
                    case 1:
                        Items item = new HealthPotion();
                        bool transaccion = player.Comprar(item);
                        if(transaccion){
                            Console.WriteLine("[Mercader]--> Disfruta de salud prospera viajero.");
                        }else if(!transaccion){
                            Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                        }
                        break;
                    case 2:
                        Items item2 = new HealthPotion();
                        bool transaccion2 = player.Comprar(item2);
                        if(transaccion2){
                            Console.WriteLine("[Mercader]--> Disfruta de salud prospera viajero.");
                        }else if(!transaccion2){
                            Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        Console.WriteLine("Presiona cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            
                Console.WriteLine("[Mercader]--> ¿Deseas comprar algo más? 1.- Si 2.- No");
                int auxopc2 = Convert.ToInt32(Console.ReadLine());
                if(auxopc2 == 2){
                    ContinueX = false;
                }
            }

        }
        
    }
}

