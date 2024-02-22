using System.Text;
using NUnit.Framework;
using TicTacToe.Controllers;
using TicTacToe.Models;

namespace Tests.Tests
{
    public class DisplayTests : BaseTest
    {
        [Test]
        public void New_Game_Displays()
        {
            var app = Start();
            app.NewGame();

            var display = app.App.Display;
            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(display.Board);
            expected.AppendLine(display.PlayerTurnMessage);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }

        [Test]
        public void Invalid_Entry_Displays_Message_And_Current_Player_Prompt()
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);
            var game = new GameController(gameState);

            game.NewGame();
            game.MakeMove("a");

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(display.Board);
            expected.AppendLine(display.InvalidPlacementMessage);
            expected.AppendLine(display.PlayerTurnMessage);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }

        [Test]
        public void Placing_In_Occupied_Displays_Message_And_Current_Player_Prompt()
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);
            var game = new GameController(gameState);

            game.NewGame();
            game.MakeMove("1");
            game.MakeMove("1");

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(display.Board);
            expected.AppendLine(display.PlacementOccupiedMessage);
            expected.AppendLine(display.PlayerTurnMessage);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }

        [Test]
        public void Player_Making_Winning_Move_Displays_Winner()
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);
            var game = new GameController(gameState);

            game.NewGame();
            game.MakeMove("1");
            game.MakeMove("4");
            game.MakeMove("2");
            game.MakeMove("5");
            game.MakeMove("3");

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(display.Board);
            expected.AppendLine(display.WinnerMessage);
            expected.AppendLine(display.NewGameOption);
            expected.AppendLine(display.EnableAIOption);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }

        [Test]
        public void On_Menu_Screen_When_Game_Not_Active()
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);

            Assert.That(gameState.IsActiveGame, Is.False);

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(display.Board);
            expected.AppendLine(display.NewGameOption);
            expected.AppendLine(display.ToggleAIOption);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }
    }
}
