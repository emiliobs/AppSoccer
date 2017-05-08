using AppSoccer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSoccer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectMachPage : ContentPage
    {
        public SelectMachPage()
        {
            InitializeComponent();

            //aqui consumo el singleton
            var selectMatchViewModel = SelectMachViewModel.GetInstance();

            //aqui hago el refres de forma local en la page
            base.Appearing += (object sender, EventArgs e) => 
            {
                selectMatchViewModel.RefreshCommand.Execute(this);
            };
        }

        
    }
}
