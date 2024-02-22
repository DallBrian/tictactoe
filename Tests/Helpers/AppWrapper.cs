using TicTacToe;

namespace Tests.Helpers
{
    public class AppWrapper
    {
        public MockConsole Console;
        public App App;

        public AppWrapper()
        {
            Console = new();
            App = new(Console);
        }

        public void NewGame()
        {
            PlayerInput("1");
        }

        public void PlayerInput(string input)
        {
            Console.Input = input;
            Console.WaitFor(c => c.Input is null, "Timed out waiting for input to be read.");
            Console.WaitFor(c => c.IsAwaitingInput, "Timed out waiting for the app to be waiting for input");
        }
    }
}
