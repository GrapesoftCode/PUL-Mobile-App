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
        public double Total { get; set; } = 0;
        public Book CurrentBook { get; set; }
        public Menu CurrentMenu { get; set; }
        public CustomerGroup CurrentGroup { get; set; }
        public ObservableCollection<Menu> Menus { get; set; }
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

                    //double account = 0;
                    //foreach (var obj in menuList)
                    //{
                    //    double accountPerItem = 0;
                    //    var item = (Menu)obj;
                    //    if (item.Quantity == 0)
                    //        item.Quantity = 1;
                    //    accountPerItem = item.Price * item.Quantity;
                    //    account += accountPerItem;
                    //}

                    //await CoreMethods.PushNewNavigationServiceModal(basiNavContainer, bookMenuDetail.GetModel());

                    if (!IsInitialized)
                    {
                        //Menus = new List<Menu>();
                        //Menus.Add((Menu)SelectedMenu.LastOrDefault());
                        //CurrentBook.Menus = Menus;
                        //await CoreMethods.PushPageModel<BookMenuDetailViewModel>(CurrentBook);
                        //Menus.Add();

                        Menus.Add((Menu)SelectedMenu.LastOrDefault());
                        
                        await CoreMethods.PushPageModel<BookMenuDetailViewModel>((Menu)SelectedMenu.LastOrDefault());
                        IsInitialized = false;
                    }
                    //if (account >= currentBook.SubTotal)
                    //{
                    //    //CurrentBook.SubTotal = account;
                    //    //CurrentBook.Total = 0;
                    //    //CurrentBook.PerPerson = CurrentBook.Table.MinimumConsumption;
                    //    //var menus = new List<Menu>();
                    //    //foreach (var obj in menuList)
                    //    //{
                    //    //    var item = (Menu)obj;
                    //    //    menus.Add(item);
                    //    //}
                    //    //CurrentBook.Menus = menus;
                    //    //await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentBook);


                    //}

                    IsBusy = false;
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
            Menus = CurrentBook.Menus;
            RefreshItems();
            Menus = new ObservableCollection<Menu>();
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
            }
            RefreshItems();
            
            //SelectedMenu = new ObservableCollection<object>();
        }


        private void RefreshItems()
        {
            var grouped = menuAgent.GetListFoods(CurrentBook.Establishment.id).objectResult;

            //var grouped =
            //    from c in costumers
            //    orderby c.Category
            //    group c by c.Category
            //    into groups
            //    select
            //        new CustomerGroup(groups.Key, groups.ToList());

            int index = 0;
            double account = 0;
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
                        Total = account;
                    }
                    index++;
                }
            }

            Customers =
                new ObservableCollection<CustomerGroup>(grouped);

        }
    }
}
