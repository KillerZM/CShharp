using System;
namespace Ejercicio2Ex
{
    public class CholtunA{
        public int kin,uinal,tunes,katun,baktun;
        public int transfUnial(int Unidad){
            uinal = Unidad/20;
            return (Unidad%20);
        }
        public int transfTun(int Unidad){
            tunes = Unidad/(20*18);
            return (Unidad%(20*18));
        }
        public int transfKatun(int Unidad){
            katun = Unidad/(20*18*20);
            return (Unidad%(20*18*20));
        }
        public int transfBaktun(int Unidad){
            baktun = Unidad/(20*18*20*20);
            return (Unidad%(20*18*20*20));
        }
        public void Mostrar(){
            Console.WriteLine("Baktun: "+ baktun);
            Console.WriteLine("Katun: "+ katun);
            Console.WriteLine("Tun: "+ tunes);
            Console.WriteLine("uinal: "+ uinal);
            Console.WriteLine("Kin: "+ kin);
        }
        public CholtunA(int Unidad){
            kin = Unidad;
        }
        public void Transformacion(){
            kin = transfBaktun(kin);
            kin = transfKatun(kin);
            kin = transfTun(kin);
            kin = transfUnial(kin);

        }
    }
    class ejercicio1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la cantidad de (Dias o Kines): ");
            int Unidad = int.Parse(Console.ReadLine());
            var Object = new CholtunA(Unidad);
            Object.Transformacion();
            Object.Mostrar();
        }
    }
}