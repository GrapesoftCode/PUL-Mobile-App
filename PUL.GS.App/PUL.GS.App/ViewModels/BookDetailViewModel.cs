using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {
        public Book CurrentBook
        {
            get => currentBook; set
            {
                currentBook = value;
            }
        }
        private Book currentBook;
        private List<Menu> menus;

        public List<Menu> Menus
        {
            get => menus; set
            {
                menus = value;
            }
        }

        public ICommand BookCommand { get; set; }
        public readonly IUserDialogs dialogs;
        public BookDetailViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;
        }

    public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;


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
                            dialogs.Alert($"Tu reservación esta pendiente.");
                        }
                        
                        
                        CurrentBook = null;
                    }
                    dialogs.HideLoading();
                    IsBusy = false;
                }
            });
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Menus = currentBook.Menus;
        }
    }
}
