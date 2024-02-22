using TicTacToe.Models;

namespace TicTacToe.Utilities
{
    public static class Extensions
    {
        public static bool IsWinningMove(this Square[] squares, int index, string playerMark)
        {
            return squares.Where(s => s.Index != index).All(s => s.Value == playerMark);
        }

        public static bool DoesMoveCauseAWin(this BoardState board, int index, string playerMark)
        {
            if (board.GetColAt(index).IsWinningMove(index, playerMark)) return true;
            if (board.GetRowAt(index).IsWinningMove(index, playerMark)) return true;
            if (board.GetLeftDiagonalSquares().IsWinningMove(index, playerMark)) return true;
            if (board.GetRightDiagonalSquares().IsWinningMove(index, playerMark)) return true;
            return false;
        }

        public static int PossibleWins(this BoardState board, int index, string playerMark)
        {
            int wins = 0;
            if (board.GetRowAt(index).All(s => s.IsEmpty || s.Value == playerMark)) wins++;
            if (board.GetColAt(index).All(s => s.IsEmpty || s.Value == playerMark)) wins++;
            if (board.GetLeftDiagonalContaining(index) is not null)
                if (board.GetLeftDiagonalSquares().All(s => s.IsEmpty || s.Value == playerMark))
                    wins++;
            if (board.GetRightDiagonalContaining(index) is not null)
                if (board.GetRightDiagonalSquares().All(s => s.IsEmpty || s.Value == playerMark))
                    wins++;
            return wins;
        }
        
        public static string Center(this string str, int width)
        {
            return str.PadLeft(((width - str.Length) / 2) + str.Length).PadRight(width);
        }
    }
}
