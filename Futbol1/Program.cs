using System;
using System.Threading;
using System.Collections.Generic;
//using Juego_futbol;

namespace Juego_futbol{
    public class Jugador{
            //Se crea la lista de manera global aqui.
            public static List<Jugador> jugadores = new List<Jugador>();
            public static Jugador Portero;
            
            Random random= new Random();

            public int numero;
            public bool balon;
            public string nombre;
            public string posX;

            public int goles;

            public void pasar(Jugador Receptor){
                this.balon = false;
                Receptor.balon = true;

                Console.WriteLine("\n !!! --> El jugador "+ this.nombre + " Pasó el balon a "+ Receptor.nombre);
            }
            public void Robar(Jugador Victima){
                this.balon = true;
                Victima.balon = false;

                Console.WriteLine("\n !!! --> El jugador "+ this.nombre + " Robó el balon a "+ Victima.nombre);
            }

            public bool Posicion_Balon(){
                foreach(Jugador jugador in jugadores){
                    if(jugador.balon==true){
                    Console.WriteLine("\n\t !!! --> El jugador "+ jugador.nombre + "Tiene el balon");
                    return jugador.balon;
                    }
                }
                Console.WriteLine("\n !!! --> Ups parece que el balon no existe");
                return false;

            }

            //CON este metodo el jugador puede intentar anotar gol a la porteria
            //Dependiendo de su pocicion en el campo puede tener mas chance a anotar o menos.
            public void TiroGol(){
                int prob = random.Next(1,10); //crea un numero aleatorio del 1 al 10
                if(balon == false  || posX=="Defensa" || posX=="Portero"){
                    Console.WriteLine("\n\t !!! --> El jugador "+ nombre + " No cumple los requisitos para realizar un Tiro a porteria");
                    return;
                }
                if(posX=="Delantero"){
                    if(prob<=7){
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre + " Anotó gol!!! y ahora el portero "+ Portero.nombre +"ahora tiene el balon.");
                        goles = goles+1;
                        balon = false;
                        Portero.balon=true;
                    }else{
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre + " Falló el tiro :(,  y ahora el portero "+ Portero.nombre +"ahora tiene el balon.");
                        balon = false;
                        Portero.balon=true;
                    }
                }else if(posX=="Medio"){
                    if(prob<=5){
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre + " Anotó gol!!! y ahora el portero "+ Portero.nombre +"ahora tiene el balon.");
                        goles = goles+1;
                        balon = false;
                        Portero.balon=true;
                    }
                    else{
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre + " Falló el tiro :(,  y ahora el portero "+ Portero.nombre +"ahora tiene el balon.");
                        balon = false;
                        Portero.balon=true;
                    }
                }
            }

            //Con este un jugador puede cambiar posicion con otro siempre y cuando lo tenga al lado
            // El portero no puede cambiar posicion
            public void Cambiar_Posicion(Jugador Cambio){
                if(Cambio.posX=="Portero"){
                    Console.WriteLine("\n\t !!! --> No se puede cambiar posición con el portero.");
                    return;
                }
                string aux;
                if(posX=="Defensa"){
                    if(Cambio.posX=="Medio"){
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre +" Cambio de posicion con el jugador: "+ Cambio.nombre);
                        aux = posX;
                        posX = Cambio.posX;
                        Cambio.posX = aux;
                    }else{
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre +" no pudo cambiar posicion con "+ Cambio.nombre + " por que está demasiado lejos.");
                    }
                }else if(posX=="Medio"){
                    if(Cambio.posX=="Medio" || Cambio.posX=="Delantero"){
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre +" Cambio de posicion con el jugador: "+ Cambio.nombre);
                        aux = posX;
                        posX = Cambio.posX;
                        Cambio.posX = aux;
                    }else{
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre +" no pudo cambiar posicion con "+ Cambio.nombre + " por que está demasiado lejos.");
                    }
                }else if(posX=="Delantero"){
                    if(Cambio.posX=="Medio"){
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre +" Cambio de posicion con el jugador: "+ Cambio.nombre);
                        aux = posX;
                        posX = Cambio.posX;
                        Cambio.posX = aux;
                    }else{
                        Console.WriteLine("\n\t !!! --> El jugador "+ nombre +" no pudo cambiar posicion con "+ Cambio.nombre + " por que está demasiado lejos.");
                    }
                }
            }            

            public void Mostrar_stats(){
                Console.WriteLine("\n !!! --> Estadisticas de el jugador: "+ nombre+" Son\n\n");
                Console.WriteLine("Nombre: "+nombre+" \n Numero: "+ numero +"\n Posición: "+ posX +"\n Goles: "+ goles+"\n Balon: "+ (balon ? "Sí" : "No")+ "\n\n");
            }

            public Jugador(string name, int numero, bool balon,string pos){
                this.nombre = name;
                this.numero = numero;
                this.balon = balon;
                this.posX = pos;
                goles = 0;

                jugadores.Add(this);
            }
    }
    public class Partido{
        public static void Main(){
            List<Jugador> jugadores = new List<Jugador>();
            Jugador.Portero = new Jugador("Iker", 1, false, "Portero");
            jugadores.Add(Jugador.Portero); // Añadir el nuevo jugador a la lista

            int opcion=0;
            
            //Creacion de jugadores
            for(int i=0;i<2;i++){
                string Nom="";
                int Num=0;
                string PosI="";
                Console.WriteLine("\n\tIngrese el nombre del jugador "+ (i+1) +": ");
                Nom = Console.ReadLine();
                Console.WriteLine("\n\tIngrese el numero del jugador "+ (i+1) +": ");
                Num = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\t Posiciones disponibles: {1.Defensa.  2.Medio.   3.Deltantero}");
                Console.WriteLine("\n\tIngrese posicion del jugador "+ (i+1) +": ");
                PosI = Console.ReadLine();

                Jugador nuevoJugador = new Jugador(Nom,Num,false,PosI);
                jugadores.Add(nuevoJugador); // Añadir el nuevo jugador a la lista
            }

            Console.WriteLine("Recuerda que el jugador"+ Jugador.Portero.nombre+ " Siempre inicia con el balon");

            do{
                Console.WriteLine("\n\n\t --Opciones del partido-- \n");
                Console.WriteLine("1. Pasar Balon ");
                Console.WriteLine("2. Robar Balon ");
                Console.WriteLine("3. Posicion Balon ");
                Console.WriteLine("4. Tiro gol ");
                Console.WriteLine("5. Cambiar posicion ");
                Console.WriteLine("6. Mostrar estadisticas ");
                Console.WriteLine("7. Salir \n");
                Console.WriteLine("Seleccione una opcion-> ");
                opcion= Convert.ToInt32(Console.ReadLine());
                string aux1="",aux2="";
                switch(opcion){
                    

                    case 1: {
                        Console.WriteLine("Que jugador Tiene el balon: ");
                        aux1= Console.ReadLine();
                        Console.WriteLine("A que jugador se lo pasará: ");
                        aux2= Console.ReadLine();

                        Jugador Player1 = Jugador.jugadores.Find(j => j.nombre == aux1);//Busca un jugador con ese nombre en la lista
                        Jugador Player2 = Jugador.jugadores.Find(j => j.nombre == aux2);//Busca un jugador con ese nombre en la lista
                        if(Player1 != null && Player2 != null){
                            Player1.pasar(Player2);
                        }else{
                            if (Player1 == null) {
                                Console.WriteLine("\n !!! -->El juagador" + aux1 + " no fue encontrado.");
                            }
                            if (Player2 == null) {
                                Console.WriteLine("\n !!! -->El jugador " + aux2 + " no fue encontrado.");
                            }
                        }
                        break;
                    }
                    case 2: {
                        Console.WriteLine("\n !!! -->Que jugador robara el balon: ");
                        aux1= Console.ReadLine();
                        Console.WriteLine("\n !!! -->A que jugador se lo robara: ");
                        aux2= Console.ReadLine();

                        Jugador Player1 = Jugador.jugadores.Find(j => j.nombre == aux1);//Busca un jugador con ese nombre en la lista
                        Jugador Player2 = Jugador.jugadores.Find(j => j.nombre == aux2);//Busca un jugador con ese nombre en la lista
                        if(Player1 != null && Player2 != null){
                            Player1.Robar(Player2);
                        }else{
                            if (Player1 == null) {
                                Console.WriteLine("\n !!! --> El jugador " + aux1 + " no fue encontrado.");
                            }
                            if (Player2 == null) {
                                Console.WriteLine("\n !!! --> El jugador " + aux2 + " no fue encontrado.");
                            }
                        }
                        break;
                    }
                    case 3: {
                        //Aqui solo se usa el portero para llamar el metodo de balon
                        bool uwu = Jugador.Portero.Posicion_Balon();
                        Console.WriteLine("\n");
                        break;
                    }
                    case 4: {
                        Console.WriteLine("\n !!! -> Que jugador tirará: ");
                        aux1= Console.ReadLine();
                        Jugador Player1 = Jugador.jugadores.Find(j=> j.nombre == aux1);
                        if(Player1 != null){
                            Player1.TiroGol();
                        }else{
                            if (Player1 == null) {
                                Console.WriteLine("\n !!! --> El jugador " + aux1 + " no fue encontrado.");
                            }
                        }
                        break;
                    }
                    case 5: {
                        Console.WriteLine("\n !!! -> Que jugador esta en pos 1: ");
                        aux1=Console.ReadLine();
                        Console.WriteLine("\n !!! -> Que jugador esta en pos 2: ");
                        aux1=Console.ReadLine();
                        Jugador Player1= Jugador.jugadores.Find(j => j.nombre == aux1);
                        Jugador Player2= Jugador.jugadores.Find(j => j.nombre == aux2);
                        if(Player1 != null && Player2 != null){
                            Player1.Cambiar_Posicion(Player2);
                        }else{
                            if (Player1 == null) {
                                Console.WriteLine("\n !!! --> El jugador " + aux1 + " no fue encontrado.");
                            }
                            if (Player2 == null) {
                                Console.WriteLine("\n !!! --> El jugador " + aux2 + " no fue encontrado.");
                            }
                        }
                        break;
                    }
                    case 6: {
                        Console.WriteLine("\n !!! -> De quien quieres ver estadisticas: ");
                        aux1= Console.ReadLine();
                        Jugador Player1= Jugador.jugadores.Find(j => j.nombre == aux1);
                        if(Player1 != null){
                            Player1.Mostrar_stats();
                        }else{
                            if (Player1 == null) {
                                Console.WriteLine("\n !!! --> El jugador " + aux1 + " no fue encontrado.");
                            }
                        }
                        break;
                    }
                    case 7: {
                        //Mensaje de salida progresivo patrocinado por chatgpt
                        string mensaje = "\n!!! -> Saliendo";
                        for (int i = 0; i < 3; i++) {
                            Console.Write(mensaje);
                            for (int j = 0; j <= i; j++) {
                                Console.Write(".");
                                Thread.Sleep(100); // Pausa de 500 milisegundos
                            }
                            if (i < 2) {
                                Console.Write("\r"); // Retorno de carro para sobrescribir la línea
                                Thread.Sleep(100);
                            }
                        }
                        break;
                    }
                    default: {
                        Console.WriteLine("\n !!! -> Selecciona una opcion valida.");
                        break;
                    }
                    }
            }while(opcion != 7);
        }
    }
}
