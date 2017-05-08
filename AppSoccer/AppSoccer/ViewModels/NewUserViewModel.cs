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
    public class NewUserViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private DataService dataService;     
        private bool isRunning;
        private bool isEnabled;    
        private List<League> leagues;
        private int favoriteLeagueId;

        #endregion

        #region Properties

        public ObservableCollection<LeagueItemViewModel> Leagues { get; set; }
        public ObservableCollection<TeamItemViewModel> Teams { get; set; }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    RaiseOnPropertyChange();
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    RaiseOnPropertyChange();
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public int FavoriteLeagueId
        {
            set
            {
                if (favoriteLeagueId != value)
                {
                    favoriteLeagueId = value;

                    LoadTeams(favoriteLeagueId);
                    RaiseOnPropertyChange();
                }
            }
            get
            {
                return favoriteLeagueId;
            }
        }

       
        #endregion

        #region Constructor
        public NewUserViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            Leagues = new ObservableCollection<LeagueItemViewModel>();
            Teams = new ObservableCollection<TeamItemViewModel>();

            IsEnabled = true;

            LoadLegues();
        }

        #endregion

        #region Commads

        #endregion

        #region Methods
        private async void LoadLegues()
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

            IsRunning = true;
            IsEnabled = false;
            var parameter = dataService.First<Parameter>(false);
            var response = await apiService.Get<League>(parameter.URLBase, "/api", "/Leagues");

            IsRunning = false;
            IsEnabled =true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            //aqui lo tengo en memoria
            leagues = (List<League>)response.Result;
            ReloadLeagues(leagues);
        }

        private void ReloadLeagues(List<League> leagues)
        {
            Leagues.Clear();

            foreach (var league in leagues.OrderBy(l => l.Name))
            {
                Leagues.Add(new LeagueItemViewModel
                {
                  LeagueId = league.LeagueId,
                  Logo = league.Logo,
                  Name= league.Name,
                  Teams = league.Teams,
                });
            }
        }

        private void LoadTeams(int favoriteLeagueId)
        {
            Teams.Clear();

            var teams = leagues.Where(l => l.LeagueId == favoriteLeagueId).FirstOrDefault().Teams;

            foreach (var team in teams.OrderBy(t => t.Name))
            {
                Teams.Add(new TeamItemViewModel
                {
                    Fans = team.Fans,
                    Logo = team.Logo,
                    LeagueId = team.LeagueId,
                    Initials = team.Initials,
                    Name = team.Name,
                    TeamId = team.TeamId,
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
