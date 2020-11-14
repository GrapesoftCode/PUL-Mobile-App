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
        readonly IUserDialogs dialogs;
        public string ColorBc5
        {
            get => colorBc5; set
            {
                colorBc5 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText5
        {
            get => colorText5; set
            {
                colorText5 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg5
        {
            get => colorBg5; set
            {
                colorBg5 = value;
                OnPropertyChanged();
            }
        }

        public string ColorBc10
        {
            get => colorBc10; set
            {
                colorBc10 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText10
        {
            get => colorText10; set
            {
                colorText10 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg10
        {
            get => colorBg10; set
            {
                colorBg10 = value;
                OnPropertyChanged();
            }
        }

        public string ColorBc15
        {
            get => colorBc15; set
            {
                colorBc15 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText15
        {
            get => colorText15; set
            {
                colorText15 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg15
        {
            get => colorBg15; set
            {
                colorBg15 = value;
                OnPropertyChanged();
            }
        }

        public string ColorBc20
        {
            get => colorBc20; set
            {
                colorBc20 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText20
        {
            get => colorText20; set
            {
                colorText20 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg20
        {
            get => colorBg20; set
            {
                colorBg20 = value;
                OnPropertyChanged();
            }
        }
        public List<Contact> UpdateContacts { get; set; }
        public ObservableCollection<Contact> Contacts
        {
            get => contacts; set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }
        public List<Menu> Menus
        {
            get => menus; set
            {
                menus = value;
                OnPropertyChanged();
            }
        }
        public Contact CurrentContact
        {
            get => currentContact; set
            {
                currentContact = value;
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
        private bool isRefreshing;
        private Contact currentContact;
        private int coverCount;
        private double cover;
        private string colorBc5;
        private string colorText5;
        private string colorText20;
        private string colorBg20;
        private string colorBg5;
        private string colorBc10;
        private string colorText10;
        private string colorBg10;
        private string colorBc15;
        private string colorText15;
        private string colorBg15;
        private string colorBc20;
        private double tip;

        public ObservableCollection<Contact> contacts { get; set; }


        public double Tip
        {
            get => tip; set
            {
                tip = value;
                OnPropertyChanged();
            }
        }
        public int TipPercent { get; set; }
        public double SubtotalTip { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }

        public int CoverCount
        {
            get => coverCount; set
            {
                coverCount = value;
                OnPropertyChanged("CoverCount");

                if (CoverCount > 0)
                {
                    Cover = 100 * CoverCount;
                }

            }
        }

        public double Cover
        {
            get => cover; set
            {
                cover = value;
                OnPropertyChanged();
            }
        }


        public int Split { get; set; } = 1;
        public double Personal { get; set; }
        public bool IsRefreshing
        {
            get => isRefreshing; set
            {
                isRefreshing = value;
            }
        }
        public ICommand SplitAccountCommand => new Command(async () =>
        {
            dialogs.ShowLoading("Dividir cuenta...");
            if (!IsBusy)
            {
                IsBusy = true;
                if (!IsInitialized)
                {
                    if (CurrentContact != null)
                    {
                        //UpdateContacts = new List<Contact>();
                        var last = CurrentContact;
                        var exists = UpdateContacts.Count > 0 ? UpdateContacts.Where(x => x.id == last.id).SingleOrDefault() : null;
                        if (exists == null)
                        {
                            last.Split = "split.png";
                            UpdateContacts.Add(last);
                        }
                        else
                        {
                            UpdateContacts.Remove(exists);
                        }
                        Split = 1;
                        Split += UpdateContacts.Count();
                        await RefreshItems();
                        SplitCalculation();
                    }
                    IsInitialized = false;
                }
                IsBusy = false;
            }
            dialogs.HideLoading();
        });

        public ICommand RefreshCommand => new Command(async () =>
        {
            IsRefreshing = true;
            await RefreshItems();
            IsRefreshing = false;
        });

        public ICommand TipCommand => new Command(async (p) =>
        {
            if (!IsBusy)
            {
                IsBusy = true;
                dialogs.ShowLoading("Conectando");
                TipPercent = Convert.ToInt32(p.ToString());
                TipCalculation();
                await RefreshItems();
                dialogs.HideLoading();
                IsBusy = false;
            }
        });

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

        public ICommand CoverCommand => new Command(async (price) =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                if (CoverCount > 0)
                {
                    double cost = (double)price;
                    Cover = cost * CoverCount;
                    await Task.Delay(1000);
                }

                dialogs.HideLoading();

                IsBusy = false;
            }
        });


        public ICommand AddExtrasCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                CurrentBook.TipPercent = TipPercent;
                currentBook.SubTotal = SubTotal;
                currentBook.Tip = SubtotalTip;

                await CoreMethods.PushPageModel<BookProgressMenuViewModel>(CurrentBook);

                dialogs.HideLoading();

                IsBusy = false;
            }
        });


        public ICommand PayCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                await Task.Delay(5000);
                await CoreMethods.PopToRoot(false);

                dialogs.HideLoading();

                IsBusy = false;
            }
        });


        public BookProgressViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;

            Tip = 0;
            SubtotalTip = 0;
            Total = 0;

            ColorBc5 = "Black";
            ColorText5 = "Black";
            ColorBg5 = "#FFFFFF";

            ColorBc10 = "Black";
            ColorText10 = "Black";
            ColorBg10 = "#FFFFFF";

            ColorBc15 = "Black";
            ColorText15 = "Black";
            ColorBg15 = "#FFFFFF";

            ColorBc20 = "Black";
            ColorText20 = "Black";
            ColorBg20 = "#FFFFFF";

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            dialogs.ShowLoading("Cargando...");
            CurrentBook = initData as Book;
            UpdateContacts = new List<Contact>();
            await RefreshItems();
            dialogs.HideLoading();
        }

        public override async void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            dialogs.ShowLoading("Cargando...");
            if (returnedData != null)
            {
                var bookExtras = returnedData as Book;
                //var menuExtras = bookExtras.Menus;
                //CurrentBook.Menus = menuExtras;

                CurrentBook = bookExtras;
                //SubTotal = CurrentBook.SubTotal;
                //SubtotalTip = CurrentBook.Tip;
                //Total = CurrentBook.Total;
                await RefreshItems();
            }
            dialogs.HideLoading();
        }

        //protected override void ViewIsAppearing(object sender, EventArgs e)
        //{
        //    base.ViewIsAppearing(sender, e);

        //    dialogs.ShowLoading("Cargando");



        //    dialogs.HideLoading();
        //}

        private async Task RefreshItems()
        {
            var list = await contactAgent.GetAll(CurrentBook.User.Username);
            foreach (var item in list.objectResult)
            {
                var update = UpdateContacts.Count > 0 ? UpdateContacts.Where(x => x.id == item.id).SingleOrDefault() : null;
                if (update != null)
                {
                    item.Split = update.Split;
                }
                else
                {
                    item.Split = "notsplit.png";
                }
            }
            Contacts = new ObservableCollection<Contact>(list.objectResult);

            Menus = CurrentBook.Menus;
            SubTotal = CurrentBook.SubTotal;
            Tip = Tip;
            //SubtotalTip = ((double)(Tip)/100) * CurrentBook.SubTotal;            

            TipCalculation();
            SplitCalculation();
            Total = SubTotal + SubtotalTip;
            RefreshLabel(CurrentBook.TipPercent);
        }

        public void TipCalculation()
        {
            Tip = (double)TipPercent / (double)100;
            SubtotalTip = Tip * SubTotal;
        }

        private void SplitCalculation()
        {
            Personal = Total / Split;
        }
        public void RefreshLabel(int tip)
        {
            if (tip == 5)
            {
                ColorBc5 = "#F05E13";
                ColorText5 = "#FFFFFF";
                ColorBg5 = "#F05E13";

                ColorBc10 = "Black";
                ColorText10 = "Black";
                ColorBg10 = "#FFFFFF";

                ColorBc15 = "Black";
                ColorText15 = "Black";
                ColorBg15 = "#FFFFFF";

                ColorBc20 = "Black";
                ColorText20 = "Black";
                ColorBg20 = "#FFFFFF";
            }
            else if (tip == 10)
            {
                ColorBc10 = "#F05E13";
                ColorText10 = "#FFFFFF";
                ColorBg10 = "#F05E13";

                ColorBc5 = "Black";
                ColorText5 = "Black";
                ColorBg5 = "#FFFFFF";

                ColorBc15 = "Black";
                ColorText15 = "Black";
                ColorBg15 = "#FFFFFF";

                ColorBc20 = "Black";
                ColorText20 = "Black";
                ColorBg20 = "#FFFFFF";
            }
            else if (tip == 15)
            {
                ColorBc15 = "#F05E13";
                ColorText15 = "#FFFFFF";
                ColorBg15 = "#F05E13";

                ColorBc5 = "Black";
                ColorText5 = "Black";
                ColorBg5 = "#FFFFFF";

                ColorBc10 = "Black";
                ColorText10 = "Black";
                ColorBg10 = "#FFFFFF";

                ColorBc20 = "Black";
                ColorText20 = "Black";
                ColorBg20 = "#FFFFFF";

            }
            else if (tip == 20)
            {
                ColorBc20 = "#F05E13";
                ColorText20 = "#FFFFFF";
                ColorBg20 = "#F05E13";

                ColorBc5 = "Black";
                ColorText5 = "Black";
                ColorBg5 = "#FFFFFF";

                ColorBc10 = "Black";
                ColorText10 = "Black";
                ColorBg10 = "#FFFFFF";

                ColorBc15 = "Black";
                ColorText15 = "Black";
                ColorBg15 = "#FFFFFF";
            }
        }
    }
}
