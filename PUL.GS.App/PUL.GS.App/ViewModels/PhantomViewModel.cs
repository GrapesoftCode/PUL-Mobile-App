using Acr.UserDialogs;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class PhantomViewModel : BaseViewModel
    {
        readonly IUserDialogs dialogs;
        public StatusIndicator CurrentPhantom { get; set; }
        public ObservableCollection<StatusIndicator> PhantomList { get; set; }


        public ICommand PhantomCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");


                if(CurrentPhantom != null)
                {
                    //await CoreMethods.PopPageModel(CurrentPhantom);

                    await CoreMethods.PopPopupPageModel();
                   
                    //await CoreMethods.PopToRoot(false);


                    CurrentPhantom = null;
                }

                dialogs.HideLoading();

                IsBusy = false;
            }
            //if ((string)color == "greenphantom.png")

            //    Phantom = "redphantom.png";
            //else
            //    Phantom = "greenphantom.png";
        });


        public PhantomViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
            RefreshItems();
        }

        private void RefreshItems()
        {
            List<StatusIndicator> phantom = new List<StatusIndicator>()
            {
                new StatusIndicator(){
                    Value = "Inactivo",
                    Image = "redphantom.png"
                },
                 new StatusIndicator(){
                    Value = "Activo",
                    Image = "greenphantom.png"
                },
                  new StatusIndicator(){
                    Value = "Facebook",
                    Image = "bluephantom.png"
                },
            };

            PhantomList = new ObservableCollection<StatusIndicator>(phantom);
        }
    }
}
