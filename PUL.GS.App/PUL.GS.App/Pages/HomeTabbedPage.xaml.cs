using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PUL.GS.App.Controls;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace PUL.GS.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : CustomTabbedPage
    {
        public HomeTabbedPage()
        {
            InitializeComponent();
            
            this.BarBackgroundColor = Color.FromHex("#ffffff");
            this.BarTextColor = Color.FromHex("#000000");
            this.UnselectedTabColor = Color.FromHex("#yyyyyy");
        }
        
    }

}