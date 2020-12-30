using Acr.UserDialogs;
using Com.OneSignal;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class RegisterViewModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }
        public User NewUser { get; set; } = new User();
        IUserDialogs dialogs;

        private readonly AccountData _accountAgent;
        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        public RegisterViewModel(IUserDialogs _userDialogs)
        {
            _accountAgent = new AccountData(appSettings);
            dialogs = _userDialogs;
        }

        public ICommand NewUserCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;
                dialogs.ShowLoading("Conectando");

                NewUser.Role = new Role { Id = 3, Name = "Puler" };
                Com.OneSignal.OneSignal.Current.IdsAvailable(new Com.OneSignal.Abstractions.IdsAvailableCallback((playerID, pushToken) =>
                {
                    NewUser.PlayerId = playerID;
                }));

                var result = await _accountAgent.AddUser(NewUser);
                if (result.Success)
                {
                    await CoreMethods.PushPageModel<LoginViewModel>();
                    dialogs.Alert($"El usuario {NewUser.Username} se creó con éxito.");
                }
                dialogs.HideLoading();
                IsBusy = false;
            }
        });
    }
}
