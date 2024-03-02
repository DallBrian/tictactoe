using NUnit.Framework;
using Tests.Helpers;
using TicTacToe.Utilities;

namespace Tests.Tests
{
    [Category("UI Regression")]
    public class ApplicationTests : BaseTest
    {
        private const string Title = "  _____ _       _____           _____         \r\n" +
                                     " |_   _(_)__ __|_   _|_ _ __ __|_   _|__  ___ \r\n" +
                                     "   | | | / _|___|| |/ _` / _|___|| |/ _ \\/ -_)\r\n" +
                                     "   |_| |_\\__|    |_|\\__,_\\__|    |_|\\___/\\___|\r\n" +
                                     "                                              \r\n";
        private const string EmptyBoard = "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                 1|       2|       3\r\n" +
                                          "          --------------------------\r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                 4|       5|       6\r\n" +
                                          "          --------------------------\r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                  |        |        \r\n" +
                                          "                 7|       8|       9";
        private const string NewGameLine = "                  1 New Game                  ";
        private const string EnableAILine = "                 2 Enable AI                  ";
        private const string DisableAILine = "                 2 Disable AI                 ";
        private const string Player1Turn = "             It's Player 1's turn             ";
        private const string Player2Turn = "             It's Player 2's turn             ";
        private const string ResumeLine = "                   3 Resume                   ";

