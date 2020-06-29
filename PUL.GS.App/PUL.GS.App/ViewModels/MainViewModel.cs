using FreshMvvm;
using PUL.GS.App.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        public string UserName { get; set; }
        public bool IsBusy { get; set; }
        public MainViewModel()
        {
            
        }
    }
}
