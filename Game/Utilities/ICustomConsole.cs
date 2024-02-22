namespace TicTacToe.Utilities
{
    public interface ICustomConsole
    {
        public void Initialize();

        public string? ReadKey();

        public void RenderScreen(string display);
    }
}
