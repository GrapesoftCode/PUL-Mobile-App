using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PUL.GS.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageMasterViewModel
        {
            public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
                {
                    new MainPageMasterMenuItem { Id = 0, Title = "Perfil", Icon="e.png",  TargetType = typeof(ProfilePage) },
                    new MainPageMasterMenuItem { Id = 1, Title = "Salas", Icon="b.png", TargetType= typeof(RoomsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "Settings", Icon="c.png",  TargetType = typeof(SettingsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "About", Icon="d.png",  TargetType = typeof(AboutPage) },
                });
            }

            #region INotifyPropertyChanged Implementation
            //public event PropertyChangedEventHandler PropertyChanged;
            //void OnPropertyChanged([CallerMemberName] string propertyName = "")
            //{
            //    if (PropertyChanged == null)
            //        return;

            //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //}
            #endregion
        }
    }
}