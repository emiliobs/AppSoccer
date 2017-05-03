using AppSoccer.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Service
{
    public class NavigationService
    {
        #region Methods

        public void SetMainPage(string pageName)
        {
          switch(pageName)
            {
                case "MasterPage":
                    App.Current.MainPage = new MasterPage();
                    break;
                case "LoginPage":
                    App.Current.MainPage = new LoginPage();
                    break;
            }
        }

        #endregion
    }
}
