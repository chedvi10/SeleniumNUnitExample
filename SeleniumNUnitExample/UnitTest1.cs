using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumNUnitExample.PagesObjects;

namespace SeleniumNUnitExample
{

    [TestFixture]
    public class GoogleSearchTest
    {
        private IWebDriver driver;
        private GoogleHomePage googleHomePage;
        private GoogleResultsPage googleResultPage;

        public GoogleSearchTest()
        {

        }
        [SetUp]
        public void SetUp()
        {
            string path = "C:\\Users\\1\\Desktop\\אוטומציה\\SeleniumNUnitExample\\SeleniumNUnitExample";
            driver = new ChromeDriver(path);
            driver.Manage().Window.Maximize();
            googleHomePage = new GoogleHomePage(driver);
            googleResultPage = new GoogleResultsPage(driver);
        }
        [Test]
        public void TestGoogleSearch()
        {
            //1
            // Navigate to Google

            driver.Navigate().GoToUrl("https://www.google.com");

            //2
            // Verify the title of the page

            Assert.AreEqual("Google", driver.Title);

            //3
            // Search for a term

            // Verify that results are displayed
            Assert.IsTrue(googleResultPage.ResultsDisplayed());

            // Get the title of the first result and click it
            string firstResultTitle = googleResultPage.GetFirstResultTitle();
            googleResultPage.ClickFirstResult();

            // Verify the title of the new page
            Assert.IsTrue(driver.Title.Contains(firstResultTitle));

            // Navigate back to the Google search results page
            driver.Navigate().Back();

            // Verify the search box still contains the search term
            Assert.AreEqual("Selenium WebDriver", driver.FindElement(By.Name("q")).GetAttribute("value"));
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }

}

