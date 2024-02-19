using NUnit.Framework;
using TicTacToe;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BoardDisplaysEmpty()
        {
            var board = new Game().Board;
            Assert.That(board.Values, Is.EqualTo(new[] { "", "", "", "", "", "", "", "", "" }));
        }

        [TestCase(1), TestCase(2), TestCase(3), 
         TestCase(4), TestCase(5), TestCase(6), 
         TestCase(7), TestCase(8), TestCase(9)]
        public void Player1CanInputX(int input)
        {
            var game = new Game();
            game.MakeMove(input);
            var expected = new[] { "", "", "", "", "", "", "", "", "" };
            expected[input - 1] = "X";
            Assert.That(game.Board.Values, Is.EqualTo(expected));
        }

        [Test]
        public void Player2CanInputO()
        {
            var game = new Game();
            game.MakeMove(1);
            game.MakeMove(2);
            Assert.That(game.Board.Values, Is.EqualTo(new[] { "X", "O", "", "", "", "", "", "", "" }));
        }

        [TestCase(0)]
        [TestCase(10)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void CannotInputInvalidMove(int input)
        {
            var game = new Game();
            game.MakeMove(input);
            Assert.That(game.Board.Values, Is.EqualTo(new[] { "", "", "", "", "", "", "", "", "" }));
        }

        [TestCase(new[] { 1, 4, 2, 5, 3 }, 1, TestName = "GameEndsIf(P1Row1Filled)")]
        [TestCase(new[] { 4, 2, 5, 3, 6 }, 1, TestName = "GameEndsIf(P1Row2Filled)")]
        [TestCase(new[] { 7, 4, 8, 5, 9 }, 1, TestName = "GameEndsIf(P1Row3Filled)")]
        [TestCase(new[] { 1, 2, 4, 3, 7 }, 1, TestName = "GameEndsIf(P1Col1Filled)")]
        [TestCase(new[] { 2, 4, 5, 7, 8 }, 1, TestName = "GameEndsIf(P1Col2Filled)")]
        [TestCase(new[] { 3, 4, 6, 5, 9 }, 1, TestName = "GameEndsIf(P1Col3Filled)")]
        [TestCase(new[] { 1, 4, 5, 6, 9 }, 1, TestName = "GameEndsIf(P1DiagonalLeftFilled)")]
        [TestCase(new[] { 3, 4, 5, 6, 7 }, 1, TestName = "GameEndsIf(P1DiagonalRightFilled)")]
        [TestCase(new[] { 7, 1, 4, 2, 5, 3 }, 2, TestName = "GameEndsIf(P2Row1Filled)")]
        [TestCase(new[] { 7, 4, 2, 5, 3, 6 }, 2, TestName = "GameEndsIf(P2Row2Filled)")]
        [TestCase(new[] { 1, 7, 4, 8, 5, 9 }, 2, TestName = "GameEndsIf(P2Row3Filled)")]
        [TestCase(new[] { 5, 1, 2, 4, 3, 7 }, 2, TestName = "GameEndsIf(P2Col1Filled)")]
        [TestCase(new[] { 1, 2, 4, 5, 7, 8 }, 2, TestName = "GameEndsIf(P2Col2Filled)")]
        [TestCase(new[] { 1, 3, 4, 6, 5, 9 }, 2, TestName = "GameEndsIf(P2Col3Filled)")]
        [TestCase(new[] { 2, 1, 4, 5, 6, 9 }, 2, TestName = "GameEndsIf(P2DiagonalLeftFilled)")]
        [TestCase(new[] { 1, 3, 4, 5, 6, 7 }, 2, TestName = "GameEndsIf(P2DiagonalRightFilled)")]
        public void GameEndsIf(int[] moves, int winningPlayer)
        {
            var game = new Game();
            foreach (var move in moves)
            {
                game.MakeMove(move);
            }
            Assert.That(game.IsOver);
            Assert.That(game.Winner, Is.EqualTo(winningPlayer));
        }
    }
}