using OpenQA.Selenium;
using System.Security.Cryptography.X509Certificates;

namespace E2EMantis.Pages
{
    public class LoginPage
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
        private IWebElement ErroMessage => _driver.FindElement(By.CssSelector(".alert.alert-danger"));

        // Métodos para interagir com a página
        public void EnterUsername(string username)
        {
            UsernameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
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

            string expectUrl = baseUrl;

            Assert.AreEqual(expectUrl, actualUrl, $"A URL atual '{actualUrl}' não corresponde à URL esperada '{expectUrl}'.");
        }

        public void ErrorMessageValidate(string erro) 
        {
            string actualText = ErroMessage.Text;

            Assert.AreEqual(erro, actualText, $"A mensagem de erro exibida:'{actualText}' não corresponde com a mensagem de erro esperada '{erro}'.");
        }
    }
}
