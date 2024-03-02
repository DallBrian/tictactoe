using TicTacToe;

namespace Tests.Helpers
{
    public class AppWrapper
    {
        private readonly MockConsole Console;
        private readonly App App;

        public AppWrapper()
        {
            Console = new();
            App = new(Console);
        }

        public string? Screen => Console.Screen;
        public string[]? ScreenLines => Screen?.Split("\r\n");
        public string? Title => GetScreenLines(..6);
        public string? Board => GetScreenLines(6..23);
        public string Message1 => GetScreenLines(24..25) ?? string.Empty;
        public string Message2 => GetScreenLines(25..26) ?? string.Empty;
        public string Message3 => GetScreenLines(26..27) ?? string.Empty;
        public bool IsRunning => App.IsRunning;
        public string[] BoardValues => App.GameState.Board.Values;

        private string? GetScreenLines(Range range)
        {
            return ScreenLines is null || ScreenLines.Length < range.End.Value ? null : string.Join("\r\n", ScreenLines[range]);
        }

        public AppWrapper Start()
        {
            App.Run();
            App.WaitFor(a => a.IsRunning, "Expected app to be running");
            Console.WaitFor(c => c.IsAwaitingInput, "Timed out waiting for the app to be waiting for input");
            return this;
        }

        public void PlayerInput(string input, bool waitForNextInput = true)
        {
            Console.Input = input;
            Console.WaitFor(c => c.Input is null, "Timed out waiting for input to be read.");
            if (waitForNextInput)
                Console.WaitFor(c => c.IsAwaitingInput, "Timed out waiting for the app to be waiting for input");
        }

        public void NewGame()
        {
            PlayerInput("1");
        }

        private bool IsAIEnabled => App.GameState.NextPlayer.IsAI;

        public void ToggleAI()
        {
            var isAIEnabled = IsAIEnabled;
            PlayerInput("2");
            this.WaitFor(a => a.IsAIEnabled != isAIEnabled, "Expected AI to be toggled");
        }

        public void Resume()
        {
            PlayerInput("3");
        }
    }
}