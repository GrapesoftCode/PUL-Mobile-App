using System;
using System.Collections.Generic;
using System.Text;
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


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BookSplashText = "Tu reserva ha sido aceptada";
            BookSplashImage = "check.png";
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
    }
}
