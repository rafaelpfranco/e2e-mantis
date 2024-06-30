using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EMantis.Interfaces
{
    public interface ITaskPage
    {
        // Locators
        IWebElement SucessMessage { get; }
        IWebElement SummaryInput { get; }
        IWebElement CreateIssueButton { get; }
        IWebElement DescriptionInput { get; }
        IWebElement PageContent { get; }
        IWebElement CommentsField { get; }
        IWebElement CreateNote { get; }

        // Métodos adicionais
        void TypeElement(IWebElement element, string text);
        void SelectElement(IWebElement selectElement, string value);
        void UploadFile(string filePath);
        void ClickElement(IWebElement selectElement);
        void MessageValidate(IWebElement element, string message);
        void CreateIssue(string category, string reproducibility, string severity, string priority, string platform, string os, string osVersion, string summary, string description, string stepsToReproduce, string additionalInfo, bool isPublic, string tagInput, string tagSelect, string path);
        void ToolTipValidate(IWebElement element, string expectedMessage);
        void CreateNoteIssue(string note);
        void CreateNoteWithImageIssue(string path);
    }
}
