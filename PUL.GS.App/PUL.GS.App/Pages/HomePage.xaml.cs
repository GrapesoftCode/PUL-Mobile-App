using PUL.GS.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PUL.GS.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            //BindingContext = new HomeViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}