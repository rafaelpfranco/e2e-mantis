using OpenQA.Selenium;
using NUnit.Framework;
using E2EMantis.Interfaces;

namespace E2EMantis.Pages
{
    public class LoginPage : ILoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        private IWebElement UsernameField => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement EnterButton => _driver.FindElement(By.CssSelector("input[type='submit'].btn-success"));
        private IWebElement NavBrand => _driver.FindElement(By.CssSelector(".smaller-75"));
        private IWebElement UserNameInfo => _driver.FindElement(By.CssSelector(".user-info"));
        private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector(".alert.alert-danger"));

        // Métodos para interagir com a página
        public void EnterUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }

        public void ClickEnterButton()
        {
            EnterButton.Click();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            ClickEnterButton();
            EnterPassword(password);
            ClickEnterButton();
        }

        // Asserts
        public void HeaderValidate()
        {
            string expectedText = "MantisBT";
            string actualText = NavBrand.Text;

            Assert.AreEqual(expectedText, actualText, $"O nome do sistema exibido:'{actualText}' não corresponde ao nome do sistema esperado '{expectedText}'.");
        }

        public void UserNameValidate(string username)
        {
            string actualText = UserNameInfo.Text;

            Assert.AreEqual(username, actualText, $"O nome do usuário exibido:'{actualText}' não corresponde ao nome do usuário esperado '{username}'.");
        }

        public void UrlValidate(string baseUrl)
        {
            string actualUrl = _driver.Url;

            StringAssert.Contains(baseUrl, actualUrl, $"A URL atual '{actualUrl}' não contém a URL esperada '{baseUrl}'.");
        }

        public void ErrorMessageValidate(string errorMessage) 
        {
            string actualText = ErrorMessage.Text;

            Assert.AreEqual(errorMessage, actualText, $"A mensagem de erro exibida:'{actualText}' não corresponde com a mensagem de erro esperada '{errorMessage}'.");
        }
    }
}
