using DungeonBS.Controllers;

namespace DungeonBS
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
            GameController juego = new GameController();
            juego.IniciarJuego();
            Console.WriteLine("Programa finalizado. Presiona cualquier tecla para salir..."); 
            Console.ReadLine();
            } catch (Exception ex)
            { Console.WriteLine($"Se produjo un error: {ex.Message}"); Console.ReadLine(); // Espera a que el usuario presione una tecla antes de cerrar }
        }

    }
}
}
