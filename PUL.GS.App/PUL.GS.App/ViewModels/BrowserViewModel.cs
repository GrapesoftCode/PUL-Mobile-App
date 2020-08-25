using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
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
    public class BrowserViewModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }

        private readonly EstablishmentData establishmentAgent;
        public ObservableCollection<Establishment> Establishments { get; set; }
        public ObservableCollection<Book> Books { get; set; }

        public User CurrentUser { get; set; }
        public Establishment CurrentEstablishment { get; set; }
        public Promotion CurrentPromotion { get; set; }
        public Combo CurrentCombo { get; set; }

        public ICommand EstablishmentCommand { get; set; }

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        readonly IUserDialogs dialogs;
        public BrowserViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
            establishmentAgent = new EstablishmentData(appSettings);
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            //EstablishmentCommand = new Command(async () =>
            //{
            //    if (!IsBusy)
            //    {
            //        IsBusy = true;

            //        if (CurrentEstablishment != null)
            //        {
            //            Establishment establishment = CurrentEstablishment;
            //            await CoreMethods.PushPageModel<BookViewModel>(establishment);
            //            CurrentEstablishment = null;
            //        }

            //        IsBusy = false;
            //    }
            //});
        }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            Establishments = new ObservableCollection<Establishment>(establishmentAgent.GetRecordsEstablishment().objectResult.Records);

            dialogs.HideLoading();
        }
    }
}
