using System;
using System.Diagnostics;

namespace Caracol1{
public class Caracol{
    int S,B,P,R,D;

    public Caracol(int fuerza,int debilidad,int profundidad){
        S= fuerza;
        B= debilidad;
        P= profundidad;
        R= 0;//RECORRIDO
    }

    public void subir(){
        R = R+S; //Escalar
    }

    public void resbalar(){
        R = R-B;
    }

    public void chambear(){
        D +=1;
    }


    public static void Main(){
    int auxF,auxB,auxP;
    Console.WriteLine("\n\t Ingrese la fuerza del caracol: ");
    auxF = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\n\t Ingrese lo que baja el caracol: ");
    auxB = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\n\t Ingrese la profundiad del agujero: ");
    auxP = Convert.ToInt32(Console.ReadLine());

    Caracol Turbo = new Caracol(auxF,auxB,auxP);
    bool UwU= false;

    while(UwU==false){
            if (Turbo.S <= Turbo.B){
                Console.WriteLine("\n\t El caracol nunca saldrá");
                break;
            }
            //Turbo.chambear();
            Turbo.subir();
            if(Turbo.R>=Turbo.P){   
                Console.WriteLine("\n\tEl caracol salio en: "+ Turbo.D + " Dias."+ "\n");
                break;
            }
            Turbo.chambear();
            Turbo.resbalar();

            Console.WriteLine("\n\t Inicia un nuevo dia \n");

    }

}
}
}