using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EMantis.Interfaces
{
    public interface IViewIssuesPage
    {
        void SearchElement(string searchText);
        void FindElementInTable(string searchText);
        void OpenIssue();
    }
}
