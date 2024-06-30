using OpenQA.Selenium;
using NUnit.Framework;
using E2EMantis.Interfaces;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace E2EMantis.Pages
{
    internal class ViewIssuesPage : IViewIssuesPage
    {
        private readonly IWebDriver _driver;

        public ViewIssuesPage(IWebDriver driver)
        {
            _driver = driver; 
        }
       

        // Locators
        private IWebElement SearchTextField => _driver.FindElement(By.Id("filter-search-txt"));
        private IWebElement SearchTextButton => _driver.FindElement(By.CssSelector("input[name='filter_submit'][type='submit']\r\n"));
        private IWebElement TableList => _driver.FindElement(By.CssSelector("#bug_action tbody tr"));
        private IWebElement IssueLink => TableList.FindElement(By.CssSelector(".column-id a"));

        public void SearchElement(string searchText)
        {
            SearchTextField.Clear();
            SearchTextField.SendKeys(searchText);

            SearchTextButton.Click();
        }

        // Assertions
        public void FindElementInTable(string searchText)
        {
           bool isElementFound = TableList.Text.Contains(searchText);
           Assert.IsTrue(isElementFound, "Elemento não encontrado na tabela.");
        }

        public void OpenIssue()
        {
            IssueLink.Click();
        }
    }
}
