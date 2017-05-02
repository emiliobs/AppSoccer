using AppSoccer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Infrastructure
{
    public class IntanceLocator
    {
        public MainViewModel Main { get; set; }


        public IntanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
