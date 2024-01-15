using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MessageSenderConsole
{
    public class WhatsappActions
    {
        private readonly ChromeDriver _webDriver;
        private readonly ElementFinder _elementFinder;
        private readonly MessageSender _messageSender;

        public WhatsappActions(ChromeDriver webDriver, ElementFinder elementFinder)
        {
            _webDriver = webDriver;
            _elementFinder = elementFinder;
        }

        public void Login(string number)
        {
            _webDriver.Navigate().GoToUrl("https://web.whatsapp.com");

            _elementFinder.WaitForElementToBeVisible(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span"), 10);

            _webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span")).Click();

            //warten bis input für tel Nummer sichtbar ist
            var input = _elementFinder.FindElementWithTimeout(
                By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[1]/div[2]/div/div/div/form/input"), 20);
            //input für tel Nummer
            input.Click();
            input.SendKeys(number);
            //Weiter Button
            var weiterButton =
                _elementFinder.FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[2]/button"), 20);
            weiterButton.Click();

            Console.WriteLine(
                "Wait for your Phone to send you a message from whatsapp and enter the Code you see on the Display");
            Console.WriteLine($"the code is {_messageSender.GetCode(_webDriver)}");

            _elementFinder.WaitForElementToBeVisible(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[1]/div/span"),
                100);
            Console.WriteLine("login finished");
        }

        public void Logout()
        {
            var menu = _elementFinder.FindElementWithTimeout(
                By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[5]/div/span"), 3);
            menu.Click();
            var logoutButton =
                _elementFinder.FindElementWithTimeout(
                    By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[5]/span/div/ul/li[7]/div"), 3);
            logoutButton.Click();
            //*[@id="pane-side"]/div[1]/div/div/div[4]/div/div/div/div[1]/div/div/div/img
            var logoutButtonSubmit =
                _elementFinder.FindElementWithTimeout(
                    By.XPath("//*[@id='app']/div/span[2]/div/div/div/div/div/div/div[3]/div/button[2]/div/div"), 3);
            Thread.Sleep(3000);
            logoutButtonSubmit.Click();
            Console.WriteLine("logged out");
        }

    public void SearchChat(string searchQuery)
    {
        _elementFinder.WaitForElementToBeVisible(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p"), 10);
        var searchField = _elementFinder.FindElementWithTimeout(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p"), 10);
        searchField.Click();

        searchField.SendKeys(searchQuery);

        Thread.Sleep(5000);
        Console.WriteLine("timeout before clicking is finished");

        var searchResultDivXPath = "//*[@id='pane-side']/div[1]/div/div";
        var searchResultDiv = _elementFinder.FindElementWithTimeout(By.XPath(searchResultDivXPath), 20);
        IList<IWebElement> searchResults = searchResultDiv.FindElements(By.XPath("./*"));

        foreach (var searchResult in searchResults)
        {
            // Your actions for each child element

            // Click on the chat element
            searchResult.Click();
            Console.WriteLine("Result Clicked");

            // Find and print the name of the chat
            var currentChatInfo = _elementFinder.FindElementWithTimeout(By.XPath("//*[@id='main']/header/div[1]/div/img"), 10);

            // Click on the chat to open it
            currentChatInfo.Click();
            Console.WriteLine("Chat info Clicked");

            try
            {
                // Try to find the element with the first XPath = private chat
                var telNumber = _elementFinder.FindElementWithTimeout(
                    By.XPath(
                        "//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/section/div[1]/div[2]/div/span/span"),
                    2);

                // Element with the first XPath exists
                Console.WriteLine("In Private Chat");

                // Perform actions for the first XPath
                var currentNumber = telNumber.Text;
                Console.WriteLine("Current Number: " + currentNumber);
                _webDriver.FindElement(
                    By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/header/div/div[1]/div/span")).Click();
                break;
            }
            catch (WebDriverTimeoutException)
            {
                try
                {
                    // Try to find the element with the second XPath = group chat
                    var notTelNumber = _elementFinder.FindElementWithTimeout(
                        By.XPath(
                            "//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/div/section/div[1]/div/div[3]/span/span"),
                        2);

                    // Element with the second XPath exists
                    Console.WriteLine("Group Chat");
                    _webDriver.FindElement(
                            By.XPath(
                                "//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/header/div/div[1]/div/span"))
                        .Click();
                }
                catch (WebDriverTimeoutException)
                {
                    // Both elements do not exist
                    Console.WriteLine("Neither element exists.");
                    //here I want to close the program
                }
            }

            // Perform other actions as needed
        }
    }

        public void SendMessage(string message)
        {
            var messageInput =
                _elementFinder.FindElementWithTimeout(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"),
                    20);
            messageInput.SendKeys(message);
            messageInput.SendKeys(Keys.Enter);
        }
    }
}