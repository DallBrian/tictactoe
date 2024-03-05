namespace TicTacToe.Models
{
    [Serializable]
    public class GameState
    {
        public Player CurrentPlayer { get; set; } = new() { Id = 1, Name = "Player 1", Mark = "X" };

        public Player NextPlayer { get; set; } = new() { Id = 2, Name = "Player 2", Mark = "O" };

        public Errors Errors { get; } = new();

        public bool IsOver { get; set; } = false;

        public Player? Winner { get; set; } = null;

        public BoardState Board { get; set; } = new();

        public bool IsActiveGame { get; set; } = false;

        public bool IsPaused { get; set; } = false;

        public string CurrentLayout { get; set; } = Layout.Layouts[0];
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