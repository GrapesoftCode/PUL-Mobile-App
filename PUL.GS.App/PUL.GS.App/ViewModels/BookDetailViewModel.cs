using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.Models;
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
        int Tip
        {
            get => tip; set
            {
                tip = value;
                OnPropertyChanged();
            }
        }
        double SubtotalTip
        {
            get => subtotalTip; set
            {
                subtotalTip = value;
                OnPropertyChanged();
            }
        }
        double Total
        {
            get => total; set
            {
                total = value;
                OnPropertyChanged();
            }
        }
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
        private bool isRefreshing;
        private double subtotalTip = 0;
        private double total = 0;
        private int tip = 0;

        public List<Menu> Menus
        {
            get => menus; set
            {
                menus = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get => isRefreshing; set
            {
                isRefreshing = value;
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
                    dialogs.ShowLoading("Conectando");
                    if (CurrentBook != null)
                    {
                        var result = bookAgent.AddBook(CurrentBook);
                        if (result.Success)
                        {
                            //await CoreMethods.PopModalNavigationService();
                            dialogs.Alert($"Tu reservación está pendiente.");
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
                    RefreshItems();
                    dialogs.HideLoading();
                    IsBusy = false;
                }
            });

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await Task.Delay(3000);
                RefreshItems();
                IsRefreshing = false;
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;
            RefreshItems();
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Menus = currentBook.Menus;
        }


        private void RefreshItems()
        {
            SubtotalTip = (CurrentBook.SubTotal * Math.Round((double)Tip / 100, 2));
            Total = SubtotalTip + CurrentBook.SubTotal;
        }
    }
}
