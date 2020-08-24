using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class EstablishmentDetailViewModel :  FreshBasePageModel
    {
        public Establishment CurrentEstablishment { get; set; } = new Establishment();
        public bool IsBusy { get; set; }

        readonly IUserDialogs dialogs;

        public EstablishmentDetailViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;
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
                UserId = establishment.UserId
            };

        }

        public ICommand BookCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Cargando");

                await CoreMethods.PushPageModel<BookViewModel>();

                dialogs.HideLoading();

                IsBusy = false;
            }
        });
    }
}
