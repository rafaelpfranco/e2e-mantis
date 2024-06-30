using E2EMantis.Interfaces;
using E2EMantis.Pages;
using E2EMantis.Utils;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EMantis.Tests
{
    [TestFixture]
    internal class ViewIssueTests
    {
        private IWebDriver _driver;
        private ILoginPage _loginPage;
        private INavBarPage _navBarPage;
        private ITaskPage _taskPage;
        private IViewIssuesPage _viewIssuesPage;
        private static IConfiguration Configuration { get; set; }

        private static string BaseUrl { get; set; }
        private static string UserName { get; set; }
        private static string Password { get; set; }
        private static string EmptyError { get; set; }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Program.ConfigureSettings();

            // Inicializar propriedades com valores da configuração
            BaseUrl = Program.Configuration["Environment:BaseUrl"];
            UserName = Program.Configuration["User:UserName"];
            Password = Program.Configuration["User:Password"];
            EmptyError = Program.Configuration["Error:search_issue_empty_id"];
        }

        [SetUp]
        public void SetUp()
        {
            SetupDriver();
            NavigateToLoginPage();
            _loginPage.Login(UserName, Password);
            NavigateToViewIssue();
        }

        //[TearDown]
        //public void TearDown()
        //{
        //    _driver.Dispose();
        //    _driver = null;
        //}
        private void SetupDriver()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var options = new ChromeOptions();
            _driver = new ChromeDriver(path + @"\drivers\", options);
            _loginPage = new LoginPage(_driver);
            _navBarPage = new NavBarPage(_driver);
            _taskPage = new TaskPage(_driver);
            _viewIssuesPage = new ViewIssuesPage(_driver);

        }

        private void NavigateToLoginPage()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(BaseUrl + "/login.php");
        }

        private void NavigateToViewIssue()
        {
            _navBarPage.ClickViewIssuesMenuItem();
        }

        [Test]
        public void SearchBySummary()
        {
            _viewIssuesPage.SearchElement("Handcrafted Rubber Sausages");

            // Asserts
            _viewIssuesPage.FindElementInTable("Handcrafted Rubber Sausages");
        }

        [Test]
        public void SearchByIssueID()
        {
            string summary = "0000949: Refined Metal Chicken";
            _viewIssuesPage.SearchById("0000949");

            // Asserts
            _taskPage.SumarayValidate(summary);
        }

        [Test]
        public void SearchByIssueIDEmptyField()
        {
            _viewIssuesPage.SearchById("");

            // Asserts
            _taskPage.MessageValidate(_taskPage.PageContent, EmptyError);
        }
    }
}
