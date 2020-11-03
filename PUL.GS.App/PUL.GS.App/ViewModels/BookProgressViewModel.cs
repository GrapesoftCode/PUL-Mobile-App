using Acr.UserDialogs;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class BookProgressViewModel : BaseViewModel
    {
        public int Split { get; set; } = 1;
        public double Personal { get; set; }
        public bool IsRefreshing
        {
            get => isRefreshing; set
            {
                isRefreshing = value;
            }
        }
        public ICommand SplitAccountCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand TikectCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPopupPageModel<TikectViewModel>(CurrentBook.FullName);

                dialogs.HideLoading();

                IsBusy = false;
            }
        });
        public List<Contact> UpdateContacts { get; set; }
        public ObservableCollection<Contact> Contacts
        {
            get => contacts; set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<object> CurrentContacts
        {
            get => currentContacts; set
            {
                currentContacts = value;
                OnPropertyChanged();
            }
        }

        public Book CurrentBook
        {
            get => currentBook; set
            {
                currentBook = value;
                OnPropertyChanged();
            }
        }
        private Book currentBook;
        private List<Menu> menus;
        private ObservableCollection<object> currentContacts;
        private bool isRefreshing;

        public List<Menu> Menus
        {
            get => menus; set
            {
                menus = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Contact> contacts { get; set; }
        readonly IUserDialogs dialogs;
        public BookProgressViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;
            SplitAccountCommand = new Command(() =>
            {
                dialogs.ShowLoading("Dividir cuenta...");
                if (!IsBusy)
                {
                    IsBusy = true;
                    if (!IsInitialized)
                    {
                        //UpdateContacts = new List<Contact>();
                        var last = CurrentContacts.LastOrDefault();
                        var contact = (Contact)last;
                        var exists = UpdateContacts.Count > 0 ? UpdateContacts.Where(x => x.id == contact.id).SingleOrDefault() : null;
                        if (exists == null)
                        {
                            contact.Split = "split.png";
                            UpdateContacts.Add(contact);
                        }
                        else
                        {
                            UpdateContacts.Remove(exists);
                        }
                        Split = 1;
                        Split += UpdateContacts.Count();
                        RefreshItems();
                        SplitCalculation();

                        IsInitialized = false;
                        IsBusy = false;
                    }
                }
                dialogs.HideLoading();
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
            CurrentContacts = new ObservableCollection<object>();
            UpdateContacts = new List<Contact>();
            RefreshItems();
            SplitCalculation();
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Menus = currentBook.Menus;


        }

        private async void RefreshItems()
        {
            var list = await contactAgent.GetAll(CurrentBook.User.Username);
            foreach (var item in list.objectResult)
            {
                var update = UpdateContacts.Count > 0 ? UpdateContacts.Where(x => x.id == item.id).SingleOrDefault() : null;
                if (update!=null)
                {
                    item.Split = update.Split;
                }
                else
                {
                    item.Split = "notsplit.png";
                }
            }
            Contacts = new ObservableCollection<Contact>(list.objectResult);
        }

        private void SplitCalculation()
        {
            Personal = CurrentBook.Total / Split;
        }
    }
}
