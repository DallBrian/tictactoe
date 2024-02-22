using NUnit.Framework;
using Tests.Helpers;
using TicTacToe.Utilities;

namespace Tests.Tests
{
    public class ApplicationTests : BaseTest
    {
        [Test]
        public void Application_Can_Be_Stopped_By_User_Input()
        {
            //Start the app and wait for the menu to display
            var app = Start();
            Assert.That(app.App.IsRunning);
            app.Console.Input = Keys.Escape;
            app.WaitFor(a => a.App.IsRunning == false, "Expected app to no longer be running");
            Assert.That(app.App.IsRunning, Is.False);
        }

        [Test]
        public void User_Can_Select_New_Game()
        {
            var app = Start();
            Assert.That(app.App.GameState.IsActiveGame, Is.False);
            app.Console.Input = "1";
            app.WaitFor(a => a.App.GameState.IsActiveGame, "Expected game to be active");
            Assert.That(app.App.GameState.IsActiveGame);
        }

        [Test]
        public void User_Can_Enable_And_Disable_AI()
        {
            var app = Start();
            Assert.That(app.App.GameState.NextPlayer.IsAI, Is.False);
            Assert.That(app.App.GameState.CurrentPlayer.IsAI, Is.False);
            app.Console.Input = "2";
            app.WaitFor(a => a.App.GameState.NextPlayer.IsAI, "Expected opponent to be AI");
            Assert.That(app.App.GameState.NextPlayer.IsAI, Is.True);
            Assert.That(app.App.GameState.CurrentPlayer.IsAI, Is.False);
            app.Console.Input = "2";
            app.WaitFor(a => a.App.GameState.NextPlayer.IsAI == false, "Expected opponent to no longer be AI");
            Assert.That(app.App.GameState.NextPlayer.IsAI, Is.False);
            Assert.That(app.App.GameState.CurrentPlayer.IsAI, Is.False);
        }

        [Test]
        public void User_Can_Pause_Game_And_Change_Menu_Options()
        {
            var app = Start();
            app.NewGame();
            Assert.That(app.App.GameState.IsPaused, Is.False);
            app.PlayerInput(Keys.Escape);
            Assert.That(app.App.GameState.IsPaused);
            app.PlayerInput("2");
            Assert.That(app.App.GameState.NextPlayer.IsAI, Is.True);
            app.PlayerInput("3");
            Assert.That(app.App.GameState.IsPaused, Is.False);
        }
    }
}
