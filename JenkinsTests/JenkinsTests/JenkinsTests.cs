using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FluentAssertions;

namespace JenkinsTests
{
    [TestFixture]
    public class JenkinsTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument($"--user-data-dir={Path.GetTempPath()}chrome_user_data_dir_{Guid.NewGuid()}");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void JenkinsTest1()
        {
            driver.Navigate().GoToUrl("https://www.epam.com/");
            driver.FindElement(By.XPath("//a[contains(@class, 'top-navigation__item-link') and contains(., 'Careers')]")).Click();
            driver.FindElement(By.Id("new_form_job_search-keyword")).Displayed.Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
