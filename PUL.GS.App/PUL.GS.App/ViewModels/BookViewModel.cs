using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class BookViewModel : FreshBasePageModel
    {
        public Establishment CurrentEstablishment { get; set; } = new Establishment();

        private readonly TableData tableAgent;
        public bool IsBusy { get; set; }

        readonly IUserDialogs dialogs;
        public ObservableCollection<Table> Tables { get; set; }

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };


        public BookViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;
            tableAgent = new TableData(appSettings);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var establishment = initData as Establishment;

            CurrentEstablishment = new Establishment()
            {
                id = establishment.id,
                Name = establishment.Name,
                PhoneNumber = establishment.PhoneNumber,
                Email = establishment.Email,
                Cover = establishment.Cover,
                Rfc = establishment.Rfc,
                Logo = establishment.Logo,
                userId = establishment.userId
            };

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            Tables = new ObservableCollection<Table>(tableAgent.GetTables(CurrentEstablishment.userId, CurrentEstablishment.id).objectResult);

            dialogs.HideLoading();
        }

    }
}
