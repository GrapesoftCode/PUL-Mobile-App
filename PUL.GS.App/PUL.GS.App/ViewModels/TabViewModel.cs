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
    public class TabViewModel
    {
        public string Name { set; get; }
        public string TabName { get; set; }
        public string EstablismentId { get; set; }

        public MenuDetailViewModel DetailViewModel { set; get; }

        public TabViewModel(string tabName, string establishmentId )
        {
            TabName = tabName;
            EstablismentId = establishmentId;
            DetailViewModel = new MenuDetailViewModel(TabName, EstablismentId);
        }
    }
}
