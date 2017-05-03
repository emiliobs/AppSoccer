using AppSoccer.Models;
using AppSoccer.Service;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppSoccer.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Atributtes        
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        private bool isRemembered;
        #endregion

        #region Properties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
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
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public bool IsRemembered
        {
            set
            {
                if (isRemembered != value)
                {
                    isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRemembered"));
                }
            }
            get
            {
                return isRemembered;
            }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            apiService    = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            IsRemembered = true;

        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            //Validaciones de Campos:
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "You must enter the User Email.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter the User Password.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

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
                await dialogService.ShowMessage("Error","Check Your internet Connection.");
            }

            var token = await apiService.GetToken("http://soccerapi.azurewebsites.net",Email,Password);

            if (token == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error","The user Name or Password ");
                Password = null;
                return;
            }

            //aqui pruebo si no me llega token, aqui muestro el error de mapeo del token tanto arriba como abajo:
            if (string.IsNullOrEmpty(token.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", token.ErrorDescription);
                Password = null;
                return;

            }

            //Aqui ya le pregunto con el usurioname o email, para que me retorne el usuario autorizado:
            var response = await apiService.GetUserByEmail("http://soccerapi.azurewebsites.net", "/api", "/Users/GetUserByEmail",
                token.TokenType, token.AccessToken, token.UserName);


            //aqui valido si el usuario existe(es casi emplosible que el usuario no exista por el token..
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try again latter.");
                return;
            }


            //limpios campos:
            Email = null;
            Password = null;

            //Si llego hasta aui todo esta bien..
            IsRunning = false;
            IsEnabled = true;

            var user = (User)response.Result;
            //Prueba que todo esta bien:
            //await dialogService.ShowMessage("All equals OK...", $"Welcome: {user.FirstName} {user.LastName}, Alias {user.NickName}");

            //aqui utilizo el singleton con sus mienbros:
            var mainViewModel = MainViewModel.GetInstance();
            //aqui le llevos los valores del servicio a los valores mapeados en las clases locales:
            mainViewModel.CurrentUser = user;
            //aqui ya con los datos verificados en la nube, navega al amasterPage:
            navigationService.SetMainPage("MasterPage");

        }

    
        #endregion

        #region Methods

        #endregion    

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion
    }
}
