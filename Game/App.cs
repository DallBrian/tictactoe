using System.ComponentModel;
using TicTacToe.Controllers;
using TicTacToe.Models;
using TicTacToe.Utilities;

namespace TicTacToe
{
    public class App
    {
        private readonly ICustomConsole Console;
        private BackgroundWorker worker;

        public App(ICustomConsole userInput)
        {
            Console = userInput;
        }

        public bool IsRunning => worker.IsBusy;

        public GameState GameState { get; private set; }
        public DisplayController Display { get; private set; }
        public GameController Game { get; private set; }
        public AIController AI { get; private set; }

        public void Run()
        {
            GameState = new();
            Display = new(GameState);
            Game = new(GameState);
            AI = new(GameState);

            worker = new();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += GameLoop;
            worker.RunWorkerAsync();
        }

        private void GameLoop(object sender, DoWorkEventArgs e)
        {
            Console.Initialize();

            do
            {
                Console.RenderScreen(Display.GetCurrentDisplayState());
                var playerInput = Console.ReadKey();
                switch (playerInput)
                {
                    case Keys.Escape when GameState is { IsActiveGame: true, IsPaused: false }:
                        Game.PauseGame();
                        break;
                    case Keys.Escape when GameState is { IsPaused: true }: return;
                    case Keys.Escape when GameState is { IsActiveGame: false }: return;
                    case "1" when GameState is not { IsActiveGame: true, IsPaused: false }:
                        Game.NewGame();
                        break;
                    case "2" when !GameState.IsActiveGame || GameState.IsPaused:
                        Game.ToggleAI();
                        break;
                    case "3" when GameState.IsPaused:
                        Game.ResumeGame();
                        break;
                    default: Game.MakeMove(playerInput);
                        break;
                }

                if (GameState.CurrentPlayer.IsAI && GameState is { IsActiveGame: true, IsPaused: false })
                    Game.MakeMove(AI.DetermineMove().ToString());

            } while (true);
        }
    }
}
