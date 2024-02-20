using NUnit.Framework;
using TicTacToe;

namespace Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void NewGameBoardDisplaysEmpty()
        {
            var board = new GameController(new()).Board;
            Assert.That(board.Values, Is.EqualTo(new[] { "", "", "", "", "", "", "", "", "" }));
        }

        [TestCase("1"), TestCase("2"), TestCase("3"), 
         TestCase("4"), TestCase("5"), TestCase("6"), 
         TestCase("7"), TestCase("8"), TestCase("9")]
        public void Player1CanInputXAndPassTurn(string input)
        {
            var state = new GameState();
            var game = new GameController(state);
            var nextPlayerName = state.NextPlayer.Name;
            game.MakeMove(input);
            var expected = new[] { "", "", "", "", "", "", "", "", "" };
            expected[int.Parse(input) - 1] = "X";
            Assert.That(game.Board.Values, Is.EqualTo(expected));
            Assert.That(state.CurrentPlayer.Name, Is.EqualTo(nextPlayerName));
        }

        [Test]
        public void Player2CanInputOAndPassTurn()
        {
            var state = new GameState();
            var game = new GameController(state);
            var secondPlayerName = state.NextPlayer.Name;
            game.MakeMove("1");
            Assert.That(state.CurrentPlayer.Name, Is.EqualTo(secondPlayerName));
            var firstPlayerName = state.NextPlayer.Name;
            game.MakeMove("2");
            Assert.That(game.Board.Values, Is.EqualTo(new[] { "X", "O", "", "", "", "", "", "", "" }));
            Assert.That(state.CurrentPlayer.Name, Is.EqualTo(firstPlayerName));
        }

        [TestCase("0")]
        [TestCase("10")]
        [TestCase("asdf")]
        [TestCase("!@#$%^&*()_+=-")]
        [TestCase("01")]
        public void CannotInputInvalidMove(string input)
        {
            var state = new GameState();
            var game = new GameController(state);
            var currentPlayerName = state.CurrentPlayer.Name;
            game.MakeMove(input);
            Assert.That(game.Board.Values, Is.EqualTo(new[] { "", "", "", "", "", "", "", "", "" }));
            Assert.That(state.CurrentPlayer.Name, Is.EqualTo(currentPlayerName));
        }

        [TestCase(new[] { 1, 4, 2, 5, 3 }, "1", TestName = "GameEndsIf(P1Row1Filled)")]
        [TestCase(new[] { 4, 2, 5, 3, 6 }, "1", TestName = "GameEndsIf(P1Row2Filled)")]
        [TestCase(new[] { 7, 4, 8, 5, 9 }, "1", TestName = "GameEndsIf(P1Row3Filled)")]
        [TestCase(new[] { 1, 2, 4, 3, 7 }, "1", TestName = "GameEndsIf(P1Col1Filled)")]
        [TestCase(new[] { 2, 4, 5, 7, 8 }, "1", TestName = "GameEndsIf(P1Col2Filled)")]
        [TestCase(new[] { 3, 4, 6, 5, 9 }, "1", TestName = "GameEndsIf(P1Col3Filled)")]
        [TestCase(new[] { 1, 4, 5, 6, 9 }, "1", TestName = "GameEndsIf(P1DiagonalLeftFilled)")]
        [TestCase(new[] { 3, 4, 5, 6, 7 }, "1", TestName = "GameEndsIf(P1DiagonalRightFilled)")]
        [TestCase(new[] { 7, 1, 4, 2, 5, 3 }, "2", TestName = "GameEndsIf(P2Row1Filled)")]
        [TestCase(new[] { 7, 4, 2, 5, 3, 6 }, "2", TestName = "GameEndsIf(P2Row2Filled)")]
        [TestCase(new[] { 1, 7, 4, 8, 5, 9 }, "2", TestName = "GameEndsIf(P2Row3Filled)")]
        [TestCase(new[] { 5, 1, 2, 4, 3, 7 }, "2", TestName = "GameEndsIf(P2Col1Filled)")]
        [TestCase(new[] { 3, 2, 4, 5, 7, 8 }, "2", TestName = "GameEndsIf(P2Col2Filled)")]
        [TestCase(new[] { 1, 3, 4, 6, 5, 9 }, "2", TestName = "GameEndsIf(P2Col3Filled)")]
        [TestCase(new[] { 2, 1, 4, 5, 6, 9 }, "2", TestName = "GameEndsIf(P2DiagonalLeftFilled)")]
        [TestCase(new[] { 1, 3, 4, 5, 6, 7 }, "2", TestName = "GameEndsIf(P2DiagonalRightFilled)")]
        public void GameEndsIf(int[] moves, string winningPlayer)
        {
            var game = new GameController(new());
            foreach (var move in moves)
            {
                Assert.That(game.Winner, Is.Null);
                Assert.That(game.IsOver, Is.False);
                game.MakeMove(move.ToString());
            }
            Assert.That(game.IsOver);
            Assert.That(game.Winner?.Name, Is.EqualTo(winningPlayer));
        }
    }
}