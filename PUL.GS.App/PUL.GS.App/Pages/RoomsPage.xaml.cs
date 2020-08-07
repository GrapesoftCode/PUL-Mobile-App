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
    public partial class RoomsPage : ContentPage
    {
        public RoomsPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}