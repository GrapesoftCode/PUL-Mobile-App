using Acr.UserDialogs;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        bool isInitialized = false;

        public bool IsInitialized
        {
            get { return isInitialized; }
            set { SetProperty(ref isInitialized, value); }
        }

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        public readonly EstablishmentData establishmentAgent;
        public readonly PromotionData promotionAgent;
        public readonly ComboData comboAgent;
        public readonly TableData tableAgent;
        public readonly MenuData menuAgent;

        public BaseViewModel()
        {
            establishmentAgent = new EstablishmentData(appSettings);
            promotionAgent = new PromotionData(appSettings);
            comboAgent = new ComboData(appSettings);
            tableAgent = new TableData(appSettings);
            menuAgent = new MenuData(appSettings);
        }
    }
}
