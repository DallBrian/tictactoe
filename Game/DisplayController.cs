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

        private int TitleWidth => Title.Split(Environment.NewLine)[1].Length;
        private int SquareWidth => 9;
        private int SquareHeight => 5;
        private int BoardWidth => (SquareWidth * 3) + 2;
        private int BoardLeftPadding => ((TitleWidth - BoardWidth) / 2) + BoardWidth;
        public string Board
        {
            get
            {
                var squares = State.Board.Squares;

                string CenteredRow(string v1, string v2, string v3)
                {
                    return $"{v1.Center(SquareWidth)}|{v2.Center(SquareWidth)}|{v3.Center(SquareWidth)}".PadLeft(BoardLeftPadding);
                }

                string LeftAlignedRow(string v1, string v2, string v3)
                {
                    return $"{v1.PadLeft(SquareWidth)}|{v2.PadLeft(SquareWidth)}|{v3.PadLeft(SquareWidth)}".PadLeft(BoardLeftPadding);
                }

                var emptyRow = LeftAlignedRow(string.Empty, string.Empty, string.Empty);
                var spacerRow = $"{string.Empty.PadLeft(BoardWidth, '-')}".PadLeft(BoardLeftPadding);

                var emptyRowsNeededAbove = (SquareHeight - 1) / 2;
                
                void AppendRow(StringBuilder sb, int i1, int i2, int i3)
                {
                    for (var i = 0; i < emptyRowsNeededAbove; i++)
                        sb.AppendLine(emptyRow);
                    sb.AppendLine(CenteredRow(squares[i1].Value, squares[i2].Value, squares[i3].Value));
                    for (var i = 0; i < emptyRowsNeededAbove - 1; i++)
                        sb.AppendLine(emptyRow);
                    sb.AppendLine(LeftAlignedRow((i1 + 1).ToString(), (i2 + 1).ToString(), (i3 + 1).ToString()));
                }

                var board = new StringBuilder();
                AppendRow(board, 0, 1, 2);
                board.AppendLine(spacerRow);
                AppendRow(board, 3, 4, 5);
                board.AppendLine(spacerRow);
                AppendRow(board, 6, 7, 8);
                return board.ToString();
            }
        }

        public string DrawMessage => "Draw! No winner this time.";
        public string WinnerMessage => $"Congrats Player {State.Winner?.Name} you won!";
        public string PlayAgainMessage => "Play Again? Y/";
        public string PlayerTurnMessage => $"It's Player {State.CurrentPlayer.Name}'s turn";
        public string InvalidPlacementMessage => $"Invalid choice options '{string.Join(',', BoardState.ValidInput)}'";
        public string PlacementOccupiedMessage => "Space is already occupied!";
        public string EnableAIMessage => "Would you like to play against the AI? Y/";

        public int WindowWidth { get; set; } = 47;
        public int WindowHeight { get; set; } = 20;

        private readonly GameState State;

        public DisplayController(GameState gameState)
        {
            State = gameState;
        }

        public void InitializeConsole()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public string? ReadPlayerInput()
        {
            return Console.ReadKey().KeyChar.ToString();
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
            displayString.AppendLine(Board);

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