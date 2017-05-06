using AppSoccer.Models;
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
    public class MachItemViewModel : Match
    {
        #region Attributes
        private NavigationService navigationService;
        #endregion
        #region Properties

        #endregion

        #region Constructor
        public MachItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand SelectMatchCommand { get { return new RelayCommand(SelectMatch); }  }

        private async void SelectMatch()
        {
            var mainViewModel = MainViewModel.GetInstance();
            //con el this paso toda lainstacion de la clase:
            mainViewModel.EditPrediction = new EditPredictionViewModel(this);
            await navigationService.Navigate("EditPredictionPage");  
        }
        #endregion
    }
}
