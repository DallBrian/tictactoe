using TicTacToe.Models;

namespace TicTacToe.Utilities
{
    public static class Extensions
    {
        public static bool IsWinningMove(this Square[] squares, int index, string playerMark)
        {
            return squares.Where(s => s.Index != index).All(s => s.Value == playerMark);
        }
        
        public static string Center(this string str, int width)
        {
            return str.PadLeft(((width - str.Length) / 2) + str.Length).PadRight(width);
        }
    }
}
