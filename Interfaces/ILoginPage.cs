using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EMantis.Interfaces
{
    internal interface ILoginPage
    {
        void Login(string username, string password);
        void EnterUsername(string username);
        void EnterPassword(string password);
        void ClickEnterButton();
        void ErrorMessageValidate(string expectedErrorMessage);
        void UrlValidate(string expectedUrl);
        void UserNameValidate(string expectedUserName);
        void HeaderValidate();
    }
}
