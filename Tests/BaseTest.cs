using Tests.Helpers;

namespace Tests
{
    public class BaseTest
    {
        public AppWrapper Start()
        {
            var app = new AppWrapper();
            return app.Start();
        }
    }
}
