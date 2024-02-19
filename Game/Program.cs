namespace TicTacToe // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                var game = new Game();
                while (!game.IsOver)
                {
                    game.ReadInput();
                }

                game.Prompt(game.Winner is null
                    ? "Draw! No winner this time."
                    : $"Congrats Player {game.Winner} you won!");
                Console.WriteLine("Play Again? Y/");
            } while (Console.ReadLine().ToLower() == "y");
        }
    }
}