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
            if (!IsValidMove(placement)) return;

            var index = int.Parse(placement) - 1;
            MarkBoardForCurrentPlayer(index);
            CheckIfPlayerWon(index);
            CheckIfNoValidMovesLeft();
            SwitchPlayers();
        }

        private bool IsValidMove(string? placement)
        {
            State.Errors.Clear();

            if (!BoardState.ValidInput.Contains(placement))
            {
                State.Errors.InvalidPlacement = true;
                return false;
            }

            var oneBasedIndex = int.Parse(placement);
            if (!State.Board.Squares[oneBasedIndex - 1].IsEmpty)
            {
                State.Errors.PlacementAlreadyOccupied = true;
                return false;
            }

            return true;
        }

        private void MarkBoardForCurrentPlayer(int index)
        {
            State.Board.Squares[index].Value = State.CurrentPlayer.Mark;
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

    }
}