using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace MessageSenderConsole
{
    public class MessageSender
    {
        private readonly ElementFinder _elementFinder;
        private readonly WhatsappActions _whatsappActions;

        public string FromTel { get; set; }
        public string ToTel { get; set; }
        public string Message { get; set; }
        

        public readonly ChromeDriver _webDriver;

        public MessageSender()
        {
            _webDriver = InitializeWebDriver();
            _elementFinder = new ElementFinder(_webDriver);
            _whatsappActions = new WhatsappActions(_webDriver, _elementFinder);
        }

        public ChromeDriver InitializeWebDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new ChromeDriver();
        }
        public void TimeoutInit(int seconds, bool feedback = true)
        {
            var ms = seconds * 1000;
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(ms);
            if (feedback) Console.WriteLine($"timeout of {seconds} seconds finished");
        }
        
       
        public void Start()
        {
            InitializeWebDriver();
            TimeoutInit(5);
            _whatsappActions.Login(FromTel);
        }
        
        public void Continue(string toTel, string message)
        {
            // Ensure 'ToTel' is set before calling 'SearchChat'
            if (string.IsNullOrEmpty(toTel))
            {
                Console.WriteLine("ToTel must be set before starting the chat.");
                return;
            }

            // Call 'SearchChat' to find and open the chat
            _whatsappActions.SearchChat(toTel);

            // Send the message only if the chat was successfully opened
            _whatsappActions.SendMessage(message);

        }

        public void End()
        {
            _whatsappActions.Logout();
            Quit();
        }

        public void Quit()
        {
            _webDriver.Quit();
        }
    }
}