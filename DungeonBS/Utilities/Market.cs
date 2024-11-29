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
                        ComprarMarket(player);
                        break;
                    case 2:
                        VenderMarket(player);
                        break;
                    case 3:
                        Console.WriteLine("[Mercader]--> Hasta luego viajero.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        Console.WriteLine("Presiona cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            

        }
    

        public void ComprarMarket(Jugadores player)
        {
            int auxopc = 0;
            bool auxcorrect = false;
            bool ContinueX = true;
            while (ContinueX)
            {
                while (!auxcorrect)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("--__ Mercado __--");
                        Console.WriteLine("Tu oro actual es: " + player.getOro());
                        Console.WriteLine("__--Comprando en el mercado--__");
                        Console.WriteLine($"1.- Comprar Pocion de vida [30 oro]");
                        Console.WriteLine($"2.- Comprar Pocion de mana [20 oro]");
                        Console.WriteLine($"3.- Comprar Armas.");
                        Console.WriteLine($"4.- Comprar Armadura.");
                        Console.WriteLine("5.- Cancelar");
                        Console.Write("Opción: ");
                        auxopc = Convert.ToInt32(Console.ReadLine());
                        auxcorrect = true;
                    }
                    catch (Exception)
                    {
                        auxcorrect = false;
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        Console.WriteLine("Presiona cualquier tecla para continuar.");
                        Console.ReadKey();
                    }
                }

            // Se ejecutará hasta que el usuario ya no quiera comprar más objetos
            
            
                switch (auxopc)
                {
                    case 1:
                        Items item = new HealthPotion();
                        bool transaccion = player.Comprar(item);
                        if (transaccion)
                        {
                            Console.WriteLine("[Mercader]--> Disfruta de salud prospera viajero.");
                        }
                        else
                        {
                            Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                        }
                        break;
                    case 2:
                        Items item2 = new ManaPotion();
                        bool transaccion2 = player.Comprar(item2);
                        if (transaccion2)
                        {
                            Console.WriteLine("[Mercader]--> Disfruta de salud prospera viajero.");
                        }
                        else
                        {
                            Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                        }
                        break;
                    case 3:
                        bool weaponaux = true;
                        int auxweapon = 0;
                        while (weaponaux)
                        {
                            Console.Clear();
                            Console.WriteLine("[Mercader]--> Aquí tienes las armas que tengo en venta.");
                            Console.WriteLine("1.- Espada de Hierro [50 oro]");
                            Console.WriteLine("2.- Espada de Barion [150 oro]");
                            Console.WriteLine("3.- Espada de Diamante [250 oro]");
                            Console.WriteLine("4.- Escudo de Madera [100 oro]");
                            Console.WriteLine("5.- Cancelar");
                            try
                            {
                                auxweapon = Convert.ToInt32(Console.ReadLine());
                                weaponaux = false;
                                if (auxweapon == 5)
                                {
                                    Console.WriteLine("[Mercader]--> Hasta luego viajero.");
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                weaponaux = true;
                                Console.WriteLine("[Mercader]--> Selecciona un arma e intenta de nuevo.");
                                Console.WriteLine("(Presiona cualquier tecla para continuar.)");
                                Console.ReadKey();
                            }
                        }
                        switch (auxweapon)
                        {
                            case 1:
                                Items item3 = new IronSword();
                                bool transaccion3 = player.Comprar(item3);
                                if (transaccion3)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nueva espada.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 2:
                                Items item4 = new BarionSword();
                                bool transaccion4 = player.Comprar(item4);
                                if (transaccion4)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nueva espada.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 3:
                                Items item5 = new DiamondSword();
                                bool transaccion5 = player.Comprar(item5);
                                if (transaccion5)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nueva espada.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 4:
                                Items item6 = new WoodenShield();
                                bool transaccion6 = player.Comprar(item6);
                                if (transaccion6)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nuevo escudo.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine("[Mercader]--> Yo no vendo eso, intenta algo de aquí.");
                                Console.WriteLine("(Presiona cualquier tecla para continuar.)");
                                Console.ReadKey();
                                break;
                        }
                        break;
                    case 4:
                        bool armoraux = true;
                        int auxarmor = 0;
                        while (armoraux)
                        {
                            Console.Clear();
                            Console.WriteLine("[Mercader]--> Aquí tienes las armaduras que tengo en venta.");
                            Console.WriteLine("1.- Armadura de Hierro [100 oro]");
                            Console.WriteLine("2.- Armadura de Barion [200 oro]");
                            Console.WriteLine("3.- Armadura de Diamante [400 oro]");
                            Console.WriteLine("4.- Cancelar");
                            try
                            {
                                auxarmor = Convert.ToInt32(Console.ReadLine());
                                armoraux = false;
                                if (auxarmor == 4)
                                {
                                    Console.WriteLine("[Mercader]--> Hasta luego viajero.");
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                armoraux = true;
                                Console.WriteLine("[Mercader]--> Selecciona una armadura e intenta de nuevo.");
                                Console.WriteLine("(Presiona cualquier tecla para continuar.)");
                                Console.ReadKey();
                            }
                        }
                        switch (auxarmor)
                        {
                            case 1:
                                Items item7 = new IronArmor();
                                bool transaccion7 = player.Comprar(item7);
                                if (transaccion7)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nueva armadura.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 2:
                                Items item8 = new BarionArmor();
                                bool transaccion8 = player.Comprar(item8);
                                if (transaccion8)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nueva armadura.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 3:
                                Items item9 = new DiamondArmor();
                                bool transaccion9 = player.Comprar(item9);
                                if (transaccion9)
                                {
                                    Console.WriteLine("[Mercader]--> Disfruta de tu nueva armadura.");
                                }
                                else
                                {
                                    Console.WriteLine("[Mercader]--> No tienes suficiente oro para comprar este objeto.");
                                }
                                break;
                            case 4:
                                break;
                            default:
                                Console.WriteLine("[Mercader]--> Yo no vendo eso, intenta algo de aquí.");
                                Console.WriteLine("(Presiona cualquier tecla para continuar.)");
                                Console.ReadKey();
                                break;
                        }
                        break;
                    case 5:
                        ContinueX = false;
                        break;
                    default:
                        Console.WriteLine("[Mercader]-> Yo no vendo eso, intenta algo de aquí.");
                        Console.WriteLine("(Presiona cualquier tecla para continuar.)");
                        Console.ReadKey();
                        break;
                }

                if (auxopc != 5)
                {
                    Console.WriteLine("[Mercader]--> ¿Deseas comprar algo más? 1.- Si 2.- No");
                    int auxopc2 = Convert.ToInt32(Console.ReadLine());
                    if (auxopc2 == 1)
                    {
                        ContinueX = true;
                        auxcorrect = false;
                    }else{
                        ContinueX = false;
                        auxcorrect = false;
                    }
                }
            }
        }
        
        public void VenderMarket(Jugadores player){
        bool continuarVenta = true;
        while (continuarVenta){
            Console.Clear();
            Console.WriteLine("--__ Mercado __--");
            Console.WriteLine("__--Vendiendo en el mercado--__");
            Console.WriteLine("Selecciona el objeto que deseas vender:");

            for (int i = 0; i < player.Inventario.Count; i++){
                Console.WriteLine($"{i + 1}.- {player.Inventario[i].Name} por [{player.Inventario[i].Value*2/3} oro]");
            }
            Console.WriteLine($"{player.Inventario.Count + 1}.- Cancelar");

            int opcionVenta = 0;
            try{
                opcionVenta = Convert.ToInt32(Console.ReadLine());
            }catch(Exception){
                Console.WriteLine("Opción no válida, intenta de nuevo.");
                Console.WriteLine("Presiona cualquier tecla para continuar.");
                Console.ReadKey();
                continue;
            }

            if (opcionVenta == player.Inventario.Count + 1){
                Console.WriteLine("[Mercader]--> Hasta luego viajero.");
                continuarVenta = false;
                break;
            }

            if (opcionVenta > 0 && opcionVenta <= player.Inventario.Count){
                Items item = player.Inventario[opcionVenta - 1];
                bool transaccion = player.Vender(item);
                if (transaccion){
                    Console.WriteLine("[Mercader]--> Objeto vendido con éxito por [{(item.Value * 2) / 3}] de oro.");
                }else{
                    Console.WriteLine("[Mercader]--> No tienes este objeto en tu inventario.");
                }
            }else{
                Console.WriteLine("Opción no válida, intenta de nuevo.");
            }

            Console.WriteLine("[Mercader]--> ¿Deseas vender otro objeto? 1.- Si 2.- No");
            int auxopc2 = Convert.ToInt32(Console.ReadLine());
            if (auxopc2 == 2){
                continuarVenta = false;
            }
        }
    }
    }
}

