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

        public string ColorBc5
        {
            get => colorBc5; set
            {
                colorBc5 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText5
        {
            get => colorText5; set
            {
                colorText5 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg5
        {
            get => colorBg5; set
            {
                colorBg5 = value;
                OnPropertyChanged();
            }
        }

        public string ColorBc10
        {
            get => colorBc10; set
            {
                colorBc10 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText10
        {
            get => colorText10; set
            {
                colorText10 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg10
        {
            get => colorBg10; set
            {
                colorBg10 = value;
                OnPropertyChanged();
            }
        }

        public string ColorBc15
        {
            get => colorBc15; set
            {
                colorBc15 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText15
        {
            get => colorText15; set
            {
                colorText15 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg15
        {
            get => colorBg15; set
            {
                colorBg15 = value;
                OnPropertyChanged();
            }
        }

        public string ColorBc20
        {
            get => colorBc20; set
            {
                colorBc20 = value;
                OnPropertyChanged();
            }
        }
        public string ColorText20
        {
            get => colorText20; set
            {
                colorText20 = value;
                OnPropertyChanged();
            }
        }
        public string ColorBg20
        {
            get => colorBg20; set
            {
                colorBg20 = value;
                OnPropertyChanged();
            }
        }


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
        private string colorBc15;
        private string colorText15;
        private string colorBg15;
        private string colorBc5;
        private string colorText5;
        private string colorBg5;
        private string colorBc10;
        private string colorText10;
        private string colorBg10;
        private string colorBc20;
        private string colorText20;
        private string colorBg20;

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

                                await CoreMethods.PushPageModel<BookSplashViewModel>(notificationResult);
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

            TipCommand = new Command(async (p) =>
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

                    await RefreshLabel(Tip);
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

            ColorBc5 = "Black";
            ColorText5 = "Black";
            ColorBg5 = "#FFFFFF";

            ColorBc10 = "Black";
            ColorText10 = "Black";
            ColorBg10 = "#FFFFFF";

            ColorBc15 = "Black";
            ColorText15 = "Black";
            ColorBg15 = "#FFFFFF";

            ColorBc20 = "Black";
            ColorText20 = "Black";
            ColorBg20 = "#FFFFFF";
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



        public async Task RefreshLabel(int tip)
        {
            if (tip == 5)
            {
                ColorBc5 = "#F05E13";
                ColorText5 = "#FFFFFF";
                ColorBg5 = "#F05E13";

                ColorBc10 = "Black";
                ColorText10 = "Black";
                ColorBg10 = "#FFFFFF";

                ColorBc15 = "Black";
                ColorText15 = "Black";
                ColorBg15 = "#FFFFFF";

                ColorBc20 = "Black";
                ColorText20 = "Black";
                ColorBg20 = "#FFFFFF";
            }
            else if (tip == 10)
            {
                ColorBc10 = "#F05E13";
                ColorText10 = "#FFFFFF";
                ColorBg10 = "#F05E13";

                ColorBc5 = "Black";
                ColorText5 = "Black";
                ColorBg5 = "#FFFFFF";

                ColorBc15 = "Black";
                ColorText15 = "Black";
                ColorBg15 = "#FFFFFF";

                ColorBc20 = "Black";
                ColorText20 = "Black";
                ColorBg20 = "#FFFFFF";
            }
            else if (tip == 15)
            {
                ColorBc15 = "#F05E13";
                ColorText15 = "#FFFFFF";
                ColorBg15 = "#F05E13";

                ColorBc5 = "Black";
                ColorText5 = "Black";
                ColorBg5 = "#FFFFFF";

                ColorBc10 = "Black";
                ColorText10 = "Black";
                ColorBg10 = "#FFFFFF";

                ColorBc20 = "Black";
                ColorText20 = "Black";
                ColorBg20 = "#FFFFFF";

            }
            else if (tip == 20)
            {
                ColorBc20 = "#F05E13";
                ColorText20 = "#FFFFFF";
                ColorBg20 = "#F05E13";

                ColorBc5 = "Black";
                ColorText5 = "Black";
                ColorBg5 = "#FFFFFF";

                ColorBc10 = "Black";
                ColorText10 = "Black";
                ColorBg10 = "#FFFFFF";

                ColorBc15 = "Black";
                ColorText15 = "Black";
                ColorBg15 = "#FFFFFF";
            }
        }


        //private void RefreshItems()
        //{
        //    SubtotalTip = Tip * CurrentBook.SubTotal;
        //    Total = CurrentBook.SubTotal + SubtotalTip;
        //}
    }
}
