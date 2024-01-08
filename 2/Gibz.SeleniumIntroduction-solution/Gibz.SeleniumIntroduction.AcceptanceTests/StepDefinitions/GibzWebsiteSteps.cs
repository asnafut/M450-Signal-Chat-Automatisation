using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Gibz.SeleniumIntroduction.AcceptanceTests.StepDefinitions
{
    [Binding]
    public sealed class GibzWebsiteSteps
    {
        private ChromeDriver webDriver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            webDriver = new ChromeDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            webDriver.Quit();
        }

        [Given(@"I open the GIBZ website")]
        public void GivenIOpenTheGibzWebsite()
        {
            webDriver.Navigate().GoToUrl("https://www.gibz.ch");
        }

        [Given(@"I click on Grundbildung")]
        public void GivenIClickOnGrundbildung()
        {
            webDriver.FindElement(By.Id("grundbildung")).Click();
        }

        [Given(@"I click on Unsere Berufe")]
        [When(@"I click on Unsere Berufe")]
        public void GivenWhenIClickOnUnsereBerufe()
        {
            webDriver.FindElement(By.XPath("//li[@id='portaltab-grundbildung']/ul/li[2]/a")).Click();
        }

        [When(@"I click on Informatiker/in EFZ")]
        public void WhenIClickOnInformatikerInEfz()
        {
            webDriver.FindElement(By.XPath("//a[@title='Informatiker/in EFZ']")).Click();
        }

        [Then(@"the first Berufsverantwortlicher is Peter Gisler")]
        public void ThenTheFirstBerufsverantwortlicherIsPeterGisler()
        {
            var firstContactText = webDriver.FindElement(By.XPath("//a[@id='kontakt']//following-sibling::div[1]/p[1]")).Text;
            var normalizedFirstContactText = NormalizeText(firstContactText);
            normalizedFirstContactText.Should().Contain("Peter Gisler");
        }

        [Then(@"the second Berufsverantwortlicher is Tobias Schmid")]
        public void ThenTheSecondBerufsverantwortlicherIsTobiasSchmid()
        {
            var secondContactText = webDriver.FindElement(By.XPath("//a[@id='kontakt']//following-sibling::div[1]/p[2]")).Text;
            var normalizedSecondContactText = NormalizeText(secondContactText);
            normalizedSecondContactText.Should().Contain("Tobias Schmid");
        }

        [Then(@"(.*) Berufe starting with (.*) should be displayed")]
        public void ThenBerufeStartingWithShouldBeDisplayed(int expectedNumberOfBerufe, string startingKey)
        {
            var countLinks = webDriver.FindElements(By.XPath($"//div[@class = 'table-wrapper']//a[starts-with(text(), '{startingKey}')]")).Count;
            var countSpans = webDriver.FindElements(By.XPath($"//div[@class = 'table-wrapper']//span[starts-with(text(), '{startingKey}')]")).Count;
            var count = countLinks + countSpans;
            count.Should().Be(expectedNumberOfBerufe);
        }

        private string NormalizeText(string input)
        {
            return input.Replace("­", "");
        }
    }
}
