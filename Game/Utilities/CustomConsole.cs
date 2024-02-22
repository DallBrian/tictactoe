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
            //Weird issue using the terminal app causing clear to not clear everything https://stackoverflow.com/questions/75471607/console-clear-doesnt-clean-up-the-whole-console
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
            Console.WriteLine(display);
        }
    }
}