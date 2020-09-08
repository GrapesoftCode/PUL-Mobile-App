using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        private List<Object> selectedMenu;


        public ICommand MenuCommand { get; set; }
        public ICommand MenuChangedCommand { get; set; }
        public Menu CurrentMenu { get; set; } = new Menu();

        public List<Object> SelectedMenu
        {
            get => selectedMenu; 
            set
            {
                selectedMenu = value;
            }
        }

        public string Name { set; get; }
        public string _TabName { get; set; }
        public string _EstablismentId { get; set; }

        public MenuDetailViewModel MenuDetailViewModel { set; get; }

        public TabViewModel(string tabName, string establishmentId)
        {
            _TabName = tabName;
            _EstablismentId = establishmentId;
            MenuDetailViewModel = new MenuDetailViewModel(_TabName, _EstablismentId);

            SelectedMenu = new List<object>();

            MenuCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentMenu != null)
                    {
                        //CurrentBook.Establishment = CurrentEstablishment;
                        //CurrentBook.User = CurrentUser;
                        //dialogs.ShowLoading("Conectando");
                        await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentMenu);
                        //dialogs.HideLoading();
                        CurrentMenu = null;
                    }

                    IsBusy = false;
                }
            });

            MenuChangedCommand = new Command(async () => 
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentMenu != null)
                    {
                        //CurrentBook.Establishment = CurrentEstablishment;
                        //CurrentBook.User = CurrentUser;
                        //dialogs.ShowLoading("Conectando");
                        var menuList = selectedMenu;
                        //await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentMenu);
                        //dialogs.HideLoading();
                        CurrentMenu = CurrentMenu;
                    }

                    IsBusy = false;
                }
            });
        }

        //public ICommand MenuCommand => new Command(async () => {
        //    {
        //        if (!IsBusy)
        //        {
        //            IsBusy = true;

        //            if (CurrentMenu != null)
        //            {
        //                //currentbook.establishment = currentestablishment;
        //                //currentbook.user = currentuser;
        //                //dialogs.showloading("conectando");
        //                await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentMenu);
        //                //dialogs.hideloading();
        //                CurrentMenu = null;
        //            }

        //            IsBusy = false;
        //        }
        //    }
        //});

    }
}
