using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using OpenQA.Selenium;
using System.Net.Mail;
using Xunit.Abstractions;
using XUnitDemo.XUnitExtension;

namespace XUnitDemo
{
    public class SeleniumWithAutofixture : IClassFixture<WebDriverFixture>
    {
        private readonly WebDriverFixture _webDriverFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public SeleniumWithAutofixture(WebDriverFixture webDriverFixture, ITestOutputHelper testOutputHelper)
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


        public void ClassFixtureTestFillData()
        {
            var driver = _webDriverFixture.ChromeDriver;
            _testOutputHelper.WriteLine("First test");
            driver
                .Navigate()
                .GoToUrl("https://www.youtube.com/");

            var search = new Fixture().Create<MailAddress>();

            driver.FindElement(By.Name("search_query")).SendKeys(search.Address);
            driver.FindElement(By.Id("search-icon-legacy")).Click();

            _testOutputHelper.WriteLine("Test completed");
        }

        [Fact]
        public void TestRegisterUserWithAutoFixture()
        {
            _testOutputHelper.WriteLine("First test");

            var driver = _webDriverFixture.ChromeDriver;
            driver.Navigate().GoToUrl("http://eaapp.somee.com");

            var fixture = new Fixture();

            var user = fixture.Build<RegisterUserModel>()
                            .With(q => q.Email, "test@email.com")
                            .Create();

            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys(user.Name);
            driver.FindElement(By.Id("Password")).SendKeys(user.Password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(user.ConfirmPassword);
            driver.FindElement(By.Id("Email")).SendKeys(user.Email);

            _testOutputHelper.WriteLine("Test completed");
        }

        [Theory, AutoData]
        public void TestRegisterUserWithAutoData(RegisterUserModel user)
        {
            _testOutputHelper.WriteLine("First test");

            var driver = _webDriverFixture.ChromeDriver;
            driver.Navigate().GoToUrl("http://eaapp.somee.com");

            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys(user.Name);
            driver.FindElement(By.Id("Password")).SendKeys(user.Password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(user.ConfirmPassword);
            driver.FindElement(By.Id("Email")).SendKeys(user.Email);

            _testOutputHelper.WriteLine("Test completed");
        }

        [Theory, RegisterUserAttribute]
        public void TestRegisterUserWithRegisterUserAttribute(RegisterUserModel user)
        {
            _testOutputHelper.WriteLine("First test");

            var driver = _webDriverFixture.ChromeDriver;
            driver.Navigate().GoToUrl("http://eaapp.somee.com");

            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys(user.Name);
            driver.FindElement(By.Id("Password")).SendKeys(user.Password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(user.ConfirmPassword);
            driver.FindElement(By.Id("Email")).SendKeys(user.Email);

            _testOutputHelper.WriteLine("Test completed");
        }
    }
}
