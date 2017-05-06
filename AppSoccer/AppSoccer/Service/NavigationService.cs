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

        public async Task Navigate(string pageName)
        {
            //esto lo hago para ocultar la master detail:
            App.Master.IsPresented = false;

            switch(pageName)
            {
                case "SelectTournamentPage":
                    await App.Navigator.PushAsync(new SelectTournamentPage());
                    break;
                case "SelectMachPage":
                    await App.Navigator.PushAsync(new SelectMachPage());
                    break;
                case "EditPredictionPage":
                    await App.Navigator.PushAsync(new EditPredictionPage());
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
