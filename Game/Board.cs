namespace TicTacToe
{
    public class Board
    {
        private readonly (int Left, int Top) cursorPosition;

        public Board((int Left, int Top) cursorPosition = default)
        {
            this.cursorPosition = cursorPosition;
        }

        public static string[] ValidInput = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public Square[] Squares = { new(0), new(1), new(2), new(3), new(4), new(5), new(6), new(7), new(8) };

        public string[] Values => Squares.Select(s => s.Value).ToArray();

        public string[] GetRowContaining(int index)
        {
            return Squares.Where(s => s.Row == Squares[index].Row).Select(s => s.Value).ToArray();
        }

        public string[] GetColContaining(int index)
        {
            return Squares.Where(s => s.Column == Squares[index].Column).Select(s => s.Value).ToArray();
        }

        public string[]? GetLeftDiagonalContaining(int index)
        {
            return Squares[index].IsLeftDiagonal
                ? Squares.Where(s => s.IsLeftDiagonal).Select(s => s.Value).ToArray()
                : null;
        }

        public string[]? GetRightDiagonalContaining(int index)
        {
            return Squares[index].IsRightDiagonal
                ? Squares.Where(s => s.IsRightDiagonal).Select(s => s.Value).ToArray()
                : null;
        }

        public void Refresh()
        {
            Console.SetCursorPosition(cursorPosition.Left, cursorPosition.Top);
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"   |   |\r\n" +
                   $" {Squares[0]} | {Squares[1]} | {Squares[2]}\r\n" +
                   $"  1|  2|  3\r\n" +
                   $"-----------\r\n" +
                   $"   |   |\r\n" +
                   $" {Squares[3]} | {Squares[4]} | {Squares[5]}\r\n" +
                   $"  4|  5|  6\r\n" +
                   $"-----------\r\n" +
                   $"   |   |\r\n" +
                   $" {Squares[6]} | {Squares[7]} | {Squares[8]}\r\n" +
                   $"  7|  8|  9\r\n";
        }
    }

    public class Square
    {
        public int Row { get; }
        public int Column { get; }
        public bool IsLeftDiagonal { get; }
        public bool IsRightDiagonal { get; }

        public string Value { get; set; }

        public Square(int index)
        {
            Value = string.Empty;
            Row = index switch
            {
                0 or 1 or 2 => 1,
                3 or 4 or 5 => 2,
                6 or 7 or 8 => 3,
                _ => throw new(),
            };
            Column = index switch
            {
                0 or 3 or 6 => 1,
                1 or 4 or 7 => 2,
                2 or 5 or 8 => 3,
                _ => throw new(),
            };
            IsLeftDiagonal = index is 0 or 4 or 8;
            IsRightDiagonal = index is 2 or 4 or 6;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Value) ? " " : Value;
        }
    }
}
