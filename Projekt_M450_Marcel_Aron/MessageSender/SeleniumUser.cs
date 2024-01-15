using System.Text.RegularExpressions;
using IRobot.UIAutomation.Activities.Browser.JScriptExecutor;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace MessageSender
{
    public class SeleniumUser
    {
        private string FromTel = "defaultTel";
        private string ToTel = "defaultTel";
        private string Message = "defaultTel";
        private ChromeDriver webDriver;

        public SeleniumUser()
        {
            InitializeWebDriver();
        }

        public void setFromTel(string Tel = "defaultTel")
        {
            this.FromTel = Tel;
        }

        public void setToTel(string Tel)
        {
            this.ToTel = Tel;
        }

        public void setMessage(string Me)
        {
            this.Message = Me;
        }

        private void InitializeWebDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            webDriver = new ChromeDriver();
        }

        public void sendMessage()
        {
            InitializeWebDriver();

            webDriver.Navigate().GoToUrl("https://web.whatsapp.com");

            // Your WhatsApp automation logic here

            webDriver.Quit();
        }

        private void timeoutInit(int seconds, bool feedback = true)
        {
            int ms = seconds * 1000;
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(ms);
            if (feedback)
            {
                Console.WriteLine($"timeout of {seconds} seconds finished");
            }

        }

        private void logout()
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

        private string getCode(ChromeDriver driver)
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
        
        private void WaitForElementToBeVisible(By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.ExpectedConditions.ElementIsVisible(by));
            // Console.WriteLine("element" + webDriver.FindElement(by).Text +"found");
        }

        private IWebElement FindElementWithTimeout(By by, int timeoutInSeconds)
        {
            WaitForElementToBeVisible(by, timeoutInSeconds);
            // Console.WriteLine("element" + webDriver.FindElement(by).Text + "set");
            return webDriver.FindElement(by);
        }

        private void searchChat(string searchQuery)
        {
            // // Example: Wait for an element with ID "exampleElement" to be present
            // WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            //
            // // Replace "exampleElement" with the actual ID of the element you are waiting for
            // IWebElement element = wait.Until(SeleniumExtras.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p")));
            //
            // // Your code to interact with the element after it's loaded
            // element.Click();
            
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
                    webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/header/div/div[1]/div/span")).Click();
                    break;


                    // // Extract only digits using regex
                    // string extractedDigits = new string(currentNumber.Where(char.IsDigit).ToArray());
                    // Console.WriteLine("Extracted Digits: " + extractedDigits);
                    //
                    // if (int.TryParse(extractedDigits, out int phoneNumber))
                    // {
                    //     // Successfully parsed as an integer
                    //     Console.WriteLine("Phone Number as Integer: " + phoneNumber);
                    //     if (int.TryParse(searchQuery, out int inputPhoneNumber))
                    //     {
                    //         // Successfully parsed as an integer
                    //         Console.WriteLine("Another Number as Integer: " + inputPhoneNumber);
                    //
                    //         // Now you can compare phoneNumber and anotherNumber
                    //         if (phoneNumber == inputPhoneNumber)
                    //         {
                    //             Console.WriteLine("Phone Number and Inputed number are equal.");
                    //             
                    //             //here I want to get out of the loop
                    //             
                    //         }
                    //         else
                    //         {
                    //             Console.WriteLine("Phone Number and Another Number are not equal.");
                    //             //here I want to close the program
                    //         }
                    //     }
                    //     else
                    //     {
                    //         // Could not parse anotherNumberString as an integer
                    //         Console.WriteLine("Another Number is not a valid integer.");
                    //         //here I want to close the program
                    //     }                    
                    // }
                    // else
                    // {
                    //     // Could not parse as an integer
                    //     Console.WriteLine("Extracted digits are not a valid integer.");
                    //     //here I want to close the program
                    // }                    
                    // // Perform other actions as needed

                }
                catch (WebDriverTimeoutException)
                {
                    try
                    {
                        // Try to find the element with the second XPath = group chat
                        IWebElement notTelNumber = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/div/section/div[1]/div/div[3]/span/span"), 2);

                        // Element with the second XPath exists
                        Console.WriteLine("Group Chat");
                        webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[5]/span/div/span/div/div/header/div/div[1]/div/span")).Click();

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
            

            // var firstResult = FindElementWithTimeout(By.XPath("//*[@id='pane-side']/div[1]/div/div/div[5]"), 20);
            // Console.WriteLine("first result found");
            // firstResult.Click();

            // Thread.Sleep(5000);
            // timeoutInit(5);
            // var searchField = webDriver.FindElement(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p"));
            // searchField.Click();
            // searchField.SendKeys(searchQuery);
            // Thread.Sleep(7000);
            // timeoutInit(7);
            // Console.WriteLine("checkpoint before finding");
            // var firstResult = webDriver.FindElement(By.XPath("//*[@id='pane-side']/div[1]/div/div/div[4]/div/div/div/div[1]/div/div/div/img"));
            // Thread.Sleep(5000);
            // timeoutInit(5);
            // firstResult.Click();
        }

        private void sendMessage(string message)
        {
            var messageInput =FindElementWithTimeout(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"), 20);
            messageInput.SendKeys(message);
            messageInput.SendKeys(Keys.Enter);
        }
        
        private void login(string number)
        {
            InitializeWebDriver();

            timeoutInit( 5);
            webDriver.Navigate().GoToUrl("https://web.whatsapp.com");
            
            WaitForElementToBeVisible(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span"),10 );

            webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span")).Click();

            //warten bis input für tel Nummer sichtbar ist
            var input = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[1]/div[2]/div/div/div/form/input"), 20);
            //input für tel Nummer
            input.Click();
            input.SendKeys(number);
            //Weiter Button
            var weiterButton = FindElementWithTimeout(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[2]/button"), 20);
            weiterButton.Click();
            
            Console.WriteLine("Wait for your Phone to send you a message from whatsapp and enter the Code you see on the Display");
            Console.WriteLine($"the code is {getCode(webDriver)}");
            
            WaitForElementToBeVisible(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[1]/div/span"), 100);
            Console.WriteLine("login finished");
        }


        public void start()
        {
            login(FromTel);
            searchChat(ToTel);
            sendMessage(Message);
            logout();
            Thread.Sleep(5000);
            timeoutInit(5);
            webDriver.Quit();
        }

        public string completleOtherTest()
        {
            InitializeWebDriver();

            webDriver.Navigate().GoToUrl("https://www.cyone.ch");
            string word = webDriver.FindElement(By.XPath("//*[@id='hs-eu-confirmation-button']")).Text;

            webDriver.Quit();

            return word;
        }
    }
}