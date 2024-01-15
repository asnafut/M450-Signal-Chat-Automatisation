using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MessageSender
{
    public interface ISeleniumUser
    {
        void SetFromTel(string tel = "defaultTel");
        void SetToTel(string tel);
        void SetMessage(string me);
        void InitializeWebDriver();
        void TimeoutInit(int seconds, bool feedback = true);
        void Logout();
        string GetCode(ChromeDriver driver);
        void WaitForElementToBeVisible(By by, int timeoutInSeconds);
        IWebElement FindElementWithTimeout(By by, int timeoutInSeconds);
        void SearchChat(string searchQuery);
        void SendMessage(string message);
        void Login(string number);
        void Start();
        void Quit();
    }
}