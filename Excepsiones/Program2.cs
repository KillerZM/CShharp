using System;
namespace Exception2{
    class Program{
        static void Main(string[] args){
            try{
                Console.WriteLine("Ingrese un valor:");
                string linea= Console.ReadLine();
                var num = int.Parse(linea);
                var cuadrado= num*num;
                Console.WriteLine($"El cuadrado de {num} es {cuadrado}");

                Console.WriteLine($"Ingrese el Dividendo");
                var Dividendo = int.Parse(Console.ReadLine());
                Console.WriteLine($"Ingrese el Divisor");
                var Divisor = int.Parse(Console.ReadLine());
                var Resultado = Dividendo/Divisor;

                Console.WriteLine($"La division es: {Resultado}");
            }
            catch(FormatException e){
                Console.WriteLine("Debes ingresar obligatoriamente un numero");
            }
            catch(DivideByZeroException e2){
                Console.WriteLine("Debes ingresar un divisor distinto de Cero.");
            }
            Console.ReadKey();
        }
    }
}