namespace TicTacToe.Models
{
    public class BoardState
    {
        public static readonly string[] ValidInput = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public Square[] Squares = { new(0), new(1), new(2), new(3), new(4), new(5), new(6), new(7), new(8) };
        
        public string[] Values => Squares.Select(s => s.Value).ToArray();

        public Square[] GetRowAt(int index)
        {
            return Squares.Where(s => s.Row == Squares[index].Row).ToArray();
        }

        public string[] GetRowContaining(int index)
        {
            return GetRowAt(index).Select(s => s.Value).ToArray();
        }

        public Square[] GetColAt(int index)
        {
            return Squares.Where(s => s.Column == Squares[index].Column).ToArray();
        }

        public string[] GetColContaining(int index)
        {
            return GetColAt(index).Select(s => s.Value).ToArray();
        }

        public Square[] GetLeftDiagonalSquares()
        {
            return Squares.Where(s => s.IsLeftDiagonal).ToArray();
        }

        public string[]? GetLeftDiagonalContaining(int index)
        {
            return Squares[index].IsLeftDiagonal
                ? GetLeftDiagonalSquares().Select(s => s.Value).ToArray()
                : null;
        }

        public Square[] GetRightDiagonalSquares()
        {
            return Squares.Where(s => s.IsRightDiagonal).ToArray();
        }

        public string[]? GetRightDiagonalContaining(int index)
        {
            return Squares[index].IsRightDiagonal
                ? GetRightDiagonalSquares().Select(s => s.Value).ToArray()
                : null;
        }
    }

    public class Square
    {
        public int Index { get; }

        public string GetDisplayValue(string layout)
        {
            return layout switch
            {
                Layout.OneBased => (Index + 1).ToString(),
                Layout.NumPad => Index switch
                {
                    0 => "7",
                    1 => "8",
                    2 => "9",
                    3 => "4",
                    4 => "5",
                    5 => "6",
                    6 => "1",
                    7 => "2",
                    8 => "3"
                },
                _ => throw new NotImplementedException($"Layout '{layout}' is not implemented."),
            };
        }

        public int Row { get; }

        public int Column { get; }

        public bool IsLeftDiagonal { get; }

        public bool IsRightDiagonal { get; }

        public string Value { get; set; }

        public bool IsEmpty => Value == "";

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
            Index = index;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Value) ? " " : Value;
        }
    }

    public static class Layout
    {
        public static string[] Layouts => new[] { OneBased, NumPad };
        public const string OneBased = "One-Based";
        public const string NumPad = "Num Pad";
    }
}
