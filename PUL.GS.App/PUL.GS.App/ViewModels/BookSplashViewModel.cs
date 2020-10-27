using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PUL.GS.App.ViewModels
{
    public class BookSplashViewModel : BaseViewModel
    {
        private string bookSplashText;
        private string bookSplashImage;

        public string BookSplashText
        {
            get => bookSplashText; set
            {
                bookSplashText = value;
                OnPropertyChanged();
            }
        }

        public string BookSplashImage
        {
            get => bookSplashImage; set
            {
                bookSplashImage = value;
                OnPropertyChanged();
            }
        }
        public BookSplashViewModel()
        {
            BookSplashText = "Tu reserva está en proceso de ser aceptada";
            BookSplashImage = "watch.png";
        }


        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await Task.Delay(5000);
            await CoreMethods.PopToRoot(false);
        }
    }
}
