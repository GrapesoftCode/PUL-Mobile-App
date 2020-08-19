using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class MenuViewModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }

        private readonly MenuData menuAgent;
        public ObservableCollection<Category> Categories { get; set; }
       
        public Establishment CurrentEstablishment { get; set; }
        public Book CurrentBook { get; set; } = new Book();

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };
        readonly IUserDialogs dialogs;
        public MenuViewModel(IUserDialogs _dialogs)
        {
            dialogs = _dialogs;
            menuAgent = new MenuData(appSettings);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentBook = initData as Book;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            var listCategories = menuAgent.GetCategoriesFood(CurrentBook.Establishment.id).objectResult;

            Categories = new ObservableCollection<Category>(listCategories);

            dialogs.HideLoading();
        }
    }

    class NamedMenuPage : ContentPage
    {
        public ObservableCollection<Menu> Menus { get; set; }
        private readonly MenuData menuAgent;

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        public NamedMenuPage()
        {
            menuAgent = new MenuData(appSettings);
            this.SetBinding(ContentPage.TitleProperty, "Name");
            Menus = new ObservableCollection<Menu>(menuAgent.GetListFoodsByCategory("38da4008-8168-4276-ada3-30bd3ede6381", "Tacos").objectResult);

            CollectionView collectionView = new CollectionView();
            collectionView.ItemsSource = Menus;
            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid { Padding = 10 };
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                Image image = new Image { Aspect = Aspect.AspectFill, HeightRequest = 60, WidthRequest = 60 };
                image.SetBinding(Image.SourceProperty, "Logo.Uri");

                Label nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label locationLabel = new Label { FontAttributes = FontAttributes.Italic, VerticalOptions = LayoutOptions.End };
                locationLabel.SetBinding(Label.TextProperty, "Description");

                Grid.SetRowSpan(image, 2);

                grid.Children.Add(image);
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(locationLabel, 1, 1);

                return grid;

            });

            Content = collectionView;
        }
    }
}
