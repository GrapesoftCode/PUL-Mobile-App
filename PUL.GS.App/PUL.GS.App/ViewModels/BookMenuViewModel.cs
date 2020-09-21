using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PUL.GS.App.Dependencies;
using PUL.GS.App.Models;
using System.Threading.Tasks;

namespace PUL.GS.App.ViewModels
{
    public class BookMenuViewModel : BaseViewModel
    {
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand { get; set; }
        public ICommand MenuChangedCommand { get; set; }
        public ICommand CategoryScrollCommand { get; set; }
        public ICommand FoodCartCommand { get; set; }
        public double SubTotal { get; set; } = 0;
        public int Items { get; set; }
        public Book CurrentBook { get; set; }
        public Menu CurrentMenu { get; set; }
        public CustomerGroup CurrentGroup { get; set; }
        public List<Menu> Menus { get; set; }
        public ObservableCollection<object> SelectedMenu
        {
            get => selectedMenu; 
            set
            {
                selectedMenu = value;
                OnPropertyChanged();
            }
        }

        private readonly IUserDialogs dialogs;
        private bool isRefreshing;
        private ObservableCollection<CustomerGroup> customers;
        private ObservableCollection<object> selectedMenu;

        public ObservableCollection<CustomerGroup> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }
        public BookMenuViewModel() { }

        public BookMenuViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;

            MenuChangedCommand = new Command(async (p) =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    if (!IsInitialized)
                    {
                        await CoreMethods.PushPageModel<BookMenuDetailViewModel>((Menu)SelectedMenu.LastOrDefault());
                        IsInitialized = false;

                        IsBusy = false;
                    }
                }
            });

            CategoryScrollCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    if (CurrentGroup != null)
                    {
                        await Task.Delay(3000);
                    }

                    IsBusy = false;
                }
            });

            FoodCartCommand = new Command(async () => {
                if (SubTotal >= CurrentBook.TotalMinimumConsumption)
                {
                    CurrentBook.Menus = Menus;
                    CurrentBook.SubTotal = SubTotal;
                    CurrentBook.Total = SubTotal;
                    await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentBook);
                }
                else
                {
                    await CoreMethods.DisplayAlert("Consumo mínimo", $"Aún no cubres el total del consumo mínimo. Consumo Mínimo Total: {string.Format("{0:C2}", CurrentBook.TotalMinimumConsumption)}", "Continuar");
                }
            });

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await Task.Delay(3000);
                RefreshItems();
                IsRefreshing = false;
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;
            //Menus = CurrentBook.Menus;
            RefreshItems();
            Menus = new List<Menu>();
            SelectedMenu = new ObservableCollection<object>();
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData != null)
            {

                CurrentMenu = returnedData as Menu;
                var last = (Menu)SelectedMenu.LastOrDefault();

                if (last != null)
                    last.Quantity = CurrentMenu.Quantity;

                var Items = new List<Menu>();
                foreach (var obj in SelectedMenu)
                {
                    var item = (Menu)obj;
                    if (Items.Count == 0)
                        Items.Add(item);
                    else
                    {
                        var update = Items.Where(x => x.id == item.id).SingleOrDefault();
                        if (update != null)
                        {
                            update.Quantity = item.Quantity;
                            if (update.Quantity == 0)
                                Items.Remove(update);
                        }
                        else
                        {
                            Items.Add(item);
                        }
                    }
                }
                Menus = Items;

            }
            RefreshItems();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }


        private void RefreshItems()
        {
            var menus = menuAgent.GetListFoods(CurrentBook.Establishment.id).objectResult;

            var grouped =
                from c in menus
                orderby c.Category
                group c by c.Category
                into groups
                select
                    new CustomerGroup(groups.Key, groups.ToList());

            int index = 0;
            double account = 0;
            Items = 0;
            foreach (var group in grouped)
            {
                foreach (var item in group)
                {
                    double accountItem = 0;
                    item.Index = index;
                    item.Quantity = 0;
                   
                    if (Menus != null)
                    {
                        item.Quantity = Menus.Where(x => x.id == item.id).Select(x => x.Quantity).LastOrDefault();
                        accountItem = item.Quantity * item.Price;
                        account += accountItem;
                        SubTotal = account;
                        Items += item.Quantity;

                    }
                    index++;
                }
            }
            Customers =
                new ObservableCollection<CustomerGroup>(grouped);

        }
    }
}
