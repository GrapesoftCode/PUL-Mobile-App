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
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Promotion> Promotions { get; set; }
        public ObservableCollection<Establishment> Establishments { get; set; }
        public ObservableCollection<Combo> Combos { get; set; }
        public ObservableCollection<Book> Books { get; set; }

        public User CurrentUser { get; set; }
        public Establishment CurrentEstablishment { get; set; }
        public Promotion CurrentPromotion { get; set; }
        public Combo CurrentCombo { get; set; }

        public Book CurrentBook { get; set; } = new Book();

        public ICommand EstablishmentCommand { get; set; }
        public ICommand PromotionCommand { get; set; }
        public ICommand ComboCommand { get; set; }

        readonly IUserDialogs dialogs;
        public HomeViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            EstablishmentCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentEstablishment != null)
                    {
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

            PromotionCommand = new Command(async () => {
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

            ComboCommand = new Command(async () => {
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
        }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            Establishments = new ObservableCollection<Establishment>(establishmentAgent.GetRecordsEstablishment().objectResult.Records);
            Promotions = new ObservableCollection<Promotion>(promotionAgent.GetAll().objectResult.Records);
            Combos = new ObservableCollection<Combo>(comboAgent.GetAll().objectResult.Records);

            dialogs.HideLoading();
        }
    }
}
