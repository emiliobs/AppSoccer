using AppSoccer.Classes;
using AppSoccer.Models;
using AppSoccer.Service;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppSoccer.ViewModels
{
    public class EditPredictionViewModel   : Match , INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private DataService dataService;
        private Match match;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                    //RaiseOnPropertyChange(); 
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                    //RaiseOnPropertyChange();
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public string GoalsLocal { get; set; }
        public string GoalsVisitor { get; set; }

        #endregion

        #region Constructor
        public EditPredictionViewModel(Match match)
        {
            this.match = match;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            

            DateId = match.DateId;
            DateTime = match.DateTime;
            Local = match.Local;
            LocalGoals = match.LocalGoals;
            LocalId = match.LocalId;
            MatchId = match.MatchId;
            StatusId = match.StatusId;
            TournamentGroupId = match.TournamentGroupId;
            Visitor = match.Visitor;
            VisitorGoals = match.VisitorGoals;
            VisitorId = match.VisitorId;
            WasPredicted = match.WasPredicted;

            //truco para arreglar el problema con los binding de enteros:
            GoalsLocal   = LocalGoals2.ToString();
            GoalsVisitor = VisitorGoals2.ToString();

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private async void Save()
        {
            if (string.IsNullOrEmpty(GoalsLocal))
            {
                await dialogService.ShowMessage("Error", "You must enter a valid local goals.");
                return;
            }

            if (string.IsNullOrEmpty(GoalsVisitor))
            {
                await dialogService.ShowMessage("Error", "You must enter a valid visitor goals.");
                return;
            }

            //Aqui valido la conexión con internet:
            if (!CrossConnectivity.Current.IsConnected)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Check your internet connection");
                return;
            }

            //Aqui cheqieo si tengo datos, por que puedo tener servicio sin datos(reachable = accesibilidad):
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Check Your internet Connection.");
            }

            //aqui ya consumo el api:
            IsRunning = true;
            IsEnabled = false;

            var parameters = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);

            var prediction = new Prediction
            {
                LocalGoals = int.Parse(GoalsLocal),
                MachId = MatchId,
                Points = 0,
                UserId = user.UserId,
                VisitorGoals = int.Parse(GoalsVisitor),


            };

            var response = await apiService.Post(parameters.URLBase, "/api", "/Predictions", user.TokenType, user.AccessToken, prediction);

            IsRunning = false;
            IsEnabled = true;

            //aqui pegunto si en envio fue exitoso:
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", "An Error has occurred saving the prediction, try again latter.");
                return;
            }

            //aqui lo regres a la página de predicion para que siga haciendo predicciones:
            await navigationService.Back();

        }
        #endregion

        #region Methods

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
