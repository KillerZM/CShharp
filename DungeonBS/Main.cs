using DungeonBS.Controllers;

namespace DungeonBS
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController juego = new GameController();
            juego.IniciarJuego();
        }
    }
}
