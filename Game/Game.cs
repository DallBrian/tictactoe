namespace TicTacToe
{
    public class Game
    {
        public string Title = "\r\n  _____ _       _____           _____         " +
                              "\r\n |_   _(_)__ __|_   _|_ _ __ __|_   _|__  ___ " +
                              "\r\n   | | | / _|___|| |/ _` / _|___|| |/ _ \\/ -_)" +
                              "\r\n   |_| |_\\__|    |_|\\__,_\\__|    |_|\\___/\\___|" +
                              "\r\n                                              " +
                              "\r\n";

        private int _player { get; set; }
        private readonly (int Left, int Top) promptPosition;

        public Game()
        {
            _player = 1;
            IsOver = false;
            Console.WriteLine(Title);
            Board = new(Console.GetCursorPosition());
            Board.Refresh();
            promptPosition = Console.GetCursorPosition();
        }

        public Board Board { get; }

        public bool IsOver { get; set; }

        public int? Winner { get; set; }
        
        private bool lastMessageError = false;
        public void Prompt(string message, bool isError = false)
        {
            Console.SetCursorPosition(promptPosition.Left, promptPosition.Top);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            if (!lastMessageError)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(promptPosition.Left, isError ? promptPosition.Top + 1 : promptPosition.Top);
            Console.WriteLine(message);
            if (lastMessageError)
            {
                var pos = Console.GetCursorPosition();
                Console.SetCursorPosition(pos.Left, pos.Top + 1);
            };
            lastMessageError = isError;
        }

        public void ReadInput()
        {
            bool isValidInput = false;
            do
            {
                Prompt($"It's Player {_player}'s turn");
                var userInput = Console.ReadLine();
                if (userInput == null) continue;
                if (!Board.ValidInput.Contains(userInput))
                {
                    Prompt($"Invalid choice options '{string.Join(',', Board.ValidInput)}'", true);
                    continue;
                }

                if (!MakeMove(int.Parse(userInput)))
                {
                    Prompt("Space is already occupied!", true);
                    continue;
                }

                isValidInput = true;

            } while (!isValidInput);
        }

        public bool MakeMove(int placement)
        {
            var index = placement - 1;
            if (index is < 0 or > 8) return false;
            if (Board.Squares[index].Value is not "") return false;

            Board.Squares[index].Value = _player == 1 ? "X" : "O";
            if (MadeWinningMove(index))
            {
                IsOver = true;
                Winner = _player;
            }

            if (Board.Squares.All(s => s.Value != string.Empty))
            {
                IsOver = true;
            }

            _player = _player == 1 ? 2 : 1;
            Board.Refresh();
            return true;
        }

        private bool MadeWinningMove(int index)
        {
            var playerChar = _player == 1 ? "X" : "O";
            return Board.GetRowContaining(index).All(s => s.Equals(playerChar)) ||
                   Board.GetColContaining(index).All(s => s.Equals(playerChar)) ||
                   (Board.GetLeftDiagonalContaining(index)?.All(s => s.Equals(playerChar)) ?? false) ||
                   (Board.GetRightDiagonalContaining(index)?.All(s => s.Equals(playerChar)) ?? false);
        }
    }
}