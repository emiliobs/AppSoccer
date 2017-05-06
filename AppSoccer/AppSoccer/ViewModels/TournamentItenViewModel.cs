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
   public class TournamentItenViewModel :Tournament
    {
        #region Attributes
        private NavigationService navigateService;
        #endregion

        #region Properties

        #endregion


        #region Contructor
        public TournamentItenViewModel()
        {
            navigateService = new NavigationService();
        }        
        #endregion

        #region Command
        public ICommand SelectTournamentCommand { get { return new RelayCommand(SelectTournament); } }

        private async void SelectTournament()
        {
            //AQui le paso el torneo seleccionado a la pagina seleccionado de la match(de contructo origen a contructor destino):
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SelectMatch = new SelectMachViewModel(TournamentId);
           await navigateService.Navigate("SelectMachPage");
        }
        #endregion
    }
}
