using TicTacToe.Models;
using TicTacToe.Utilities;

namespace TicTacToe.Controllers
{
    public class AIController
    {
        private readonly GameState State;

        public AIController(GameState state)
        {
            State = state;
        }

        public int DetermineMove()
        {
            var topIndex = -1;
            var totalPossibleWins = -1;

            for (var i = 0; i < 9; i++)
            {
                if (!State.Board.Squares[i].IsEmpty) continue;
                if (State.Board.DoesMoveCauseAWin(i, State.CurrentPlayer.Mark)) return i;
                if (State.Board.DoesMoveCauseAWin(i, State.NextPlayer.Mark)) return i;

                var wins = State.Board.PossibleWins(i, State.CurrentPlayer.Mark);
                if (wins <= totalPossibleWins) continue;
                topIndex = i;
                totalPossibleWins = wins;
            }

            return topIndex;
        }
    }
}
