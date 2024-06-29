using OpenQA.Selenium;

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
        private IWebElement navBrand => _driver.FindElement(By.CssSelector(".smaller-75"));
        private IWebElement userNameInfo => _driver.FindElement(By.CssSelector(".user-info"));


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
        public void headerValidate()
        {
            string expectedText = "MantisBT";
            string actualText = navBrand.Text;

            Assert.AreEqual(expectedText, actualText, $"O nome do sistema exibido:'{actualText}' não corresponde ao nome do sistema esperado '{expectedText}'.");
        }

        public void userNameValidate(string username)
        {
            string actualText = userNameInfo.Text;

            Assert.AreEqual(username, actualText, $"O nome do usuário exibido:'{actualText}' não corresponde ao nome do usuário esperado '{username}'.");
        }

        public void homePageValidate(string baseUrl)
        {
            string urlAtual = _driver.Url;

            string urlEsperada = baseUrl + "/my_view_page.php";

            Assert.AreEqual(urlEsperada, urlAtual, $"A URL atual '{urlAtual}' não corresponde à URL esperada '{urlEsperada}'.");
        }
    }
}
