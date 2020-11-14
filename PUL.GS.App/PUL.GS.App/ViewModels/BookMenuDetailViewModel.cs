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
        public Menu CurrentFood { get; set; }
        public BookMenuDetailViewModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentFood = initData as Menu;

            CloseMenuDetailCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    CurrentFood.Quantity = CurrentFood.Quantity;
                    await CoreMethods.PopPageModel(CurrentFood);
                    IsBusy = false;
                }
            });

            AddFoodCartCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    if (!IsInitialized)
                    {
                        await CoreMethods.PopPageModel(CurrentFood, false);
                        IsInitialized = false;
                    }
                    IsBusy = false;
                }
            });
        }
    }
}