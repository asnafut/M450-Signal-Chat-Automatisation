using MessageSender;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace YourActualNamespace.Tests
{
    [TestClass]
    public class SeleniumUserTests
    {
        private static ChromeDriver webDriver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // Initialize the ChromeDriver for the entire test class
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            webDriver = new ChromeDriver();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Close and quit the ChromeDriver after all tests in the class are executed
            webDriver.Quit();
        }

        [TestMethod]
        public void InitializeWebDriver_ShouldCreateChromeDriverInstance()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();

            // Act
            seleniumUser.InitializeWebDriver();

            // Assert
            Assert.IsNotNull(seleniumUser.WebDriver);
        }

        [TestMethod]
        public void Login_ShouldNavigateToWhatsAppAndDisplayQRCode()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();
            var expectedUrl = "https://web.whatsapp.com";

            // Act
            seleniumUser.Login("yourPhoneNumber");

            // Assert
            Assert.AreEqual(expectedUrl, webDriver.Url);
        }

        [TestMethod]
        public void SearchChat_ShouldFindAndClickOnChat()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();
            var searchQuery = "TestUser";

            // Act
            seleniumUser.SearchChat(searchQuery);

            // Assert
        }

        [TestMethod]
        public void SendMessage_ShouldSendGivenMessage()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();
            var message = "Hello, this is a test message.";

            // Act
            seleniumUser.SendMessage(message);

            // Assert
        }

        [TestMethod]
        public void Logout_ShouldLogoutAndDisplayLogoutMessage()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();

            // Act
            seleniumUser.Logout();

            // Assert
        }

        [TestMethod]
        public void Start_ShouldCompleteLoginSearchSendMessageAndLogout()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();

            // Act
            seleniumUser.Start();

            // Assert
        }

        [TestMethod]
        public void CompleteOtherTest_ShouldNavigateToCyoneWebsiteAndReturnConfirmationButtonText()
        {
            // Arrange
            var seleniumUser = new SeleniumUser();
            var expectedConfirmationButtonText = "ExpectedButtonText";

            // Act
            var actualConfirmationButtonText = seleniumUser.CompletleOtherTest();

            // Assert
            Assert.AreEqual(expectedConfirmationButtonText, actualConfirmationButtonText);
        }
    }
}
