﻿using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        public string UserName { get; set; }
        public bool IsBusy { get; set; }

        private readonly EstablishmentData _establishmentAgent;
        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };
        public ObservableCollection<Promotion> Promotions { get; set; }
        public ObservableCollection<Establishment> Establishments { get; set; }
        public ObservableCollection<Book> Books { get; set; }
        public MainViewModel()
        {
            _establishmentAgent = new EstablishmentData(appSettings);
            Establishments = new ObservableCollection<Establishment>(_establishmentAgent.GetAll().objectResult.Records);
        }
    }
}
