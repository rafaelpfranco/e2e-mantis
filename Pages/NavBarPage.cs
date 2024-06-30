using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using E2EMantis.Interfaces;
using SeleniumExtras.WaitHelpers;

namespace E2EMantis.Pages
{
    internal class NavBarPage : INavBarPage
    {
        private readonly IWebDriver _driver;
        private WebDriverWait _wait;

        public NavBarPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Locators
        private IWebElement userMenu => _driver.FindElement(By.CssSelector("a[data-toggle='dropdown']"));
        private IWebElement userMenuOptions => _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/logout_page.php']")));

        // Método para obter todos os itens de menu da barra de navegação
        private IList<IWebElement> GetMenuItems()
        {
            var sidebar = _driver.FindElement(By.Id("sidebar"));
            var menuItems = sidebar.FindElements(By.CssSelector(".nav-list > li > a"));
            return menuItems;
        }

        // Método para clicar no menu "Minha Visão"
        public void ClickMyVisionMenuItem()
        {
            GetMenuItems().FirstOrDefault(item => item.Text.Contains("Minha Visão"))?.Click();
        }

        // Método para clicar no menu "Ver Tarefas"
        public void ClickViewIssuesMenuItem()
        {
            GetMenuItems().FirstOrDefault(item => item.Text.Contains("Ver Tarefas"))?.Click();
        }

        // Método para clicar no menu "Criar Tarefa"
        public void ClickReportIssuesMenuItem()
        {
            GetMenuItems().FirstOrDefault(item => item.Text.Contains("Criar Tarefa"))?.Click();
        }

        // Método para clicar no menu "Registro de Mudanças"
        public void ClickChangeRegisterMenuItem()
        {
            GetMenuItems().FirstOrDefault(item => item.Text.Contains("Registro de Mudanças"))?.Click();
        }

        // Método para clicar no menu "Planejamento"
        public void ClickPlaningMenuItem()
        {
            GetMenuItems().FirstOrDefault(item => item.Text.Contains("Planejamento"))?.Click();
        }

        public void LogoutUser()
        {
            userMenu.Click();
            userMenuOptions.Click();
        }
    }
}
