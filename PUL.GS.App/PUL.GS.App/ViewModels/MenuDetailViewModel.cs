using Android.Views;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class MenuDetailViewModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }
        public ICommand MenuCommand { get; set; }
        public Menu CurrentMenu { get; set; } = new Menu();
        public ObservableCollection<Menu> Menus { get; set; }
        public string CurrentName { get; set; }
        public string CurrentEstablismentId { get; set; }

        private readonly MenuData menuAgent;

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        public MenuDetailViewModel(string tabName, string establishmentId)
        {
            menuAgent = new MenuData(appSettings);
            CurrentName = tabName;
            CurrentEstablismentId = establishmentId;

            var listFoodsByCategory = menuAgent.GetListFoodsByCategory(CurrentEstablismentId, CurrentName).objectResult;
            Menus = new ObservableCollection<Menu>(listFoodsByCategory);
        }
    }
}
