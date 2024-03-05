using System.Text;
using TicTacToe.Models;
using TicTacToe.Utilities;

namespace TicTacToe.Controllers
{
    public class DisplayController
    {
        public string Title => "  _____ _       _____           _____         \r\n" +
                               " |_   _(_)__ __|_   _|_ _ __ __|_   _|__  ___ \r\n" +
                               "   | | | / _|___|| |/ _` / _|___|| |/ _ \\/ -_)\r\n" +
                               "   |_| |_\\__|    |_|\\__,_\\__|    |_|\\___/\\___|\r\n" +
                               "                                              \r\n";

        private int TitleWidth => Title.Split(Environment.NewLine)[1].Length;
        private int SquareWidth => (int)Math.Round(SquareHeight * 1.5, 0);
        private int SquareHeight { get; set; } = 5;

        private int BoardWidth => SquareWidth * 3 + 2;
        private int BoardLeftPadding => (TitleWidth - BoardWidth) / 2 + BoardWidth;
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
                    sb.AppendLine(LeftAlignedRow(squares[i1].GetDisplayValue(State.CurrentLayout),
                        squares[i2].GetDisplayValue(State.CurrentLayout),
                        squares[i3].GetDisplayValue(State.CurrentLayout)));
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

        public string DrawMessage => "Draw! No winner this time.".Center(TitleWidth);
        public string WinnerMessage => $"Congrats {State.Winner?.Name} you won!".Center(TitleWidth);
        public string AIWinsMessage => $"{State.CurrentPlayer.Name} lost to the AI".Center(TitleWidth);
        public string PlayerTurnMessage => $"It's {State.CurrentPlayer.Name}'s turn".Center(TitleWidth);
        public string InvalidPlacementMessage => $"Invalid choice options '{string.Join(',', BoardState.ValidInput)}'".Center(TitleWidth);
        public string PlacementOccupiedMessage => "Space is already occupied!".Center(TitleWidth);

        public string NewGameOption => "1 New Game".Center(TitleWidth);
        public string DisableAIToggle => "2 Disable AI".Center(TitleWidth);
        public string EnableAIOption => "2 Enable AI".Center(TitleWidth);
        public string ToggleAIOption => State.NextPlayer.IsAI || State.CurrentPlayer.IsAI ? DisableAIToggle : EnableAIOption;
        public string ResumeOption => "3 Resume".Center(TitleWidth);
        public string CurrentLayoutOption => $"3 Layout: {State.CurrentLayout}".Center(TitleWidth);

        private readonly GameState State;

        public DisplayController(GameState gameState)
        {
            State = gameState;
        }

        public void EnlargeBoard()
        {
            SquareHeight+=2;
        }

        public void ShrinkBoard()
        {
            if (SquareHeight == 3) return;
            SquareHeight-=2;
        }

        public string GetCurrentDisplayState()
        {
            var displayString = new StringBuilder();
            displayString.AppendLine(Title);
            displayString.AppendLine(Board);

            if (State.IsOver)
            {
                displayString.AppendLine(State.Winner is null ? DrawMessage :
                    State.Winner.IsAI ? AIWinsMessage : WinnerMessage);
            }

            if (State is { IsActiveGame: true, IsPaused: false })
            {
                if (State.Errors.InvalidPlacement) displayString.AppendLine(InvalidPlacementMessage);
                if (State.Errors.PlacementAlreadyOccupied) displayString.AppendLine(PlacementOccupiedMessage);

                displayString.AppendLine(PlayerTurnMessage);
                return displayString.ToString().ReplaceLineEndings();
            }
            displayString.AppendLine(NewGameOption);
            displayString.AppendLine(ToggleAIOption);
            displayString.AppendLine(State is { IsActiveGame: true, IsPaused: true } ? ResumeOption : CurrentLayoutOption);
            return displayString.ToString().ReplaceLineEndings();
        }
    }
}