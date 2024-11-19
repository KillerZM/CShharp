using System;
using System.Collections.Generic;
using System.Threading;

namespace Tiendita.Main
{
    public class Tienda
    {
        // Se crea el inventario de la tiendita
        public List<Productos> Inventario = new List<Productos>();
        public float ventasDelDia = 0.0f; // Aquí se almacenarán las ventas del día
        public DateTime fechaActual = DateTime.Now; // Para mostrar la fecha de las ventas

        public void Menu()
        {
            int Opc = 0;
            while (Opc != 7)  // Cambié la opción 7 para salir
            {
                Console.Clear(); // Limpiamos la consola al mostrar el menú
                Console.WriteLine("\n\t Menu");
                Console.WriteLine($"Tiendita de la esquina");
                Console.WriteLine($"\n __________-OPCIONES-_____________\n");
                Console.WriteLine("1. Ingresar Articulo");
                Console.WriteLine("2. Retirar Articulo");
                Console.WriteLine("3. Venta de articulo");
                Console.WriteLine("4. Existencias.");
                Console.WriteLine("5. _____________________________________");
                // Se deja la opcion 5 para que no cierre el programa por error
                Console.WriteLine("7. Salir");
                Console.WriteLine("8. Ventas del dia.");
                Console.WriteLine($"\n Seleccione una Opcion:");
                Opc = int.Parse(Console.ReadLine());
                if (Opc == null)
                {
                    Opc = 0;
                }
                switch (Opc)
                {
                    case 0: { Console.WriteLine("-Opcion no ingresada"); break; }
                    case 1:
                        { // Ingresar artículo
                            string Nombre; int Codigo; float Precio; string Marca;
                            int Dia; int Mes; int Year; int cantidad;
                            Console.WriteLine($"El articulo es organico? 0=Si 1=No");
                            int input1 = int.Parse(Console.ReadLine());
                            if (input1 == 0)
                            {
                                Console.WriteLine("Ingrese el Nombre: ");
                                Nombre = Console.ReadLine();
                                Console.WriteLine("Ingrese el codigo de barras ej(001):");
                                Codigo = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese la Marca:");
                                Marca = Console.ReadLine();
                                Console.WriteLine("Ingrese el precio unitario o por kilo:");
                                Precio = float.Parse(Console.ReadLine());
                                Console.WriteLine("--- FECHA DE CADUCIDAD ---");
                                Console.WriteLine("Ingrese el dia de caducidad: ");
                                Dia = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el mes de caducidad: ");
                                Mes = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el año de caducidad: ");
                                Year = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el peso del articulo:");
                                float peso = float.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese la cantidad de artículos:");
                                cantidad = int.Parse(Console.ReadLine());
                                try
                                {
                                    var Articulo = new Organicos(Nombre, Codigo, Precio, Marca, Dia, Mes, Year, peso, cantidad);
                                    Inventario.Add(Articulo);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Error: Datos Ingresados erroneamente");
                                }

                            }
                            else if (input1 == 1)
                            {
                                Console.WriteLine("Ingrese el Nombre: ");
                                Nombre = Console.ReadLine();
                                Console.WriteLine("Ingrese el codigo de barras ej(001):");
                                Codigo = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese la Marca:");
                                Marca = Console.ReadLine();
                                Console.WriteLine("Ingrese el precio unitario o por kilo:");
                                Precio = float.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el peso del articulo:");
                                float peso = float.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese la cantidad de artículos:");
                                cantidad = int.Parse(Console.ReadLine());
                                try
                                {
                                    var Articulo = new Inorganicos(Nombre, Codigo, Precio, Marca, peso, cantidad);
                                    Inventario.Add(Articulo);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Error: Datos Ingresados erroneamente");
                                }
                            }
                            Thread.Sleep(1000);
                            break;
                        }
                    case 2:  // Retirar un artículo
                        {
                            Console.WriteLine("Ingrese el código del artículo a retirar:");
                            int codigoRetirar = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese la cantidad de artículos a retirar:");
                            int cantidadRetirar = int.Parse(Console.ReadLine());
                            RetirarArticulo(codigoRetirar, cantidadRetirar);
                            Thread.Sleep(3000); // Esperar 3 segundos antes de volver al menú
                            break;
                        }
                    case 3:  // Realizar una venta
                        {
                            Console.WriteLine("Ingrese el código del artículo a vender:");
                            int codigoVenta = int.Parse(Console.ReadLine());
                            VentaArticulo(codigoVenta);
                            Thread.Sleep(3000); // Esperar 3 segundos antes de volver al menú
                            break;
                        }
                    case 4:  // Mostrar existencias
                        {
                            MostrarExistencias();
                            Thread.Sleep(10000); // Esperar 3 segundos antes de volver al menú
                            break;
                        }
                    case 7: {
                        Console.WriteLine("Saliendo...");
                        break; } // No borrar la consola al salir
                    case 8: { 
                        // Mostrar ventas del día con la fecha en formato Día/Mes/Año
                        Console.WriteLine($"Ventas del día ({fechaActual.Day}/{fechaActual.Month}/{fechaActual.Year}): {ventasDelDia}");
                        Thread.Sleep(5000); // Esperar 5 segundos para mostrar las ventas del día
                        break; }
                    default: { Console.WriteLine("Opción no válida, intente nuevamente."); Thread.Sleep(3000); break; }
                }
            }
        }

