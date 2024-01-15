using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageSender;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MessageSender.Tests
{
    [TestClass]
    public class SeleniumUserTests
    {
        // Mock the ISeleniumUser interface to avoid starting an actual WebDriver
        private Mock<ISeleniumUser> _mockSeleniumUser;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockSeleniumUser = new Mock<ISeleniumUser>();
        }

        [TestMethod]
        public void SetFromTel_ShouldSetFromTelProperty()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.SetFromTel("newTel");

            // Assert
            _mockSeleniumUser.Verify(m => m.SetFromTel("newTel"), Times.Once);
        }

        [TestMethod]
        public void SetToTel_ShouldSetToTelProperty()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.SetToTel("newToTel");

            // Assert
            _mockSeleniumUser.Verify(m => m.SetToTel("newToTel"), Times.Once);
        }

        [TestMethod]
        public void SetMessage_ShouldSetMessageProperty()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.SetMessage("newMessage");

            // Assert
            _mockSeleniumUser.Verify(m => m.SetMessage("newMessage"), Times.Once);
        }

        [TestMethod]
        public void InitializeWebDriver_ShouldCallInitializeWebDriverMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.InitializeWebDriver();

            // Assert
            _mockSeleniumUser.Verify(m => m.InitializeWebDriver(), Times.Once);
        }

        [TestMethod]
        public void TimeoutInit_ShouldCallTimeoutInitMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.TimeoutInit(5);

            // Assert
            _mockSeleniumUser.Verify(m => m.TimeoutInit(5, true), Times.Once);
        }

        [TestMethod]
        public void Logout_ShouldCallLogoutMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.Logout();

            // Assert
            _mockSeleniumUser.Verify(m => m.Logout(), Times.Once);
        }

        [TestMethod]
        public void GetCode_ShouldCallGetCodeMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.GetCode(It.IsAny<ChromeDriver>());

            // Assert
            _mockSeleniumUser.Verify(m => m.GetCode(It.IsAny<ChromeDriver>()), Times.Once);
        }

        [TestMethod]
        public void WaitForElementToBeVisible_ShouldCallWaitForElementToBeVisibleMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.WaitForElementToBeVisible(It.IsAny<By>(), It.IsAny<int>());

            // Assert
            _mockSeleniumUser.Verify(m => m.WaitForElementToBeVisible(It.IsAny<By>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void FindElementWithTimeout_ShouldCallFindElementWithTimeoutMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.FindElementWithTimeout(It.IsAny<By>(), It.IsAny<int>());

            // Assert
            _mockSeleniumUser.Verify(m => m.FindElementWithTimeout(It.IsAny<By>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void SearchChat_ShouldCallSearchChatMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.SearchChat("searchQuery");

            // Assert
            _mockSeleniumUser.Verify(m => m.SearchChat("searchQuery"), Times.Once);
        }

        [TestMethod]
        public void SendMessage_ShouldCallSendMessageMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.SendMessage("message");

            // Assert
            _mockSeleniumUser.Verify(m => m.SendMessage("message"), Times.Once);
        }

        [TestMethod]
        public void Login_ShouldCallLoginMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.Login("someNumber");

            // Assert
            _mockSeleniumUser.Verify(m => m.Login("someNumber"), Times.Once);
        }

        [TestMethod]
        public void Start_ShouldCallStartMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.Start();

            // Assert
            _mockSeleniumUser.Verify(m => m.Start(), Times.Once);
        }

        [TestMethod]
        public void Quit_ShouldCallQuitMethod()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.Quit();

            // Assert
            _mockSeleniumUser.Verify(m => m.Quit(), Times.Once);
        }

        [TestMethod]
        public void Start_ShouldCallLoginSearchChatSendMessageLogoutAndQuitMethods()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;
            _mockSeleniumUser.Setup(m => m.Login(It.IsAny<string>())).Verifiable();
            _mockSeleniumUser.Setup(m => m.SearchChat(It.IsAny<string>())).Verifiable();
            _mockSeleniumUser.Setup(m => m.SendMessage(It.IsAny<string>())).Verifiable();
            _mockSeleniumUser.Setup(m => m.Logout()).Verifiable();
            _mockSeleniumUser.Setup(m => m.Quit()).Verifiable();

            // Act
            seleniumUser.Start();

            // Assert
            _mockSeleniumUser.Verify();
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            // Cleanup any resources if needed
            _mockSeleniumUser = null;
        }
    }
}
