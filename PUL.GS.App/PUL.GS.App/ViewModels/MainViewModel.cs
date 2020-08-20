using FreshMvvm;
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
        public User CurrentUser { get; set; }
        public MainViewModel()
        {
        }


        public override void Init(object initData)
        {
            CurrentUser = initData as User;
        }
    }
}
