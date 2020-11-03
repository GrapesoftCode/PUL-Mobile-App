using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Promotion> Promotions { get; set; }
        public ObservableCollection<Establishment> Establishments { get; set; }
        public ObservableCollection<Combo> Combos { get; set; }

        public User CurrentUser { get; set; }
        public Establishment CurrentEstablishment { get; set; }
        public Promotion CurrentPromotion { get; set; }
        public Combo CurrentCombo { get; set; }
        public Book CurrentBook { get; set; } = new Book();
        public Book LastBook { get; set; }
        public bool IsRefreshing
        {
            get => isRefreshing; set
            {
                isRefreshing = value;
            }
        }

        public ICommand EstablishmentCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (CurrentEstablishment != null)
                {
                    CurrentBook.User = CurrentUser;
                    CurrentBook.Establishment = CurrentEstablishment;
                    CurrentBook.establishmentId = CurrentEstablishment.id;
                    CurrentBook.User = CurrentUser;
                    CurrentBook.userId = CurrentUser.id;
                    await CoreMethods.PushPageModel<BookViewModel>(CurrentBook);
                    CurrentEstablishment = null;
                }

                IsBusy = false;
            }
        });
        public ICommand PromotionCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (CurrentPromotion != null)
                {
                    Promotion promotion = CurrentPromotion;
                    await CoreMethods.PushPageModel<BookViewModel>(CurrentUser);
                    CurrentEstablishment = null;
                }

                IsBusy = false;
            }
        });
        public ICommand ComboCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (CurrentCombo != null)
                {
                    Combo combo = CurrentCombo;
                    await CoreMethods.PushPageModel<BookViewModel>(CurrentUser);
                    CurrentEstablishment = null;
                }

                IsBusy = false;
            }
        });
        public ICommand TikectCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPopupPageModel<TikectViewModel>(LastBook.FullName);

                dialogs.HideLoading();

                IsBusy = false;
            }
        });
        public ICommand TicketDetailCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPageModel<BookProgressViewModel>(LastBook);

                dialogs.HideLoading();

                IsBusy = false;
            }
        });

        public ICommand RefreshCommand;

        public bool BookVisible
        {
            get => bookVisible; set
            {
                bookVisible = value;
                OnPropertyChanged();
            }
        }

        readonly IUserDialogs dialogs;
        private bool bookVisible;
        private bool isRefreshing;

        public HomeViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await Task.Delay(3000);
                await RefreshItems();
                IsRefreshing = false;
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;
        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            await RefreshItems();

            dialogs.HideLoading();
        }

        private async Task RefreshItems()
        {
            var establishmentList = await establishmentAgent.GetRecordsEstablishment();
            var promotionList = await promotionAgent.GetAll();
            var comboList = await comboAgent.GetAll();
            Establishments = new ObservableCollection<Establishment>(establishmentList.objectResult.Records);
            Promotions = new ObservableCollection<Promotion>(promotionList.objectResult.Records);
            Combos = new ObservableCollection<Combo>(comboList.objectResult.Records);

            var book = await bookAgent.GetBookByUserId(CurrentUser.id);
            if (book.objectResult != null)
                BookVisible = true;
            else
                BookVisible = false;
            LastBook = book.objectResult;
        }
    }
}
