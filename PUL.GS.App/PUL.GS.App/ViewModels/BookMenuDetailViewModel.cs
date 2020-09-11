using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PUL.GS.Models;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class BookMenuDetailViewModel : BaseViewModel
    {
        public ICommand CloseMenuDetailCommand { get; set; }
        public ICommand AddFoodCartCommand { get; set; }
        public Book CurrentBook { get; set; }
        public List<Menu> Menus { get; set; }
        public Menu CurrentMenu { get; set; }
        public BookMenuDetailViewModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentMenu = initData as Menu;

            CloseMenuDetailCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    await CoreMethods.PopPageModel(false);
                    IsBusy = false;
                }
            });

            AddFoodCartCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    await CoreMethods.PopPageModel(CurrentMenu, false);
                    IsBusy = false;
                }
            });
        }
    }
}