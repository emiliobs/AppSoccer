using AppSoccer.Classes;
using AppSoccer.Models;
using AppSoccer.Pages;
using AppSoccer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppSoccer
{
    public partial class App : Application
    {
        #region Attributtes
        private DataService dataService;
        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        #endregion

        #region Contructor
        public App()
        {
            InitializeComponent();
                                                                                        

            dataService = new DataService();
            LoadParameter();

            //MainPage = new LoginPage();
            //aqui busci usuario
            var user = dataService.First<User>(false);
            if (user != null && user.IsRemembered && user.TokenExpires > DateTime.Now)
            {
                var favotiteTeam = dataService.Find<Team>(user.FavoriteTeamId, false);
                user.FavoriteTeam = favotiteTeam;
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.CurrentUser = user;
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();
            }


        }


        #endregion

        #region Methods

        private void LoadParameter()
        {
            var urlsBase = Application.Current.Resources["URLBase"].ToString();
            var parameter = dataService.First<Parameter>(false);

            if (parameter == null)
            {
                parameter = new Parameter
                {
                   URLBase = urlsBase,
                };

                dataService.Insert(parameter);
            }
            else
            {
                parameter.URLBase = urlsBase;
                dataService.Update(parameter);
            }



        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
