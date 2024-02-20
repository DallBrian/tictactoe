using System.Text;

namespace TicTacToe
{
    public class DisplayController
    {
        public string Title => "\r\n  _____ _       _____           _____         " +
                              "\r\n |_   _(_)__ __|_   _|_ _ __ __|_   _|__  ___ " +
                              "\r\n   | | | / _|___|| |/ _` / _|___|| |/ _ \\/ -_)" +
                              "\r\n   |_| |_\\__|    |_|\\__,_\\__|    |_|\\___/\\___|" +
                              "\r\n                                              ";

        public string DrawMessage => "Draw! No winner this time.";
        public string WinnerMessage => $"Congrats Player {State.Winner?.Name} you won!";
        public string PlayAgainMessage => "Play Again? Y/";
        public string PlayerTurnMessage => $"It's Player {State.CurrentPlayer.Name}'s turn";
        public string InvalidPlacementMessage => $"Invalid choice options '{string.Join(',', BoardState.ValidInput)}'";
        public string PlacementOccupiedMessage => "Space is already occupied!";
        public string EnableAIMessage => "Would you like to play against the AI? Y/";

        private readonly GameState State;

        public DisplayController(GameState gameState)
        {
            State = gameState;
        }

        public string? ReadPlayerInput()
        {
            return Console.ReadLine();
        }

        public void RenderGame()
        {
            Console.Clear();
            Console.WriteLine(GetCurrentDisplayState());
        }

        public string GetCurrentDisplayState()
        {
            var displayString = new StringBuilder();
            displayString.AppendLine(Title);
            displayString.AppendLine(State.Board.ToString());

            if (State.IsOver)
            {
                displayString.AppendLine(State.Winner is null ? DrawMessage : WinnerMessage);
                displayString.AppendLine(PlayAgainMessage);
                return displayString.ToString();
            }

            if (State.Errors.InvalidPlacement) displayString.AppendLine(InvalidPlacementMessage);
            if (State.Errors.PlacementAlreadyOccupied) displayString.AppendLine(PlacementOccupiedMessage);

            displayString.AppendLine(PlayerTurnMessage);
            return displayString.ToString().ReplaceLineEndings();
        }

        public void PromptToPlayAgainstAI()
        {
            Console.WriteLine(EnableAIMessage);
        }
    }
}
