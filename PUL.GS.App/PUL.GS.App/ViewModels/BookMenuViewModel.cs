using Acr.UserDialogs;
using PUL.GS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Menu = PUL.GS.Models.Menu;
using PUL.GS.Models.Interfaces;
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

        public ICommand MenuChangedCommand => new Command(async () =>
            {
            if (!IsBusy)
            {
                IsBusy = true;
                if (!IsInitialized)
                {
                    if (CurrentFood != null)
                    {
                            //Menus = new List<Menu>();
                            var exists = Menus.Where(x => x.id == CurrentFood.id).SingleOrDefault();
                            if (exists == null)
                                Menus.Add(CurrentFood);
                            await CoreMethods.PushPageModel<BookMenuDetailViewModel>(CurrentFood);
                            CurrentFood = null;
                    }

                    IsInitialized = false;
                }
                IsBusy = false;
            }
        });

        public ICommand FoodCartCommand => new Command(async () =>
        {
            if (SubTotal >= CurrentBook.TotalMinimumConsumption)
            {
                CurrentBook.Menus = Menus;
                CurrentBook.SubTotal = SubTotal;
                CurrentBook.Total = SubTotal;
                await CoreMethods.PushPageModel<BookDetailViewModel>(CurrentBook);
            }
            else
            {
                await CoreMethods.DisplayAlert("Consumo mínimo", $"Aún no cubres el total del consumo mínimo. Consumo mínimo total: {string.Format("{0:C2}", CurrentBook.TotalMinimumConsumption)}", "Continuar");
            }
        });

        public ICommand ScrollToCommand { get; set; }
        public double SubTotal { get; set; }
        public int Items { get; set; }
        public Book CurrentBook { get; set; }
        public ItemsGroupViewModel CurrentGroup { get; set; }
        public List<Menu> Menus { get; set; }

        private readonly IUserDialogs dialogs;
        private bool isRefreshing;
        private bool isVisible;

        private IConfigurableScrollItem selectedItemVm;
        private Menu currentFood;

        public IConfigurableScrollItem SelectedItemVm
        {
            get => selectedItemVm; set
            {
                selectedItemVm = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ItemsGroupViewModel> Foods { get; set; }
        public Menu CurrentFood
        {
            get => currentFood; set
            {
                currentFood = value;
                OnPropertyChanged();
            }
        }

        public BookMenuViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
                       

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await Task.Delay(3000);
                await RefreshItems();
                IsRefreshing = false;
            });


            ScrollToCommand = new Command(() =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    if (CurrentGroup != null)
                    {
                        ExcuteScrollToGroup((string)CurrentGroup.GroupName, 1);
                        CurrentGroup = null;
                    }
                    IsBusy = false;
                }
            });

            IsVisible = false;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;
            await RefreshItems();
            Menus = new List<Menu>();
        }

        public override async void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData != null)
            {

                var food = returnedData as Menu;
                var last = Menus.Where(x => x.id == food.id).SingleOrDefault();

                if (last != null) {
                    if (food.Quantity > 0)
                        last.Quantity = food.Quantity;
                    else
                        Menus.Remove(last);
                }

                //var Items = new List<Menu>();
                //foreach (var item in Menus)
                //{
                //    if (Items.Count == 0)
                //        Items.Add(item);
                //    else
                //    {
                //        var update = Items.Where(x => x.id == item.id).SingleOrDefault();
                //        if (update != null)
                //        {
                //            update.Quantity = item.Quantity;
                //            if (update.Quantity == 0)
                //                Items.Remove(update);
                //        }
                //        else
                //        {
                //            Items.Add(item);
                //        }
                //    }
                //}
                //Menus = Items;

            }
            await RefreshItems();
        }

        private async Task RefreshItems()
        {
            dialogs.ShowLoading("Recalculando...");

            var foods = await menuAgent.GetListFoods(CurrentBook.Establishment.id);
            var listFoods = foods.objectResult;

            Items = 0;
            double account = 0;
            foreach (var food in listFoods)
            {
                if (Menus != null)
                {
                    double accountItem = 0;
                    var update = Menus.Where(x => x.id == food.id).SingleOrDefault();
                    if (update != null)
                    {
                        food.Quantity = update.Quantity;
                        accountItem = food.Quantity * food.Price;
                        account += accountItem;
                        Items += food.Quantity;
                    }

                }
            }
            SubTotal = account;

            if (Items > 0)
                IsVisible = true;
            else
                IsVisible = false;


            //int index = 0;
            //double account = 0;
            //Items = 0;
            //foreach (var group in grouped)
            //{
            //    foreach (var item in group)
            //    {
            //        double accountItem = 0;
            //        item.Index = index;
            //        item.Quantity = 0;

            //        if (Menus != null)
            //        {
            //            item.Quantity = Menus.Where(x => x.id == item.id).Select(x => x.Quantity).SingleOrDefault();
            //            accountItem = item.Quantity * item.Price;
            //            account += accountItem;
            //            SubTotal = account;
            //            Items += item.Quantity;

            //        }
            //        index++;
            //    }

            //    if (Items > 0)
            //        IsVisible = true;
            //    else
            //        IsVisible = false;
            //}


            var grouped =
               from c in listFoods
               orderby c.Category
               group c by c.Category
               into groups
               select
                   new ItemsGroupViewModel(groups.Key, groups.ToList());

            Foods = new ObservableCollection<ItemsGroupViewModel>(grouped);

            dialogs.HideLoading();
        }

        private void ExcuteScrollToGroup(string category, int index)
        {
            var groupsVm = this.Foods.SingleOrDefault(vm => vm.GroupName.Contains(category) && vm.Any(vmi => vmi.Index == index));
            var itemInGroup = groupsVm.SingleOrDefault(vm => vm.Index == index);

            //this.CurrentFood = itemInGroup;
            this.SelectedItemVm = itemInGroup;

            CanExecuteScrollToGroup(category, index);
        }

        private bool CanExecuteScrollToGroup(string category, int index)
        {
            return this.Foods.Any(vm => vm.GroupName.Contains(category) && vm.Any(vmi => vmi.Index == index));
        }
    }
}
