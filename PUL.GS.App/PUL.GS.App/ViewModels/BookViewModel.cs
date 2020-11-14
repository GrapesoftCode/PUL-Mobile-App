using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Controls;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
       
        public DateTime Hour
        {
            get => hour; set
            {
                hour = value;
                BookViewModel_PropertyChanged(this, new PropertyChangedEventArgs("Hour"));
            }
        }

        public DateTime MinimumDate
        {
            get => minimumDate;
            set
            {
                minimumDate = value;
                BookViewModel_PropertyChanged(this, new PropertyChangedEventArgs("MinimumDate"));
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                BookViewModel_PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }

        private void BookViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Date = e.PropertyName.ToString();
        }

        public Establishment CurrentEstablishment { get; set; } = new Establishment();
        public Book CurrentBook { get; set; } = new Book();
        public Table CurrentTable { get; set; } = new Table();

        public ICommand TableCommand { get; set; }
        public readonly IUserDialogs dialogs;

        public ObservableCollection<Table> Tables { get; set; }

        private DateTime date = DateTime.Now;
        private DateTime minimumDate = DateTime.Now;
        private DateTime hour = DateTime.Now.ToLocalTime();
        public int Persons { get; set; } = 0;

        public BookViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;

            TableCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentTable != null)
                    {
                        if (Persons > 0)
                        {
                            //CurrentBook.Establishment = CurrentEstablishment;
                            CurrentBook.Table = CurrentTable;
                            CurrentBook.Persons = Persons;
                            CurrentBook.Hour = Hour.ToString("hh:mm:ss tt");
                            CurrentBook.Date = Date.ToString("dd/MM/yyyy");
                            CurrentBook.PlayerId = CurrentBook.User.PlayerId;
                            CurrentBook.TotalMinimumConsumption = CurrentTable.MinimumConsumption * Persons;

                            await CoreMethods.PushPageModel<BookMenuViewModel>(CurrentBook);

                            CurrentTable = null;
                        }

                        //Tables.CollectionChanged += Tables_CollectionChanged;
                        CurrentTable = null;
                    }

                    IsBusy = false;
                }
            });

        }

        //private void Tables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if (e is null) return;
        //    ((CollectionView)sender).SelectedItem = null;
        //    e.CurrentSelection = null;
        //    IngredientCollectionView.SelectedItem = null;
        //}

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando...");

            var tables = await tableAgent.GetTables(CurrentBook.Establishment.UserId, CurrentBook.Establishment.id);

            var tableList = tables.objectResult;

            if (tableList != null)
            {
                var groups = tableList
                                .GroupBy(x => new { x.Location, x.Quantity, x.MinimumConsumption })
                                .Select(x => new Table
                                {
                                    Location = x.Key.Location,
                                    Quantity = x.Key.Quantity,
                                    MinimumConsumption = x.Key.MinimumConsumption,
                                    Count = x.Select(z => z.id).Distinct().Count(),
                                });

                Tables = new ObservableCollection<Table>(groups);
            }

            //var queries2 = listTables.GroupBy(g => new { g.Location, g.Quantity, g.MinimumConsumption })
            //             .Select(g => g.First())
            //             .ToList();
            dialogs.HideLoading();
        }

    }
}
