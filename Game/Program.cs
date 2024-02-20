namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);
            var game = new GameController(gameState);
            var ai = new AIController(gameState);

            display.PromptToPlayAgainstAI();
            var playerInput = display.ReadPlayerInput();
            if (playerInput.IsConfirmation())
                game.EnableAI();

            do
            {
                game.NewGame();
                display.RenderGame();
                while (!game.IsOver)
                {
                    playerInput = gameState.CurrentPlayer.IsAI
                        ? ai.DetermineMove().ToString()
                        : display.ReadPlayerInput();
                    game.MakeMove(playerInput);
                    display.RenderGame();
                }
            } while (display.ReadPlayerInput().IsConfirmation());
        }
    }
}