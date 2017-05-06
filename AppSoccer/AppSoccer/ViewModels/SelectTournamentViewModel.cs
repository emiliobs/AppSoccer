using AppSoccer.Classes;
using AppSoccer.Models;
using AppSoccer.Service;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppSoccer.ViewModels
{
    public class SelectTournamentViewModel : INotifyPropertyChanged
    {

        #region Atributtes        
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private DataService dataService;
        private bool isRefreshing;

        #endregion

        #region Properties
        public ObservableCollection<TournamentItenViewModel> Tournaments { get; set; }
        public bool IsRefreshing
        {
            get {return isRefreshing; } 
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                   RaiseOnPropertyChange();
                }
            }
}
        #endregion

        #region Constructor
        public SelectTournamentViewModel()
        {
            apiService        = new ApiService();
            dialogService     = new DialogService();
            navigationService = new NavigationService();
            dataService       = new DataService();

            Tournaments = new ObservableCollection<TournamentItenViewModel>();

            //Aqui cargo la lsita de tournament:
            LoadTournament();
        }

        #endregion

        #region Command
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            LoadTournament();
        }
        #endregion

        #region Methods
        private async void LoadTournament()
        {
            //aqui verifico there is connection a internet:
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check your internet conenction.");
               
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                await dialogService.ShowMessage("Error", "Check your internet conenction.");
                return;

            }

            IsRefreshing = true;
            var parameter = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);
            var response = await apiService.Get<Tournament>(parameter.URLBase,"/api", "/Tournaments", user.TokenType, user.AccessToken);
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadTournaments((List<Tournament>)response.Result);
        }

        private void ReloadTournaments(List<Tournament> tournaments)
        {
            //proeperty observableColection:
            Tournaments.Clear();

            foreach (var tournament in tournaments)
            {
                Tournaments.Add(new TournamentItenViewModel
                {
                     Dates = tournament.Dates,
                     Groups = tournament.Groups,
                     Logo = tournament.Logo,
                     Name = tournament.Name,
                     TournamentId = tournament.TournamentId,
                });
            }


        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseOnPropertyChange([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
