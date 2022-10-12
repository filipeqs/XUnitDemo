using WebDriverManager;
using Xunit.Abstractions;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace XUnitDemo
{
    public class SeleniumWithoutContext
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ChromeDriver _chromeDriver;

        public SeleniumWithoutContext(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            // WebDriverManager
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            _chromeDriver = new ChromeDriver();
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine("First test");
            _testOutputHelper.WriteLine("First test");
            _chromeDriver.Navigate().GoToUrl("https://www.youtube.com/");
        }
    }
}