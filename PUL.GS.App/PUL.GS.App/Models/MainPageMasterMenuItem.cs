﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PUL.GS.App.Models
{

    public class MainPageMasterMenuItem
    {
        public MainPageMasterMenuItem()
        {
            TargetType = new  NavigationPage();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public NavigationPage TargetType { get; set; }
    }
}