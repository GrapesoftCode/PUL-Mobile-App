using Acr.UserDialogs;
using Com.OneSignal;
using FreshMvvm;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {
        public int Tip { get; set; }
        public double SubtotalTip { get; set; }
        public double Total { get; set; }

        public Book CurrentBook
        {
            get => currentBook; set
            {
                currentBook = value;
                OnPropertyChanged();
            }
        }
        private Book currentBook;
        private List<Menu> menus;

        public List<Menu> Menus
        {
            get => menus; set
            {
                menus = value;
                OnPropertyChanged();
            }
        }

        public ICommand BookCommand { get; set; }
        public ICommand TipCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public readonly IUserDialogs dialogs;
        public BookDetailViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;

            BookCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    dialogs.ShowLoading("Reservando");
                    if (CurrentBook != null)
                    {
                        var result = await bookAgent.AddBook(CurrentBook);
                        if (result.Success)
                        {
                            //await CoreMethods.PopModalNavigationService();
                            //dialogs.Alert($"Tu reservación está pendiente.");
                            string[] playersId = new string[] { "All" };
                            Notification notification = new Notification()
                            {
                                app_id = "5b7438fc-33c7-4ca1-bfe6-2cccfae4788d",
                                url = "http://grapesoft-001-site1.ctempurl.com/",
                                included_segments = playersId,
                                headings = new Language()
                                {
                                    en = "PUL Reservación"
                                },
                                contents = new Language()
                                {
                                    en = "Tienes una reservación pendiente."
                                }
                            };

                            var notificationResult = notificationAgent.CreateNotification(notification);
                            if (!notificationResult.Success)
                            {
                                result.Error = new Error()
                                {
                                    Message = $"Se actualizo la solicitud y el usuario no fue notificado, contacte al area de sistemas."
                                };
                            }
                            else
                            {
                                await CoreMethods.PushPageModel<BookSplashViewModel>(notificationResult);
                            }
                        }


                        CurrentBook = null;
                    }
                    dialogs.HideLoading();
                    IsBusy = false;
                }
            });

            TipCommand = new Command((p) =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    dialogs.ShowLoading("Conectando");
                    Tip = Convert.ToInt32(p.ToString());
                    CurrentBook.TipPercent = Tip;
                    //RefreshItems();
                    Total = 0;
                    double percentTip = (double)Tip / (double)100;
                    SubtotalTip = percentTip * CurrentBook.SubTotal;
                    Total = CurrentBook.SubTotal + SubtotalTip;
                    CurrentBook.SubTotal = CurrentBook.SubTotal;
                    CurrentBook.Total = Total;                    

                    dialogs.HideLoading();
                    IsBusy = false;
                }
            });

            //RefreshCommand = new Command(async () =>
            //{
            //    IsRefreshing = true;
            //    await Task.Delay(3000);
            //    RefreshItems();
            //    IsRefreshing = false;
            //});

            Tip = 0;
            SubtotalTip = 0;
            Total = 0;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Menus = currentBook.Menus;
        }

        //private void RefreshItems()
        //{
        //    SubtotalTip = Tip * CurrentBook.SubTotal;
        //    Total = CurrentBook.SubTotal + SubtotalTip;
        //}
    }
}
