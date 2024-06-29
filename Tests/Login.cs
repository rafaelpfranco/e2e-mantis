// Tests/LoginTests.cs
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using E2EMantis.Pages;

namespace E2EMantis.Tests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private static IConfiguration Configuration { get; set; }

        private static string BaseUrl { get; set; }
        private static string UserName { get; set; }
        private static string Password { get; set; }
        private static string ErrorLogin { get; set; }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            string pathFixtures = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            // Configurar o carregamento do appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(pathFixtures + "/Fixtures/error_message.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Inicializar propriedades com valores da configuração
            BaseUrl = Configuration["Environment:BaseUrl"];
            UserName = Configuration["Environment:UserName"];
            Password = Configuration["Environment:Password"];
            ErrorLogin = Configuration["Error:invalid_login"];
        }

        [SetUp]
        public void SetUp()
        {
            SetupDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
            _driver = null;
        }

        private void SetupDriver()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var options = new ChromeOptions();
            _driver = new ChromeDriver(path + @"\drivers\", options);
            _loginPage = new LoginPage(_driver);
        }

        private void NavigateToLoginPage()
        {
            _driver.Navigate().GoToUrl(BaseUrl + "/login.php");
        }

        [Test]
        public void LoginValidCredentials()
        {
            NavigateToLoginPage();

            // Realizar o login
            _loginPage.Login(UserName, Password);

            // Asserts
            _loginPage.UrlValidate(BaseUrl + "/my_view_page.php");
            _loginPage.UserNameValidate(UserName);
            _loginPage.HeaderValidate();
        }

        [Test]
        public void BlankUsernameLoginTest()
        {
            NavigateToLoginPage();

            _loginPage.ClickEnterButton();

            // Asserts
            _loginPage.ErrorMessageValidate(ErrorLogin);
            _loginPage.UrlValidate(BaseUrl + "/login_page.php?error=1&return=my_view_page.php");
        }

        [Test]
        public void BlankPasswordLoginTest()
        {
            NavigateToLoginPage();

            _loginPage.EnterUsername(UserName);
            _loginPage.ClickEnterButton();
            _loginPage.ClickEnterButton();

            // Asserts
            _loginPage.ErrorMessageValidate(ErrorLogin);
            _loginPage.UrlValidate(BaseUrl + "/login_page.php?error=1&username=" + UserName + "&return=my_view_page.php");
        }

        [Test]
        public void InvalidUsernameLoginTest()
        {
            NavigateToLoginPage();

            _loginPage.EnterUsername("Invalid_Username");
            _loginPage.ClickEnterButton();
            _loginPage.EnterPassword(Password);
            _loginPage.ClickEnterButton();

            // Asserts
            _loginPage.ErrorMessageValidate(ErrorLogin);
            _loginPage.UrlValidate(BaseUrl + "/login_page.php?error=1&username=Invalid_Username&return=my_view_page.php");
        }

        [Test]
        public void InvalidPasswordLoginTest()
        {
            NavigateToLoginPage();

            _loginPage.EnterUsername(UserName);
            _loginPage.ClickEnterButton();
            _loginPage.EnterPassword("Invalid_password");
            _loginPage.ClickEnterButton();

            // Asserts
            _loginPage.ErrorMessageValidate(ErrorLogin);
            _loginPage.UrlValidate(BaseUrl + "/login_page.php?error=1&username=" + UserName + "&return=my_view_page.php");
        }
    }
}
