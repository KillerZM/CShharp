using System;

namespace Herencia{
    public class Persona{
        public string Name{get;set;}
        public int Age{get;  set;}

        public void Print(){
            Console.WriteLine("Nombre: "+ Name);
            Console.WriteLine("Edad: "+ Age);
        }
    }
    public class Empleado : Persona{
        public float sueldo{get;set;}

        new public void Print(){
            base.Print();
            Console.WriteLine("Sueldo: "+ sueldo);
        }
    }

    public class Student: Persona{
        public float Promedio{get;set;}
        public int Estudiado{get;set;}
        public void Study(){
            Console.WriteLine(Name +" Estudió.");
            Estudiado += 1;
        }
        new public void Print(){
            base.Print();
            Console.WriteLine("Promedio: " + Promedio);
            Console.WriteLine("Estudio: "+ Estudiado);
        }
    }

    class Test{
        static void Main(string[] args) {
            Persona persona = new Persona();
            persona.Name = "Juan";
            persona.Age = 25;
            persona.Print();
            Console.WriteLine();
            Empleado toji= new Empleado();
            toji.Name = "Toji";
            toji.Age = 25;
            toji.sueldo = 100000;
            toji.Print();
            Console.WriteLine();

            Student Yat = new Student();
            Yat.Name = "Yatziri";
            Yat.Age = 21;
            Yat.Promedio = 9;
            Yat.Estudiado = 0;
            Yat.Study();
            Yat.Print();


        }
    }

}