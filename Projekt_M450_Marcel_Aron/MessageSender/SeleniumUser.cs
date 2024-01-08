using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using sele

namespace MessageSender;

public class SeleniumUser
{
    public int id;
    private string tel;
    private string message;

    public void setTel(string Tel)
    {
        this.tel = Tel;
    }
    public void setMessage(string Me)
    {
        this.message = Me;
    }

    private void sendMessage()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://web.whatsapp.com");
        //suchfeld
        driver.FindElement(By.XPath("//*[@id='side']/div[1]/div/div[2]/div[2]/div/div[1]/p")).Click();
        //enter number
        
        //click enter
        
        //click first result
        driver.FindElement(By.XPath("//*[@id='pane-side']/div[1]/div/div/div[5]/div/div/div/div[2]/div[1]/div[1]/div")).Click();

    }
}