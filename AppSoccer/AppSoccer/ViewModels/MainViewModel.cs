using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.ViewModels
{
    public class MainViewModel
    {
        #region Atributtes

        #endregion

        #region Properties
        public LoginViewModel  Login { get; set; }
        #endregion

        #region Contructor
        public MainViewModel()
        {
            Login = new LoginViewModel();
        }
        #endregion

        #region Methods

        #endregion
    }
}