        [Test]
        public void On_Start_Application_Displays_Title_Board_Menu()
        {
            var app = Start();
            Assert.That(app.Title, Is.EqualTo(Title));
            Assert.That(app.Board, Is.EqualTo(EmptyBoard));
            Assert.That(app.Message1, Is.EqualTo(NewGameLine));
            Assert.That(app.Message2, Is.EqualTo(EnableAILine));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Application_Can_Be_Stopped_By_User_Input_On_Main_Menu()
        {
            var app = Start();
            Assert.That(app.IsRunning, Is.True);
            app.PlayerInput(Keys.Escape, false);
            app.WaitFor(a => !a.IsRunning, "Expected app to stop running");
            Assert.That(app.IsRunning, Is.False);
        }

        [Test]
        public void User_Can_Start_New_Game()
        {
            var app = Start();
            app.NewGame();
            Assert.That(app.Title, Is.EqualTo(Title));
            Assert.That(app.Board, Is.EqualTo(EmptyBoard));
            Assert.That(app.Message1, Is.EqualTo(Player1Turn));
            Assert.That(app.Message2, Is.EqualTo(string.Empty));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }

        [Test]
        public void User_Can_Enable_And_Disable_AI_After_Start()
        {
            var app = Start();
            Assert.That(app.Message2, Is.EqualTo(EnableAILine));
            app.ToggleAI();
            Assert.That(app.Message2, Is.EqualTo(DisableAILine));
            app.ToggleAI();
            Assert.That(app.Message2, Is.EqualTo(EnableAILine));
        }

        [Test]
        public void User_Can_Pause_Game_And_Change_Menu_Options()
        {
            var app = Start();
            app.NewGame();
            app.PlayerInput(Keys.Escape);
            Assert.That(app.Message1, Is.EqualTo(NewGameLine));
            Assert.That(app.Message2, Is.EqualTo(EnableAILine));
            Assert.That(app.Message3, Is.EqualTo(ResumeLine));
            app.ToggleAI();
            Assert.That(app.Message1, Is.EqualTo(NewGameLine));
            Assert.That(app.Message2, Is.EqualTo(DisableAILine));
            Assert.That(app.Message3, Is.EqualTo(ResumeLine));
            app.Resume();
            Assert.That(app.Message1, Is.EqualTo(Player1Turn));
            Assert.That(app.Message2, Is.EqualTo(string.Empty));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }

        [TestCase("1"), TestCase("2"), TestCase("3"),
         TestCase("4"), TestCase("5"), TestCase("6"),
         TestCase("7"), TestCase("8"), TestCase("9")]
        public void Player1_Can_Input_X_And_Pass_Turn(string input)
        {
            var app = Start();
            app.NewGame();
            app.PlayerInput(input);
            var expected = new[] { "", "", "", "", "", "", "", "", "" };
            expected[int.Parse(input) - 1] = "X";
            Assert.That(app.BoardValues, Is.EqualTo(expected));
            Assert.That(app.Message1, Is.EqualTo(Player2Turn));
            Assert.That(app.Message2, Is.EqualTo(string.Empty));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Player2_Can_Input_O_And_Pass_Turn()
        {
            var app = Start();
            app.NewGame();
            app.PlayerInput("1");
            app.PlayerInput("2");

            Assert.That(app.BoardValues, Is.EqualTo(new[] { "X", "O", "", "", "", "", "", "", "" }));
            Assert.That(app.Message1, Is.EqualTo(Player1Turn));
            Assert.That(app.Message2, Is.EqualTo(string.Empty));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }

        [TestCase("0")]
        [TestCase("10")]
        [TestCase("asdf")]
        [TestCase("!@#$%^&*()_+=-")]
        [TestCase("01")]
        public void Invalid_Entry_Displays_Message_And_Current_Player_Prompt(string input)
        {
            var app = Start();
            app.NewGame();
            app.PlayerInput(input);
            Assert.That(app.BoardValues, Is.EqualTo(new[] { "", "", "", "", "", "", "", "", "" }));
            Assert.That(app.Message1, Is.EqualTo("  Invalid choice options '1,2,3,4,5,6,7,8,9'  "));
            Assert.That(app.Message2, Is.EqualTo(Player1Turn));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Placing_In_Occupied_Displays_Message_And_Current_Player_Prompt()
        {
            var app = Start();
            app.NewGame();
            app.PlayerInput("1");
            app.PlayerInput("1");
            Assert.That(app.Message1, Is.EqualTo("          Space is already occupied!          "));
            Assert.That(app.Message2, Is.EqualTo(Player2Turn));
            Assert.That(app.Message3, Is.EqualTo(string.Empty));
        }
        
        [TestCase(new[] { 1, 4, 2, 5, 3 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1Row1Filled)")]
        [TestCase(new[] { 4, 2, 5, 3, 6 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1Row2Filled)")]
        [TestCase(new[] { 7, 4, 8, 5, 9 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1Row3Filled)")]
        [TestCase(new[] { 1, 2, 4, 3, 7 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1Col1Filled)")]
        [TestCase(new[] { 2, 4, 5, 7, 8 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1Col2Filled)")]
        [TestCase(new[] { 3, 4, 6, 5, 9 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1Col3Filled)")]
        [TestCase(new[] { 1, 4, 5, 6, 9 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1DiagonalLeftFilled)")]
        [TestCase(new[] { 3, 4, 5, 6, 7 }, "1", TestName = "Game_Ends_And_Displays_Winner_If(P1DiagonalRightFilled)")]
        [TestCase(new[] { 7, 1, 4, 2, 5, 3 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2Row1Filled)")]
        [TestCase(new[] { 7, 4, 2, 5, 3, 6 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2Row2Filled)")]
        [TestCase(new[] { 1, 7, 4, 8, 5, 9 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2Row3Filled)")]
        [TestCase(new[] { 5, 1, 2, 4, 3, 7 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2Col1Filled)")]
        [TestCase(new[] { 3, 2, 4, 5, 7, 8 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2Col2Filled)")]
        [TestCase(new[] { 1, 3, 4, 6, 5, 9 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2Col3Filled)")]
        [TestCase(new[] { 2, 1, 4, 5, 6, 9 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2DiagonalLeftFilled)")]
        [TestCase(new[] { 1, 3, 4, 5, 6, 7 }, "2", TestName = "Game_Ends_And_Displays_Winner_If(P2DiagonalRightFilled)")]
        public void Game_Ends_And_Displays_Winner_If(int[] moves, string winningPlayer)
        {
            var app = Start();
            app.NewGame();
            foreach (var move in moves) app.PlayerInput(move.ToString());
            Assert.That(app.Message1, Is.EqualTo($"          Congrats Player {winningPlayer} you won!          "));
            Assert.That(app.Message2, Is.EqualTo(NewGameLine));
            Assert.That(app.Message3, Is.EqualTo(EnableAILine));
        }

        [Test]
        public void Game_Ends_And_Displays_Winner_If_No_Valid_Moves_Left()
        {
            var app = Start();
            app.NewGame();
            var moves = new[] { 1, 2, 4, 7, 5, 6, 3, 9, 8 };
            foreach (var move in moves) app.PlayerInput(move.ToString());
            Assert.That(app.Message1, Is.EqualTo("          Draw! No winner this time.          "));
            Assert.That(app.Message2, Is.EqualTo(NewGameLine));
            Assert.That(app.Message3, Is.EqualTo(EnableAILine));
        }
    }
}
