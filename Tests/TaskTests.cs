using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using E2EMantis.Pages;
using E2EMantis.Interfaces;
using E2EMantis.Utils;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace E2EMantis.Tests
{
    [TestFixture]
    internal class TaskTests
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
        private static string SuccessIssue { get; set; }
        private static string WithouCategory { get; set; }
        private static string EmptyFields { get; set; }
        private static string WithouAnotation { get; set; }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Program.ConfigureSettings();

            // Inicializar propriedades com valores da configuração
            BaseUrl = Program.Configuration["Environment:BaseUrl"];
            UserName = Program.Configuration["User:UserName"];
            Password = Program.Configuration["User:Password"];
            SuccessIssue = Program.Configuration["Success:new_issue"];
            WithouCategory = Program.Configuration["Error:without_category"];
            EmptyFields = Program.Configuration["Error:empty_fields"];
            WithouAnotation = Program.Configuration["Error:without_anotation"];
        }

        [SetUp]
        public void SetUp()
        {
            SetupDriver();
            NavigateToLoginPage();
            _loginPage.Login(UserName, Password);
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
            _navBarPage = new NavBarPage(_driver);
            _taskPage = new TaskPage(_driver);
            _viewIssuesPage = new ViewIssuesPage(_driver);

        }
        private void NavigateToLoginPage()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(BaseUrl + "/login.php");
        }

        private void NavigateToCreateIssue()
        {
            _navBarPage.ClickReportIssuesMenuItem();
        }

        private void NavigateToViewIssue()
        {
            _navBarPage.ClickViewIssuesMenuItem();
        }

        [Test]
        public void CreateIssueValidFields()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string pathFixtures = Path.Combine(path, "Fixtures", "bug-report.jpg");
            string issueName = GenerateData.GenerateIssueName();
            NavigateToCreateIssue();

            _taskPage.CreateIssue("[Todos os Projetos] categoria teste", "sempre", "grande", "normal", "Windows", "Windows 10", "10.0.0", issueName, "This is a test description", "No steps to reproduce", "No additional info", false, "tagTest1,tagTest2", "bug", pathFixtures);

            // Asserts
            _taskPage.MessageValidate(_taskPage.SucessMessage, SuccessIssue);

            Thread.Sleep(2000);
            NavigateToViewIssue();

            _viewIssuesPage.SearchElement(issueName);

            // Asserts
            _viewIssuesPage.FindElementInTable(issueName);

        }

        [Test]
        public void CreateIssueWithoutCategory()
        {
            string issueName = GenerateData.GenerateIssueName();
            NavigateToCreateIssue();

            _taskPage.TypeElement(_taskPage.SummaryInput, issueName);
            _taskPage.TypeElement(_taskPage.DescriptionInput, "Description: " + issueName);

            _taskPage.ClickElement(_taskPage.CreateIssueButton);

            // Asserts
            _taskPage.MessageValidate(_taskPage.PageContent, WithouCategory);
        }

        [Test]
        public void CreateIssueEmptyFields()
        {
            string issueName = GenerateData.GenerateIssueName();

            NavigateToCreateIssue();

            _taskPage.ClickElement(_taskPage.CreateIssueButton);

            // Asserts 
            _taskPage.ToolTipValidate(_taskPage.SummaryInput, EmptyFields);

            _taskPage.TypeElement(_taskPage.SummaryInput, issueName);
            _taskPage.ClickElement(_taskPage.CreateIssueButton);

            // Asserts
            _taskPage.ToolTipValidate(_taskPage.DescriptionInput, EmptyFields);

        }

        [Test]
        public void CommentOnIssueWithText()
        {
            string randomNumber = GenerateData.GenerateRandomNumber();
            string commentText = "This is a test comment number: " + randomNumber; 
            NavigateToViewIssue();

            _viewIssuesPage.SearchElement("Sleek Cotton Sausages");
            _viewIssuesPage.FindElementInTable("Sleek Cotton Sausages");
            _viewIssuesPage.OpenIssue();

            _taskPage.CreateNoteIssue(commentText);

            // Asserts
            _taskPage.MessageValidate(_taskPage.CommentsField, commentText);
        }

        [Test]
        public void CommentOnIssueWithImage()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string pathFixtures = Path.Combine(path, "Fixtures", "bug-report.jpg");
            NavigateToViewIssue();

            _viewIssuesPage.SearchElement("Sleek Cotton Sausages");
            _viewIssuesPage.FindElementInTable("Sleek Cotton Sausages");
            _viewIssuesPage.OpenIssue();

            _taskPage.CreateNoteWithImageIssue(pathFixtures);

            // Asserts
            //_taskPage.MessageValidate(_taskPage.CommentsField, commentText);
        }

        [Test]
        public void CommentOnIssueEmptyField()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string pathFixtures = Path.Combine(path, "Fixtures", "bug-report.jpg");
            NavigateToViewIssue();

            _viewIssuesPage.SearchElement("Sleek Cotton Sausages");
            _viewIssuesPage.FindElementInTable("Sleek Cotton Sausages");
            _viewIssuesPage.OpenIssue();

            _taskPage.ClickElement(_taskPage.CreateNote);

            // Asserts
            _taskPage.MessageValidate(_taskPage.PageContent, WithouAnotation);
        }
    }
}
