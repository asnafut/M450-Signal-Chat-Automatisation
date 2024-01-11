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
        private string FromTel;
        private string ToTel;
        private string Message;
        private ChromeDriver webDriver;

        public void setFromTel(string Tel)
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
            var menu = webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[5]/div/span"));
            menu.Click();
            timeoutInit(3);
            var logoutButton = webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/header/div[2]/div/span/div[5]/div/span"));
            logoutButton.Click();
            //*[@id="pane-side"]/div[1]/div/div/div[4]/div/div/div/div[1]/div/div/div/img
            var logoutButtonSubmit = webDriver.FindElement(By.XPath("//*[@id='pane-side']/div[1]/div/div/div[4]/div/div/div/div[1]/div/div/div/img"));
            logoutButton.Click();

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

        private void searchChat(string searchQuery)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(target_xpath)));
 
            SearchResult.Click();
            Thread.Sleep(5000);
            timeoutInit(5);
            var searchField = webDriver.FindElement(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p"));
            searchField.Click();
            searchField.SendKeys(searchQuery);
            Thread.Sleep(7000);
            timeoutInit(7);
            Console.WriteLine("checkpoint before finding");
            var firstResult = webDriver.FindElement(By.XPath("//*[@id='pane-side']/div[1]/div/div/div[4]/div/div/div/div[1]/div/div/div/img"));
            Thread.Sleep(5000);
            timeoutInit(5);
            firstResult.Click();
        }

        private void sendMessage(string message)
        {
            var messageInput = webDriver.FindElement(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"));
            messageInput.SendKeys(message);
            messageInput.SendKeys(Keys.Enter);
        }
        
        private void login(string number)
        {
            InitializeWebDriver();

            timeoutInit( 5);
            webDriver.Navigate().GoToUrl("https://web.whatsapp.com");
            timeoutInit( 5);
            webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div/div[3]/div/span")).Click();
            
            timeoutInit( 5);

            
            var numInput = webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[2]/div/div/div/form/input"));
            var weiterButton = webDriver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[3]/div[1]/div/div[3]/div[3]/div/div/div"));        
            
            numInput.SendKeys(number);
            timeoutInit( 5);
            
            weiterButton.Click();
            
            timeoutInit(5);

            Console.WriteLine("Wait for your Phone to send you a message from whatsapp and enter the Code you see on the Display");
            Console.WriteLine($"the code is {getCode(webDriver)}");
            
            Thread.Sleep(60000);
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