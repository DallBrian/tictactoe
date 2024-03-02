using TicTacToe.Utilities;

namespace Tests.Helpers
{
    public class MockConsole : ICustomConsole
    {
        public string? Screen { get; set; }

        public string? Input { get; set; }
        public bool IsAwaitingInput { get; internal set; }

        public void Initialize()
        {
            //ignore
        }

        public string? ReadKey()
        {
            IsAwaitingInput = true;
            while (Input is null) Thread.Sleep(10);
            var key = Input;
            IsAwaitingInput = false;
            Input = null;
            return key;
        }

        public void RenderScreen(string display)
        {
            Screen = display;
        }
    }
}
