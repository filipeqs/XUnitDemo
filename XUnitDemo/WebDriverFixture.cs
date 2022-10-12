using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace XUnitDemo
{
    public class WebDriverFixture : IDisposable
    {
        public ChromeDriver ChromeDriver { get; private set; }

        public WebDriverFixture()
        {
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver = new ChromeDriver();
        }

        public void Dispose()
        {
            ChromeDriver.Quit();
            ChromeDriver.Dispose();
        }
    }
}
