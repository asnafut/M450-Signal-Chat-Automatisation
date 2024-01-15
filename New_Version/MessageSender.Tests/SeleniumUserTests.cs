using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;

namespace MessageSender.Tests
{
    [TestClass]
    public class SeleniumUserTests
    {
        private Mock<ISeleniumUser> _mockSeleniumUser;
        private Mock<IWebElement> _mockWebElement;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockSeleniumUser = new Mock<ISeleniumUser>();
        }

        // ... (Other test methods)

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
        public void End_ShouldCallLogoutAndQuitMethods()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.End();

            // Assert
            _mockSeleniumUser.Verify(m => m.Logout(), Times.Once);
            _mockSeleniumUser.Verify(m => m.Quit(), Times.Once);
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
        public void Start_ShouldCallSearchChatAndSendMessageMethods()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;
            seleniumUser.ToTel = "someToTel";
            seleniumUser.Message = "someMessage";

            // Act
            seleniumUser.Start();

            // Assert
            _mockSeleniumUser.Verify(m => m.SearchChat("someToTel"), Times.Once);
            _mockSeleniumUser.Verify(m => m.SendMessage("someMessage"), Times.Once);
        }

        [TestMethod]
        public void Login_ShouldCallInitializeWebDriverTimeoutInitAndGetCodeMethods()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act
            seleniumUser.Login("someNumber");

            // Assert
            _mockSeleniumUser.Verify(m => m.InitializeWebDriver(), Times.Once);
            _mockSeleniumUser.Verify(m => m.TimeoutInit(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
            _mockSeleniumUser.Verify(m => m.GetCode(It.IsAny<ChromeDriver>()), Times.Once);
        }
        
        [TestMethod]
        public void FindElementWithTimeout_ShouldThrowWebDriverTimeoutExceptionIfElementNotFound()
        {
            // Arrange
            var seleniumUser = _mockSeleniumUser.Object;

            // Act & Assert
            Action action = () => seleniumUser.FindElementWithTimeout(By.XPath("nonExistentXPath"), 2);

            // Assert
            action.Should().Throw<WebDriverTimeoutException>();
        }

    }
}
