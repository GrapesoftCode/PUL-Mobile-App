using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class BrowserViewModel : BaseViewModel
    {
        public ObservableCollection<MusicalGenre> MusicalGenres { get; set; }
        public ObservableCollection<TypeEstablishment> EstablishmentTypes { get; set; }
        public ObservableCollection<CostLevel> CostLevels { get; set; }
        //public ObservableCollection<Book> Books { get; set; }

        public TypeEstablishment CurrentType { get; set; }
        public MusicalGenre CurrentMusicalGenres { get; set; }
        public CostLevel CurrentCostLevel { get; set; }

        public Establishment CurrentEstablishment { get; set; }
        public User CurrentUser { get; set; }
        public Book CurrentBook { get; set; } = new Book();

        public ICommand TypeCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (CurrentType != null)
                {
                    var typeEstablishment = await establishmentAgent.GetRecordsEstablishment();

                    if (typeEstablishment.Success)
                    {
                        var establishments = typeEstablishment.objectResult.Records.ToList();
                        CurrentEstablishment = new Establishment();
                        CurrentEstablishment = establishments.Where(x => x.Type == CurrentType.Name).FirstOrDefault();

                        CurrentBook.User = CurrentUser;
                        CurrentBook.Establishment = CurrentEstablishment;
                        CurrentBook.establishmentId = CurrentEstablishment.id;
                        CurrentBook.User = CurrentUser;
                        CurrentBook.userId = CurrentUser.id;
                        await CoreMethods.PushPageModel<BookViewModel>(CurrentBook);
                    }
                    CurrentType = null;
                }

                IsBusy = false;
            }
        });
        public ICommand MusicalGenresCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (CurrentMusicalGenres != null)
                {
                    var musicalGenre = await establishmentAgent.GetRecordsEstablishment();

                    if (musicalGenre.Success)
                    {
                        var establishments = musicalGenre.objectResult.Records.ToList();
                        CurrentEstablishment = new Establishment();
                        CurrentEstablishment = establishments.Where(x => x.MusicalGenre == CurrentMusicalGenres.Name).FirstOrDefault();

                        CurrentBook.User = CurrentUser;
                        CurrentBook.Establishment = CurrentEstablishment;
                        CurrentBook.establishmentId = CurrentEstablishment.id;
                        CurrentBook.User = CurrentUser;
                        CurrentBook.userId = CurrentUser.id;
                        await CoreMethods.PushPageModel<BookViewModel>(CurrentBook);
                    }
                    CurrentMusicalGenres = null;
                }

                IsBusy = false;
            }
        });
        public ICommand CostLevelCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (CurrentCostLevel != null)
                {
                    var costLevel = await establishmentAgent.GetRecordsEstablishment();

                    if (costLevel.Success)
                    {
                        var establishments = costLevel.objectResult.Records.ToList();
                        CurrentEstablishment = new Establishment();
                        CurrentEstablishment = establishments.Where(x => x.CostLevel == CurrentCostLevel.Name).FirstOrDefault();

                        CurrentBook.User = CurrentUser;
                        CurrentBook.Establishment = CurrentEstablishment;
                        CurrentBook.establishmentId = CurrentEstablishment.id;
                        CurrentBook.User = CurrentUser;
                        CurrentBook.userId = CurrentUser.id;
                        await CoreMethods.PushPageModel<BookViewModel>(CurrentBook);
                    }
                    CurrentCostLevel = null;
                }

                IsBusy = false;
            }
        });

        readonly IUserDialogs dialogs;
        public BrowserViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;
        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando...");

            var musicalGenre = await establishmentAgent.GetRecordsEstablishment();
            var typeEstablishment = await establishmentAgent.GetRecordsEstablishment();
            var costLevel = await establishmentAgent.GetRecordsEstablishment();

            var musicalGenreList = musicalGenre.objectResult.Records;
            var typeEstablishmentList = typeEstablishment.objectResult.Records;
            var costLevelList = costLevel.objectResult.Records;

            if (musicalGenreList != null)
            { 
               var groups = musicalGenreList
                               .GroupBy(x => new { x.MusicalGenre })
                               .Select(x => new MusicalGenre
                               {
                                   Name = x.Key.MusicalGenre,
                                   Logo = x.Key.MusicalGenre == "Rock" ? "rock.png" : x.Key.MusicalGenre == "Techno" ?  "techno.png" : "electronica.png",
                               });

                MusicalGenres = new ObservableCollection<MusicalGenre>(groups);
            }

            if (typeEstablishment != null)
            {
                var groups = typeEstablishmentList
                                .GroupBy(x => new { x.Type })
                                .Select(x => new TypeEstablishment
                                {
                                    Name = x.Key.Type,
                                    Logo = x.Key.Type == "Antro" ? "antro.png" : x.Key.Type == "Bar" ? "bar.png" : "cantabar.png"
                                });

                EstablishmentTypes = new ObservableCollection<TypeEstablishment>(groups);
            }

            if (costLevel != null)
            {
                var groups = costLevelList
                                .GroupBy(x => new { x.CostLevel })
                                .Select(x => new CostLevel
                                {
                                    Name = x.Key.CostLevel,
                                    Logo = x.Key.CostLevel == "Muy Accesible" ? "muyaccesible.png" : x.Key.CostLevel == "Accesible" ? "accesible.png" : "regular.png"
                                });

                CostLevels = new ObservableCollection<CostLevel>(groups);
            }


            dialogs.HideLoading();
        }
    }
}
