using FreshMvvm;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
        private ObservableCollection<Menu> menus;

        public ObservableCollection<Menu> Menus
        {
            get => menus; set
            {
                menus = value;
            }
        }

        public BookDetailViewModel()
        {

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
    }
}
