using OpenQA.Selenium;
using NUnit.Framework;
using E2EMantis.Interfaces;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace E2EMantis.Pages
{
    internal class TaskPage : ITaskPage
    {
        private readonly IWebDriver _driver;

        public TaskPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        private IWebElement CategorySelect => _driver.FindElement(By.Id("category_id"));
        private IWebElement ReproducibilitySelect => _driver.FindElement(By.Id("reproducibility"));
        private IWebElement SeveritySelect => _driver.FindElement(By.Id("severity"));
        private IWebElement PrioritySelect => _driver.FindElement(By.Id("priority"));
        private IWebElement OpenProfileButton => _driver.FindElement(By.Id("profile_closed_link"));
        private IWebElement PlatformInput => _driver.FindElement(By.Id("platform"));
        private IWebElement OSInput => _driver.FindElement(By.Id("os"));
        private IWebElement OSVersionInput => _driver.FindElement(By.Id("os_build"));
        public IWebElement SummaryInput => _driver.FindElement(By.Id("summary"));
        public IWebElement DescriptionInput => _driver.FindElement(By.Id("description"));
        private IWebElement StepsToReproduceInput => _driver.FindElement(By.Id("steps_to_reproduce"));
        private IWebElement AdditionalInformationInput => _driver.FindElement(By.Id("additional_info"));
        private IWebElement TagInput => _driver.FindElement(By.Id("tag_string"));
        private IWebElement TagSelect => _driver.FindElement(By.Id("tag_select"));
        private IWebElement fileInput => _driver.FindElement(By.CssSelector("input[type=file]"));
        private IWebElement PublicRadio => _driver.FindElement(By.CssSelector("input[type='radio'][value='10']"));
        private IWebElement PrivateRadio => _driver.FindElement(By.CssSelector("input[type='radio'][value='50']"));
        private IWebElement MoreIssueSelect => _driver.FindElement(By.Id("report_stay"));
        public IWebElement CreateIssueButton => _driver.FindElement(By.CssSelector("input[type='submit'][value='Criar Nova Tarefa']"));
        public IWebElement SucessMessage => _driver.FindElement(By.CssSelector(".alert.alert-success"));
        public IWebElement PageContent => _driver.FindElement(By.CssSelector(".alert.alert-danger"));
        public IWebElement NoteText => _driver.FindElement(By.Id("bugnote_text"));
        public IWebElement CreateNote => _driver.FindElement(By.CssSelector("input[type='submit'][value='Adicionar Anotação']"));
        public IWebElement TableComments => _driver.FindElement(By.CssSelector("#bugnotes table tbody"));
        public IWebElement NoteCommentsField => TableComments.FindElement(By.CssSelector(".collapse-open.noprint"));
        private IWebElement SumaryIssue => _driver.FindElement(By.CssSelector(".bug-summary:not(.category)"));

        public void TypeElement(IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public void SelectElement(IWebElement selectElement, string value)
        {
            var SelectElement = new SelectElement(selectElement);
            SelectElement.SelectByText(value);
        }

        public void ClickElement(IWebElement selectElement)
        {
            if (IsElementVisible(selectElement))
            {
                selectElement.Click();
            }
            else
            {
                Console.WriteLine("O botão não está visível!");
            }
        }

        private bool IsElementVisible(IWebElement element)
        {
            try
            {
                return element.Displayed && element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public void UploadFile(string filePath)
        {
            fileInput.SendKeys(filePath);
        }

        public void CreateIssue(string category, string reproducibility, string severity, string priority, string platform, string os, string osVersion, string summary, string description, string stepsToReproduce, string additionalInfo, bool isPublic, string tagInput, string tagSelect, string path)
        {
            SelectElement(CategorySelect, category);
            SelectElement(ReproducibilitySelect, reproducibility);
            SelectElement(SeveritySelect, severity);
            SelectElement(PrioritySelect, priority);
            ClickElement(OpenProfileButton);
            TypeElement(PlatformInput, platform);
            TypeElement(OSInput, os);
            TypeElement(OSVersionInput, osVersion);
            TypeElement(SummaryInput, summary);
            TypeElement(DescriptionInput, description);
            TypeElement(StepsToReproduceInput, stepsToReproduce);
            TypeElement(AdditionalInformationInput, additionalInfo);
            TypeElement(TagInput, tagInput);
            SelectElement(TagSelect, tagSelect);

            if (isPublic)
            {
                ClickElement(PublicRadio);
            }
            else
            {
                ClickElement(PrivateRadio);
            }

            UploadFile(path);

            ClickElement(CreateIssueButton);
        }

        public void CreateNoteIssue(string note)
        {
            TypeElement(NoteText, note);
            ClickElement(CreateNote);
        }

        public void CreateNoteWithImageIssue(string path)
        {
            UploadFile(path);
            ClickElement(CreateNote);
        }

        // Asserts

        public void MessageValidate(IWebElement element, string expectedMessage)
        {
            string actualMessage = element.Text;
            Assert.IsTrue(actualMessage.Contains(expectedMessage), $"A mensagem atual '{actualMessage}' não contém a mensagem esperada '{expectedMessage}'.");
        }

        public void ToolTipValidate(IWebElement element, string expectedMessage)
        {
            string ErroMessage = element.GetAttribute("validationMessage");
            Console.WriteLine(ErroMessage);
            Assert.AreEqual(expectedMessage, ErroMessage, $"A mensagem atual '{ErroMessage}' não é igual a mensagem esperada '{expectedMessage}'.");
        }

        public void SumarayValidate(string expectedMessage)
        {
            string actualMessage = SumaryIssue.Text;
            Assert.IsTrue(actualMessage.Contains(expectedMessage), $"A mensagem atual '{actualMessage}' não contém a mensagem esperada '{expectedMessage}'.");
        }

        public void HasImageInCommentsField()
        {

            var images = NoteCommentsField.FindElements(By.CssSelector("img"));
            bool imageCheck = images.Count() > 0;

            Assert.IsTrue(imageCheck);
        }
    }
}
