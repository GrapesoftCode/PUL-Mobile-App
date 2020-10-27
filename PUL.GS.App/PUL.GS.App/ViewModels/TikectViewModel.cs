using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class TikectViewModel : BaseViewModel
    {
        private string fullName;

        public string FullName
        {
            get => fullName; set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        public TikectViewModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            FullName = initData as String;
        }
    }
}
