using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;

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

            // Provide a unique user-data-dir path for Chrome
            options.AddArgument($"--user-data-dir={Path.GetTempPath()}chrome_user_data_dir_{Guid.NewGuid()}");

            // Optional: Run without GUI (headless)
            //options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void JenkinsTest1()
        {
            driver.Navigate().GoToUrl("https://www.epam.com/");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            var dynamicElement = wait.Until(driver =>
            {
                return driver.FindElement(By.XPath("//a[contains(@class, 'top-navigation__item-link') and contains(., 'Careers')]")).Displayed;
            });

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
