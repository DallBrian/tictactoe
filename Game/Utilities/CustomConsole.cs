namespace TicTacToe.Utilities
{
    public class CustomConsole : ICustomConsole
    {
        public void Initialize()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public string? ReadKey()
        {
            return Console.ReadKey().KeyChar.ToString();
        }
        
        public void RenderScreen(string display)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(display);
        }
    }
}