        // Método para retirar una cantidad de artículos
        public void RetirarArticulo(int codigo, int cantidad)
        {
            var articulo = Inventario.Find(a => a.Codigo == codigo);
            if (articulo != null)
            {
                if (articulo.Cantidad >= cantidad)
                {
                    articulo.Cantidad -= cantidad;
                    Console.WriteLine($"Se retiraron {cantidad} unidades del artículo con código {codigo}.");
                }
                else
                {
                    Console.WriteLine($"No hay suficiente cantidad de artículos con código {codigo}.");
                }

                if (articulo.Cantidad == 0)
                {
                    Inventario.Remove(articulo);
                }
            }
            else
            {
                Console.WriteLine($"Artículo con código {codigo} no encontrado en el inventario.");
            }
        }

        // Método para registrar una venta y acumular dinero
        public void VentaArticulo(int codigo)
        {
            var articulo = Inventario.Find(a => a.Codigo == codigo);
            if (articulo != null)
            {
                if (articulo is Organicos)
                {
                    Organicos organico = (Organicos)articulo;

                    // Verificar si el artículo está caducado
                    if (organico.EstaCaducado())
                    {
                        Console.WriteLine($"El artículo {articulo.Nombre} está caducado. No se recomienda su venta.");
                        organico.Estado = false; // Marcar como caducado
                        return;  // Si está caducado, no realizar la venta
                    }
                }

                // Realizar la venta
                Console.WriteLine($"Venta realizada: {articulo.Nombre} - Precio: {articulo.Precio}");
                ventasDelDia += articulo.Precio;
                articulo.Cantidad -= 1; // Reducir la cantidad en el inventario
                if (articulo.Cantidad == 0)
                {
                    Inventario.Remove(articulo); // El artículo se retira del inventario después de la venta
                }
                Console.WriteLine($"Total acumulado en ventas del día: {ventasDelDia}");
            }
            else
            {
                Console.WriteLine($"Artículo con código {codigo} no encontrado en el inventario.");
            }
        }

        // Método para mostrar el inventario actual
        public void MostrarExistencias()
        {
            if (Inventario.Count == 0)
            {
                Console.WriteLine("El inventario está vacío.");
            }
            else
            {
                Console.WriteLine("Artículos en el inventario:");
                foreach (var item in Inventario)
                {    //Este operador no me acuerdo como se llama pero esta cool y chiquito
                    string estado = (item is Organicos && ((Organicos)item).Estado == false) ? "Caducado" : "Disponible";
                    Console.WriteLine($"{item.Nombre} - Código: {item.Codigo} - Precio: {item.Precio} - Peso: {item.Peso} - Cantidad: {item.Cantidad} - Estado: {estado}");
                }
            }
        }

        public static void Main(string[] args)
        {
            Tienda tienda = new Tienda();
            tienda.Menu();
        }
    }

    public class Productos
    {
        public string Nombre { get; set; }
        public int Codigo { get; set; }    // Se refiere al código de barras
        public float Precio { get; set; } // Precio por si se necesitan decimales
        public string Marca { get; set; }
        public float Peso { get; set; }
        public int Cantidad { get; set; } // Nueva propiedad para cantidad de artículos
    }

    public class Organicos : Productos
    {
        public bool Estado { get; set; } // Indica si está listo para venderse
        public int CadDia { get; set; }
        public int CadMes { get; set; }
        public int CadYear { get; set; }

        public Organicos(string Nombre, int Codigo, float Precio, string Marca, int CadDia, int CadMes, int CadYear, float Peso, int cantidad)
        {
            this.Nombre = Nombre;
            this.Codigo = Codigo;
            this.Precio = Precio;
            this.Marca = Marca;
            this.CadDia = CadDia;
            this.CadMes = CadMes;
            this.CadYear = CadYear;
            this.Peso = Peso;
            this.Cantidad = cantidad;
            this.Estado = true; // Por defecto, los artículos no están caducados
        }

        public bool EstaCaducado()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaCaducidad = new DateTime(CadYear, CadMes, CadDia);
            return fechaActual > fechaCaducidad;
        }
    }

    public class Inorganicos : Productos
    {
        public Inorganicos(string Nombre, int Codigo, float Precio, string Marca, float Peso, int cantidad)
        {
            this.Nombre = Nombre;
            this.Codigo = Codigo;
            this.Precio = Precio;
            this.Marca = Marca;
            this.Peso = Peso;
            this.Cantidad = cantidad;
        }
    }
}
