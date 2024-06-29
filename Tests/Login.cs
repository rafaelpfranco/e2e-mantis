// Tests/LoginTests.cs
using System;
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

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Configurar o carregamento do appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        [SetUp]
        public void SetUp()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var options = new ChromeOptions();
            _driver = new ChromeDriver(path + @"\drivers\", options);
            _loginPage = new LoginPage(_driver);
        }

        [Test]
        public void LoginValidCredentials()
        {
            // Usar a URL base e as credenciais do appsettings.json
            string baseUrl = Configuration["Environment:BaseUrl"];
            string username = Configuration["Environment:UserName"];
            string password = Configuration["Environment:Password"];

            _driver.Navigate().GoToUrl(baseUrl + "/login.php");

            // Realizar o login
            _loginPage.Login(username, password);

            // Asserts
            _loginPage.userNameValidate(username);
            _loginPage.homePageValidate(baseUrl);
            _loginPage.headerValidate();
        }


        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
            _driver = null;
        }
    }
}
