namespace TicTacToe
{
    public class GameState
    {
        public Player CurrentPlayer { get; set; } = new() { Id = 1, Name = "1", Mark = "X" };

        public Player NextPlayer { get; set; } = new() { Id = 2, Name = "2", Mark = "O" };

        public Errors Errors { get; } = new();

        public bool IsOver { get; set; } = false;

        public Player? Winner { get; set; } = null;

        public BoardState Board { get; set; } = new();
    }

    public class Errors
    {
        public bool InvalidPlacement { get; set; }

        public bool PlacementAlreadyOccupied { get; set; }

        public void Clear()
        {
            foreach (var propertyInfo in GetType().GetProperties())
                propertyInfo.SetValue(this, false);
        }
    }
}