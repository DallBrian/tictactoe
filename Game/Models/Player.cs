namespace TicTacToe.Models
{
    public class Player
    {
        public int Id { get; set; }

        private string _name { get; set; }

        public string Name
        {
            get => IsAI ? "AI" : _name;
            set => _name = value;
        }

        public string Mark { get; set; }

        public bool IsAI { get; set; }
    }
}
