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
    public class HomeViewModel : FreshBasePageModel
    {
        private readonly EstablishmentData establishmentAgent;
        private readonly PromotionData promotionAgent;

        readonly IUserDialogs dialogs;
        public User CurrentUser { get; set; }
        public bool IsBusy { get; set; }
        

        public Establishment CurrentEstablishment { get; set; }
        public ICommand EnterEstablishmentCommand { get; set; }

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };
        public ObservableCollection<Promotion> Promotions { get; set; }
        public ObservableCollection<Establishment> Establishments { get; set; }
        public ObservableCollection<Book> Books { get; set; }

        public HomeViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
            establishmentAgent = new EstablishmentData(appSettings);
            promotionAgent = new PromotionData(appSettings);
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            EnterEstablishmentCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentEstablishment != null)
                    {
                        Establishment establishment = CurrentEstablishment;
                        await CoreMethods.PushPageModel<BookViewModel>(establishment);
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

            Establishments = new ObservableCollection<Establishment>(establishmentAgent.GetAll().objectResult.Records);
            Promotions = new ObservableCollection<Promotion>(promotionAgent.GetAll().objectResult.Records);
            //Rooms = await ChatService.GetRooms();

            dialogs.HideLoading();
        }
    }
}
