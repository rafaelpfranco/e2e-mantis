using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EMantis.Interfaces
{
    internal interface INavBarPage
    {
        void ClickMyVisionMenuItem();
        void ClickViewIssuesMenuItem();
        void ClickReportIssuesMenuItem();
        void ClickChangeRegisterMenuItem();
        void ClickPlaningMenuItem();
        void LogoutUser();
    }
}
