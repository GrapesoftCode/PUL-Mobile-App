using FreshMvvm;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class HomeTabbedViewModel : FreshBasePageModel
    {
        public User CurrentUser { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;
        }
    }
}
