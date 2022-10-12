using FluentAssertions;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace XUnitDemo
{
    [Collection("Sequence")]
    public class SecondSeleniumTest : IClassFixture<WebDriverFixture>
    {
        private readonly WebDriverFixture _webDriverFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public SecondSeleniumTest(WebDriverFixture webDriverFixture, ITestOutputHelper testOutputHelper)
        {
            _webDriverFixture = webDriverFixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ClassFixtureTestNavigate()
        {
            _testOutputHelper.WriteLine("First test");
            _webDriverFixture.ChromeDriver
                .Navigate()
                .GoToUrl("https://www.youtube.com/");

            var anchors = _webDriverFixture.ChromeDriver
                            .FindElements(By.TagName("a"));

            anchors.Should().HaveCountGreaterThan(1);
        }

        [Theory]
        [InlineData("XUnit")]
        [InlineData("Selenium")]
        public void ClassFixtureTestFillData(string search)
        {
            var driver = _webDriverFixture.ChromeDriver;
            _testOutputHelper.WriteLine("First test");
            driver
                .Navigate()
                .GoToUrl("https://www.youtube.com/");

            driver.FindElement(By.Name("search_query")).SendKeys(search);
            driver.FindElement(By.Id("search-icon-legacy")).Click();

            _testOutputHelper.WriteLine("Test completed");
        }
    }
}
