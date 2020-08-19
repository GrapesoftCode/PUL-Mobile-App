using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class BookViewModel : FreshBasePageModel
    {
        public Establishment CurrentEstablishment { get; set; } = new Establishment();
        public Book CurrentBook { get; set; } = new Book();
        public Table CurrentTable { get; set; } = new Table();

        public ICommand TableCommand { get; set; }

        private readonly TableData tableAgent;
        public bool IsBusy { get; set; }

        readonly IUserDialogs dialogs;
        public ObservableCollection<Table> Tables { get; set; }

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };


        public BookViewModel(IUserDialogs _userDialogs)
        {
            dialogs = _userDialogs;
            tableAgent = new TableData(appSettings);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;

            //CurrentEstablishment = new Establishment()
            //{
            //    id = establishment.id,
            //    //Name = establishment.Name,
            //    //PhoneNumber = establishment.PhoneNumber,
            //    //Email = establishment.Email,
            //    //Cover = establishment.Cover,
            //    //Rfc = establishment.Rfc,
            //    //Logo = establishment.Logo,
            //    //userId = establishment.userId
            //};

            TableCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentTable != null)
                    {
                        CurrentBook.Table = CurrentTable;
                        await CoreMethods.PushPageModel<MenuViewModel>(CurrentBook);
                        CurrentTable = null;
                    }

                    IsBusy = false;
                }
            });

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            var listTables = tableAgent.GetTables(CurrentBook.Establishment.userId, CurrentBook.Establishment.id).objectResult;
            var groups = listTables
                            .GroupBy(x => new { x.Location, x.Quantity, x.MinimumConsumption })
                            .Select(x => new Table{ 
                                Location = x.Key.Location,
                                Quantity = x.Key.Quantity,
                                MinimumConsumption = x.Key.MinimumConsumption,
                                Count = x.Select(z=>z.id).Distinct().Count(),
                            });

            //var queries2 = listTables.GroupBy(g => new { g.Location, g.Quantity, g.MinimumConsumption })
            //             .Select(g => g.First())
            //             .ToList();

            Tables = new ObservableCollection<Table>(groups);

            dialogs.HideLoading();
        }

    }
}
