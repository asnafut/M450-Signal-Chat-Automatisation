using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using IRobot.UIAutomation.Activities.Browser.JScriptExecutor;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace MessageSender
{
    public class SeleniumUser : ISeleniumUser
    {
        public string FromTel = "defaultTel";
        public string ToTel = "defaultTel";
        public string Message = "defaultTel";
        private ChromeDriver _webDriver;

        public SeleniumUser()
        {
            InitializeWebDriver();
        }
        
        public void SetFromTel(string tel = "defaultTel")
        {
            this.FromTel = tel;
        }

        public void SetToTel(string tel)
        {
            this.ToTel = tel;
        }

        public void SetMessage(string me)
        {
            this.Message = me;
        }

        public void InitializeWebDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _webDriver = new ChromeDriver();
        }

        public void TimeoutInit(int seconds, bool feedback = true)
        {
            int ms = seconds * 1000;
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(ms);
            if (feedback)
            {
                Console.WriteLine($"timeout of {seconds} seconds finished");
            }

        }

        public void Logout()
        {
            var menu = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[5]/div/span"), 3);
            menu.Click();
            var logoutButton = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[5]/span/div/ul/li[7]/div"), 3);
            logoutButton.Click();
            //*[@id="pane-side"]/div[1]/div/div/div[4]/div/div/div/div[1]/div/div/div/img
            var logoutButtonSubmit = FindElementWithTimeout(By.XPath("//*[@id='app']/div/span[2]/div/div/div/div/div/div/div[3]/div/button[2]/div/div"), 3);
            Thread.Sleep(3000);
            logoutButtonSubmit.Click();
            Console.WriteLine("logged out");
        }

        public string GetCode(ChromeDriver driver)
        {
            string firstLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[1]/span")).Text;
            string secondLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[2]/span")).Text;
            string thirdLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[3]/span")).Text;
            string forthLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[4]/span")).Text;
            string fifthLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[5]/span")).Text;
            string sixthLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[6]/span")).Text;
            string seventhLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[7]/span")).Text;
            string eightLetter = driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[2]/div/div/div/div[8]/span")).Text;
            string word = firstLetter + secondLetter + thirdLetter + forthLetter + fifthLetter + sixthLetter + seventhLetter + eightLetter;
            return word;
        }

        public void WaitForElementToBeVisible(By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.ExpectedConditions.ElementIsVisible(by));
            // Console.WriteLine("element" + webDriver.FindElement(by).Text +"found");
        }

        public IWebElement FindElementWithTimeout(By by, int timeoutInSeconds)
        {
            WaitForElementToBeVisible(by, timeoutInSeconds);
            // Console.WriteLine("element" + webDriver.FindElement(by).Text + "set");
            return _webDriver.FindElement(by);
        }

        public void SearchChat(string searchQuery)
        {
            WaitForElementToBeVisible(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p"), 10);
            var searchField = FindElementWithTimeout(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p"), 10);
            searchField.Click();

            searchField.SendKeys(searchQuery);

            Thread.Sleep(5000);
            Console.WriteLine("timeout before clicking is finished");

            string searchResultDivXPath = "//*[@id='pane-side']/div[1]/div/div";
            IWebElement searchResultDiv = FindElementWithTimeout(By.XPath(searchResultDivXPath), 20);
            IList<IWebElement> searchResults = searchResultDiv.FindElements(By.XPath("./*"));

            foreach (IWebElement searchResult in searchResults)
            {
                // Your actions for each child element

                // Click on the chat element
                searchResult.Click();
                Console.WriteLine("Result Clicked");

                // Find and print the name of the chat
                IWebElement currentChatInfo = FindElementWithTimeout(By.XPath("//*[@id='main']/header/div[1]/div/img"), 10);

                // Click on the chat to open it
                currentChatInfo.Click();
                Console.WriteLine("Chat info Clicked");

                try
                {
                    // Try to find the element with the first XPath = private chat
                    IWebElement telNumber = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/section/div[1]/div[2]/div/span/span"), 2);

                    // Element with the first XPath exists
                    Console.WriteLine("In Private Chat");

                    // Perform actions for the first XPath
                    string currentNumber = telNumber.Text;
                    Console.WriteLine("Current Number: " + currentNumber);
                    _webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/header/div/div[1]/div/span")).Click();
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    try
                    {
                        // Try to find the element with the second XPath = group chat
                        IWebElement notTelNumber = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/div/section/div[1]/div/div[3]/span/span"), 2);

                        // Element with the second XPath exists
                        Console.WriteLine("Group Chat");
                        _webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/header/div/div[1]/div/span")).Click();

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
            var messageInput = FindElementWithTimeout(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"), 20);
            messageInput.SendKeys(message);
            messageInput.SendKeys(Keys.Enter);
        }

        public void Login(string number)
        {
            InitializeWebDriver();

            TimeoutInit(5);
            _webDriver.Navigate().GoToUrl("https://web.whatsapp.com");

            WaitForElementToBeVisible(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span"), 10);

            _webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span")).Click();

            //warten bis input für tel Nummer sichtbar ist
            var input = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[1]/div[2]/div/div/div/form/input"), 20);
            //input für tel Nummer
            input.Click();
            input.SendKeys(number);
            //Weiter Button
            var weiterButton = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[2]/button"), 20);
            weiterButton.Click();

            Console.WriteLine("Wait for your Phone to send you a message from whatsapp and enter the Code you see on the Display");
            Console.WriteLine($"the code is {GetCode(_webDriver)}");

            WaitForElementToBeVisible(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[1]/div/span"), 100);
            Console.WriteLine("login finished");
        }


        public void Start()
        {
            Login(FromTel);
            SearchChat(ToTel);
            SendMessage(Message);
            Logout();
            Thread.Sleep(5000);
            TimeoutInit(5);
            _webDriver.Quit();
        }

        public void Quit()
        {
            _webDriver.Quit();
        }
        
    }
}