using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class BookProgressViewModel : BaseViewModel
    {
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
