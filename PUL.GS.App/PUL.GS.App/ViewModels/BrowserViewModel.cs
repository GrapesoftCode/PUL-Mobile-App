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

        public User CurrentUser { get; set; }
        public Establishment CurrentEstablishment { get; set; }
        public Promotion CurrentPromotion { get; set; }
        public Combo CurrentCombo { get; set; }
        public Book CurrentBook { get; set; } = new Book();

        public ICommand EstablishmentCommand { get; set; }

        

        readonly IUserDialogs dialogs;
        public BrowserViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            EstablishmentCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentEstablishment != null)
                    {
                        CurrentBook.User = CurrentUser;
                        CurrentBook.Establishment = CurrentEstablishment;
                        CurrentBook.establishmentId = CurrentEstablishment.id;
                        CurrentBook.User = CurrentUser;
                        CurrentBook.userId = CurrentUser.id;
                        await CoreMethods.PushPageModel<BookViewModel>(CurrentBook);
                        CurrentEstablishment = null;
                    }

                    IsBusy = false;
                }
            });
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
                                   Logo = x.Key.MusicalGenre == "Rock" ? "rock.png" : x.Key.MusicalGenre == "Techno" ?  "techno.png" : "electronica.png"
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
