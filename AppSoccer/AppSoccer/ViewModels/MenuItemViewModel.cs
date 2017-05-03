using AppSoccer.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppSoccer.ViewModels
{
    public class MenuItemViewModel
    {
        #region Atributtes

        private NavigationService navigationService;

        #endregion

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Constructor
        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion


        #region Commands
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private void Navigate()
        {
            if (PageName == "LoginPage")
            {
                navigationService.SetMainPage("LoginPage");
            }
        }
        #endregion
    }
}
