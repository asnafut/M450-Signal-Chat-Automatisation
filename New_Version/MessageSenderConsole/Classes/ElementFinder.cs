using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using IRobot.UIAutomation.Activities.Browser.JScriptExecutor;
using OpenQA.Selenium.Chrome;

namespace MessageSenderConsole
{
    public class ElementFinder
    {
        private readonly ChromeDriver _webDriver;

        public ElementFinder(ChromeDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void WaitForElementToBeVisible(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.ExpectedConditions.ElementIsVisible(by));
        }

        public IWebElement FindElementWithTimeout(By by, int timeoutInSeconds)
        {
            WaitForElementToBeVisible(by, timeoutInSeconds);
            return _webDriver.FindElement(by);
        }
    }
}