using FluentAssertions;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace XUnitDemo
{
    [Collection("Sequence")]
    public class SeleniumWIthContext : IClassFixture<WebDriverFixture>
    {
        private readonly WebDriverFixture _webDriverFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public SeleniumWIthContext(WebDriverFixture webDriverFixture, ITestOutputHelper testOutputHelper)
        {
            _webDriverFixture = webDriverFixture;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("admin", "password")]
        [InlineData("admin", "password2")]
        [InlineData("admin", "password3")]
        public void TestLoginWithFillData(string username, string password)
        {
            _testOutputHelper.WriteLine("First test");
            var driver = _webDriverFixture.ChromeDriver;
            driver.Navigate().GoToUrl("http://eaapp.somee.com");

            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.CssSelector(".btn-default")).Click();

            var exception = Assert.Throws<NoSuchElementException>(() =>
                driver.FindElement(By.CssSelector(".btn-defaults")).Click());

            Assert.Contains("no such element: Unable", exception.Message);

            exception.Message.Should().Contain("no such element: Unable");

            _testOutputHelper.WriteLine("Test completed");
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void TestRegisterUser(string username, string password, string confirmPassword, string email)
        {
            _testOutputHelper.WriteLine("First test");

            var driver = _webDriverFixture.ChromeDriver;
            driver.Navigate().GoToUrl("http://eaapp.somee.com");

            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(confirmPassword);
            driver.FindElement(By.Id("Email")).SendKeys(email);

            _testOutputHelper.WriteLine("Test completed");
        }

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[]
            {
                "Filipe",
                "FilipePassword",
                "FilipePassword",
                "filipe@outlook.com"
            },
            new object[]
            {
                "Silva",
                "SilvaPassword",
                "SilvaPassword",
                "silva@outlook.com"
            },
            new object[]
            {
                "James",
                "JamesPassword",
                "JamesPassword",
                "james@outlook.com"
            },
        };
    }
}
