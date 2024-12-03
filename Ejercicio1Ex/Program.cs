using System;
namespace Ejercicio1Ex
{
    public class UnidadB{
        public int m,f,y,p,q;
        public int transfPies(int Unidad){
            p = Unidad/12;
            return (Unidad%12);
        }
        public int transfYardas(int Unidad){
            y = Unidad/(3*12);
            return (Unidad%(3*12));
        }
        public int transfurlongs(int Unidad){
            f = Unidad/(3*12*220);
            return (Unidad%(3*12*220));
        }
        public int transfMillas(int Unidad){
            m = Unidad/(3*12*220*8);
            return (Unidad%(3*12*220*8));
        }
        public void Mostrar(){
            Console.WriteLine("Millas: "+m);
            Console.WriteLine("Furlongs: "+f);
            Console.WriteLine("Yardas: "+y);
            Console.WriteLine("Pies: "+p);
            Console.WriteLine("Pulgadas: "+q);
        }
        public UnidadB(int Unidad){
            q = Unidad;
        }
        public void Transformacion(){
            q = transfMillas(q);
            q = transfurlongs(q);
            q = transfYardas(q);
            q = transfPies(q);

        }
    }
    class ejercicio1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la cantidad de pulgadas: ");
            int Unidad = int.Parse(Console.ReadLine());
            var Object = new UnidadB(Unidad);
            Object.Transformacion();
            Object.Mostrar();
        }
    }
}