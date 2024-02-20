using System.Text;
using NUnit.Framework;
using TicTacToe;

namespace Tests
{
    [TestFixture]
    public class DisplayTests
    {
        [Test]
        public void New_Game_Displays()
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(gameState.Board.ToString());
            expected.AppendLine(display.PlayerTurnMessage);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }

        [Test]
        public void Invalid_Entry_Displays_Message_And_Current_Player_Prompt()
        {
            var gameState = new GameState();
            var display = new DisplayController(gameState);
            var game = new GameController(gameState);

            game.MakeMove("a");

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(gameState.Board.ToString());
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

            game.MakeMove("1");
            game.MakeMove("1");

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(gameState.Board.ToString());
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

            game.MakeMove("1");
            game.MakeMove("4");
            game.MakeMove("2");
            game.MakeMove("5");
            game.MakeMove("3");

            var expected = new StringBuilder();
            expected.AppendLine(display.Title);
            expected.AppendLine(gameState.Board.ToString());
            expected.AppendLine(display.WinnerMessage);
            expected.AppendLine(display.PlayAgainMessage);

            Assert.That(display.GetCurrentDisplayState(), Is.EqualTo(expected.ToString().ReplaceLineEndings()));
        }
    }
}
