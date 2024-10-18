using System;

namespace Juego_futbol{
    public class jugador{
        public:
            int numero;
            bool balon;
            string nombre;
            int posX;

            public pasar(){}

            public jugador(string name, int numero, bool balon,int pos){
                this.nombre = name;
                this.numero = numero;
                this.balon = balon;
                this.posX = pos;
            }
        private:
    }
    public class partido{
        public static void main(){
            jugador Jugador1= new jugador("Messi",10,FALSE,4);
        }
    }
}
