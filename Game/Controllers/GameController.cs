using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class GameController
    {
        public GameController(GameState gameState)
        {
            State = gameState;
        }

        private GameState State { get; }

        public bool IsOver => State.IsOver;

        public Player? Winner => State.Winner;

        public BoardState Board => State.Board;

        public void MakeMove(string? placement)
        {
            var matchingDisplaySquare =
                State.Board.Squares.SingleOrDefault(s => s.GetDisplayValue(State.CurrentLayout) == placement);

            if (!IsValidMove(matchingDisplaySquare)) return;
            if (!IsNotOccupied(matchingDisplaySquare)) return;

            MarkBoardForCurrentPlayer(matchingDisplaySquare);
            CheckIfPlayerWon(matchingDisplaySquare.Index);
            CheckIfNoValidMovesLeft();
            SwitchPlayers();
        }

        private bool IsNotOccupied(Square square)
        {
            State.Errors.Clear();
            if (square.IsEmpty) return true;
            State.Errors.PlacementAlreadyOccupied = true;
            return false;
        }

        private bool IsValidMove(Square? square)
        {
            State.Errors.Clear();
            if (square is not null) return true;
            State.Errors.InvalidPlacement = true;
            return false;
        }

        private void MarkBoardForCurrentPlayer(Square square)
        {
            square.Value = State.CurrentPlayer.Mark;
        }

        private void CheckIfPlayerWon(int index)
        {
            if (!MadeWinningMove(index)) return;

            State.IsOver = true;
            State.IsActiveGame = false;
            State.Winner = State.CurrentPlayer;
        }

        private void CheckIfNoValidMovesLeft()
        {
            if (State.Board.Squares.Any(s => s.Value == string.Empty)) return;
            State.IsOver = true;
            State.IsActiveGame = false;
        }

        private bool MadeWinningMove(int index)
        {
            var playerChar = State.CurrentPlayer.Mark;
            return State.Board.GetRowContaining(index).All(s => s.Equals(playerChar)) ||
                   State.Board.GetColContaining(index).All(s => s.Equals(playerChar)) ||
                   (State.Board.GetLeftDiagonalContaining(index)?.All(s => s.Equals(playerChar)) ?? false) ||
                   (State.Board.GetRightDiagonalContaining(index)?.All(s => s.Equals(playerChar)) ?? false);
        }

        private void SwitchPlayers()
        {
            (State.CurrentPlayer, State.NextPlayer) = (State.NextPlayer, State.CurrentPlayer);
        }
        
        public void PauseGame()
        {
            State.IsPaused = true;
        }

        public void ResumeGame()
        {
            State.IsPaused = false;
        }

        public void NewGame()
        {
            State.IsActiveGame = true;
            if (State.CurrentPlayer.Id != 1) SwitchPlayers();
            State.Errors.Clear();
            State.IsOver = false;
            State.Winner = null;
            State.Board = new();
        }

        public void ToggleAI()
        {
            //Always targeting player 2 as the AI
            if (State.NextPlayer.Id == 2)
                State.NextPlayer.IsAI = !State.NextPlayer.IsAI;
            else
                State.CurrentPlayer.IsAI = !State.CurrentPlayer.IsAI;
        }

        public void SwitchLayout()
        {
            var indexOfCurrentLayout = Layout.Layouts.ToList().IndexOf(State.CurrentLayout);
            var indexOfNextLayout = indexOfCurrentLayout + 1 > Layout.Layouts.Length - 1 ? 0 : indexOfCurrentLayout + 1;
            State.CurrentLayout = Layout.Layouts[indexOfNextLayout];
        }
    }
}