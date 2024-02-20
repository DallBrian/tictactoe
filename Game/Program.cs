namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);
            var game = new GameController(gameState);

            do
            {
                game.NewGame();
                display.RenderGame();
                while (!game.IsOver)
                {
                    var playerInput = display.ReadPlayerInput();
                    game.MakeMove(playerInput);
                    display.RenderGame();
                }
            } while (display.ReadPlayerInput()?.ToLower() == "y");
        }
    }
}