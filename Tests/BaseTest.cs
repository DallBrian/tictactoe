using Tests.Helpers;

namespace Tests
{
    public class BaseTest
    {
        public AppWrapper Start()
        {
            var app = new AppWrapper();
            app.App.Run();
            app.WaitFor(a => a.App.IsRunning, "Expected app to be running");
            return app;
        }
    }
}
