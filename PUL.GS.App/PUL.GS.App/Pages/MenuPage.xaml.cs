using PUL.GS.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace PUL.GS.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            InitializeComponent();

            //this.Title = "Menú";

            //this.ItemsSource = new NamedColor[] {
            //    new NamedColor ("Cerveza", Color.Red),
            //    new NamedColor ("Refresco", Color.Yellow),
            //    new NamedColor ("Vino", Color.Green),
            //    new NamedColor ("Tequila", Color.Aqua),
            //    new NamedColor ("Vodka", Color.Blue),
            //    new NamedColor ("Purple", Color.Purple)
            //};

            this.ItemTemplate = new DataTemplate(() =>
            {
                return new NamedMenuPage();
            });
        }
    }


    //class NamedColor
    //{
    //    public NamedColor(string name, Color color)
    //    {
    //        this.Name = name;
    //        this.Color = color;
    //    }

    //    public string Name { private set; get; }

    //    public Color Color { private set; get; }

    //    public override string ToString()
    //    {
    //        return Name;
    //    }
    //}

    //class NamedColorPage : ContentPage
    //{
    //    public NamedColorPage()
    //    {
    //        // This binding is necessary to label the tabs in
    //        // the TabbedPage.
    //        this.SetBinding(ContentPage.TitleProperty, "Name");

    //        // BoxView to show the color.
    //        BoxView boxView = new BoxView
    //        {
    //            WidthRequest = 100,
    //            HeightRequest = 100,
    //            HorizontalOptions = LayoutOptions.Center,
    //            VerticalOptions = LayoutOptions.Center
    //        };

    //        boxView.SetBinding(BoxView.ColorProperty, "Color");

    //        // Build the page
    //        this.Content = boxView;
    //    }
    //}
}