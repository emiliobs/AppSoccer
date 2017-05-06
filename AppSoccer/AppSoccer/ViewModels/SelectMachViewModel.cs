using AppSoccer.Classes;
using AppSoccer.Models;
using AppSoccer.Service;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.ViewModels
{
    public class SelectMachViewModel :  INotifyPropertyChanged
    {
        #region Atributtes
        private int tournamentId;
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private DataService dataService;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<MachItemViewModel> Matches { get; set; }
        public bool IsRefreshing
        {
            get { return isRefreshing; }
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

            #region Contructor
        public SelectMachViewModel(int tournamentId)
        {
            this.tournamentId = tournamentId;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            Matches = new ObservableCollection<MachItemViewModel>();

            //
            LoadMatches();
        }

        #region Methods
        private async void LoadMatches()
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
            var controller = $"/Tournaments/GetMatchesToPredict/{tournamentId}/{user.UserId}";
            var response = await apiService.Get<Match>(parameter.URLBase, "/api", controller, user.TokenType, user.AccessToken);
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadMatch((List<Match>)response.Result);
        }

        private void ReloadMatch(List<Match> matches)
        {
            //lo lipio por si pasa la segunda vez por este hilo:
            Matches.Clear();

            foreach (var match in matches)
            {
                Matches.Add(new MachItemViewModel
                {
                  DateId   = match.DateId,
                  DateTime = match.DateTime,
                  Local    = match.Local,
                  LocalGoals = match.LocalGoals,
                  LocalId = match.LocalId,
                  MatchId = match.MatchId,
                  StatusId = match.StatusId,
                  TournamentGroupId = match.TournamentGroupId,
                  Visitor = match.Visitor,
                  VisitorGoals = match.VisitorGoals,
                  VisitorId = match.VisitorId,
                  WasPredicted = match.WasPredicted,
                  
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
        #endregion
    }
}
