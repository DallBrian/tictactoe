using TicTacToe.Utilities;

namespace TicTacToe
{
    public class Program
    {
        public static void Main()
        {
            var console = new CustomConsole();
            var app = new App(console);
            app.Run();
            while(app.IsRunning) { }
        }
    }
}