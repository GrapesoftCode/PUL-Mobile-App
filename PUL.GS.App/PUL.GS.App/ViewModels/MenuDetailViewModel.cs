using Android.Views;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System.Collections.ObjectModel;
using Menu = PUL.GS.Models.Menu;

namespace PUL.GS.App.ViewModels
{
    public class MenuDetailViewModel
    {
        public ObservableCollection<Menu> Menus { get; set; }
        public string CurrentName { get; set; }
        public string CurrentEstablismentId { get; set; }

        private readonly MenuData menuAgent;

        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };
        public MenuDetailViewModel(string tabName, string establishmentId)
        {
            menuAgent = new MenuData(appSettings);
            CurrentName = tabName;
            CurrentEstablismentId = establishmentId;

            var listFoodsByCategory = menuAgent.GetListFoodsByCategory(CurrentEstablismentId, CurrentName).objectResult;
            Menus = new ObservableCollection<Menu>(listFoodsByCategory);

            //CollectionView collectionView = new CollectionView();
            //collectionView.ItemsSource = Menus;
            //collectionView.ItemTemplate = new DataTemplate(() =>
            //{
            //    Grid grid = new Grid { Padding = 10 };
            //    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            //    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            //    Image image = new Image { Aspect = Aspect.AspectFill, HeightRequest = 60, WidthRequest = 60 };
            //    image.SetBinding(Image.SourceProperty, "Logo.Uri");

            //    Label nameLabel = new Label { FontAttributes = FontAttributes.Bold };
            //    nameLabel.SetBinding(Label.TextProperty, "Name");

            //    Label locationLabel = new Label { FontAttributes = FontAttributes.Italic, VerticalOptions = LayoutOptions.End };
            //    locationLabel.SetBinding(Label.TextProperty, "Description");

            //    Grid.SetRowSpan(image, 2);

            //    grid.Children.Add(image);
            //    grid.Children.Add(nameLabel, 1, 0);
            //    grid.Children.Add(locationLabel, 1, 1);

            //    return grid;

            //});

            //this.Content = collectionView;
        }
    }

}
