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
using System.Collections.ObjectModel;
using Menu = PUL.GS.Models.Menu;
using PUL.GS.Models.Interfaces;

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
                //OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => isVisible; set
            {
                isVisible = value;
                //OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand { get; set; }
        public ICommand MenuChangedCommand { get; set; }
        public ICommand CategoryScrollCommand { get; set; }
        public ICommand FoodCartCommand { get; set; }

        public ICommand ScrollToCommand { get; set; }
        public double SubTotal { get; set; }
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
                //OnPropertyChanged();
            }
        }

        private readonly IUserDialogs dialogs;
        private bool isRefreshing;
        private ObservableCollection<CustomerGroup> customers;
        private ObservableCollection<object> selectedMenu;
        private bool isVisible;

        private IConfigurableScrollItem selectedItemVm;
        private Menu scrollToGroupVm;

        public IConfigurableScrollItem SelectedItemVm
        {
            get => selectedItemVm; set
            {
                selectedItemVm = value;
                OnPropertyChanged();
            }
        }

        //private int _scrollToGroupNumber;
        //private int _scrollToItemNumber;
        public ObservableCollection<ItemsGroupViewModel> ScrollableItemsGroups { get; set; }
        public Menu ScrollToGroupVm
        {
            get => scrollToGroupVm; set
            {
                scrollToGroupVm = value;
                OnPropertyChanged();
            }
        }
        //private RelayCommand _scrollToGroupCommand;

        public ObservableCollection<CustomerGroup> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        public BookMenuViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;

            this.ScrollableItemsGroups = new ObservableCollection<ItemsGroupViewModel>();

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

            //CategoryScrollCommand = new Command(async () =>
            //{
            //    if (!IsBusy)
            //    {
            //        IsBusy = true;
            //        if (CurrentGroup != null)
            //        {
            //            await Task.Delay(3000);
            //        }

            //        IsBusy = false;
            //    }
            //});

            FoodCartCommand = new Command(async () =>
            {
                if (SubTotal >= CurrentBook.TotalMinimumConsumption)
                {
                    //CurrentBook.Menus = Menus;
                    CurrentBook.SubTotal = SubTotal;
                    CurrentBook.Total = SubTotal;
                    await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentBook);
                }
                else
                {
                    await CoreMethods.DisplayAlert("Consumo mínimo", $"Aún no cubres el total del consumo mínimo. Consumo mínimo total: {string.Format("{0:C2}", CurrentBook.TotalMinimumConsumption)}", "Continuar");
                }
            });

            //RefreshCommand = new Command(async () =>
            //{
            //    IsRefreshing = true;
            //    await Task.Delay(3000);
            //    RefreshItems();
            //    IsRefreshing = false;
            //});


            ScrollToCommand = new Command((category) =>
            {
                ExcuteScrollToGroup((string)category, 1);
            });

            IsVisible = false;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;

            this.InitWithGroup();
            //Menus = CurrentBook.Menus;
            //RefreshItems();
            Menus = new List<Menu>();
            SelectedMenu = new ObservableCollection<object>();
        }

        //public override void ReverseInit(object returnedData)
        //{
        //    base.ReverseInit(returnedData);
        //    if (returnedData != null)
        //    {

        //        CurrentMenu = returnedData as Menu;
        //        var last = (Menu)SelectedMenu.LastOrDefault();

        //        if (last != null)
        //            last.Quantity = CurrentMenu.Quantity;

        //        var Items = new List<Menu>();
        //        foreach (var obj in SelectedMenu)
        //        {
        //            var item = (Menu)obj;
        //            if (Items.Count == 0)
        //                Items.Add(item);
        //            else
        //            {
        //                var update = Items.Where(x => x.id == item.id).SingleOrDefault();
        //                if (update != null)
        //                {
        //                    update.Quantity = item.Quantity;
        //                    if (update.Quantity == 0)
        //                        Items.Remove(update);
        //                }
        //                else
        //                {
        //                    Items.Add(item);
        //                }
        //            }
        //        }
        //        Menus = Items;

        //    }
        //    RefreshItems();
        //}

        //private void RefreshItems()
        //{
        //    var menus = menuAgent.GetListFoods(CurrentBook.Establishment.id).objectResult;

        //    var grouped =
        //        from c in menus
        //        orderby c.Category
        //        group c by c.Category
        //        into groups
        //        select
        //            new CustomerGroup(groups.Key, groups.ToList());

        //    int index = 0;
        //    double account = 0;
        //    Items = 0;
        //    foreach (var group in grouped)
        //    {
        //        foreach (var item in group)
        //        {
        //            double accountItem = 0;
        //            item.Index = index;
        //            item.Quantity = 0;

        //            if (Menus != null)
        //            {
        //                item.Quantity = Menus.Where(x => x.id == item.id).Select(x => x.Quantity).LastOrDefault();
        //                accountItem = item.Quantity * item.Price;
        //                account += accountItem;
        //                SubTotal = account;
        //                Items += item.Quantity;

        //            }
        //            index++;
        //        }

        //        if (Items > 0)
        //            IsVisible = true;
        //        else
        //            IsVisible = false;
        //    }
        //    Customers =
        //        new ObservableCollection<CustomerGroup>(grouped);

        //}

        public void InitWithGroup()
        {

            var foods = menuAgent.GetListFoods(CurrentBook.Establishment.id).objectResult;

            var grouped =
                from c in foods
                orderby c.Category
                group c by c.Category
                into groups
                select
                    new ItemsGroupViewModel(groups.Key, groups.ToList());


            //this.ScrollableItemsGroups.Add(grouped);

            ScrollableItemsGroups = new ObservableCollection<ItemsGroupViewModel>(grouped);
            //for (int gi = 0; gi <= 5; gi++)
            //{
            //    var groupItems = new List<ItemWithGroupViewModel>();
            //    for (int i = 0; i <= 10; i++)
            //    {
            //        var vm = new ItemWithGroupViewModel() { Config = new Controls.ScrollToConfiguration(), Number = i, Text = $"Item {i}" };
            //        groupItems.Add(vm);
            //    }

            //    var giVm = new ItemsGroupViewModel($"Group {gi}", groupItems);
            //    this.ScrollableItemsGroups.Add(giVm);
            //}
        }

        private void ExcuteScrollToGroup(string category, int index)
        {
            var groupsVm = this.ScrollableItemsGroups.SingleOrDefault(vm => vm.GroupName.Contains(category) && vm.Any(vmi => vmi.Index == index));
            var itemInGroup = groupsVm.SingleOrDefault(vm => vm.Index == index);

            this.ScrollToGroupVm = itemInGroup;
            this.SelectedItemVm = itemInGroup;

            CanExecuteScrollToGroup(category, index);
        }

        private bool CanExecuteScrollToGroup(string category, int index)
        {
            return this.ScrollableItemsGroups.Any(vm => vm.GroupName.Contains(category) && vm.Any(vmi => vmi.Index == index));
        }
    }
}
