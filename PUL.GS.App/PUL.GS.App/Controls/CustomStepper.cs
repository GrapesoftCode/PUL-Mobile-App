using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.Controls
{
    public class CustomStepper: StackLayout
    {
        Button PlusBtn;
        Button MinusBtn;
        Entry Entry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 1,
              defaultBindingMode: BindingMode.TwoWay);

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public CustomStepper()
        {
            MinusBtn = new Button { Text = "-", FontAttributes = FontAttributes.Bold, FontSize = 14, HeightRequest = 30, WidthRequest= 30, TextColor = Color.FromHex("#e62025") };
            PlusBtn = new Button { Text = "+", FontAttributes = FontAttributes.Bold, FontSize = 14, HeightRequest = 30, WidthRequest = 30, TextColor = Color.FromHex("#e62025") };
            
            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        MinusBtn.BackgroundColor = Color.Transparent;
                        PlusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
                case Device.iOS:
                    {
                        MinusBtn.BackgroundColor = Color.Transparent;
                        PlusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
            }

            Orientation = StackOrientation.Horizontal;
            MinusBtn.Clicked += MinusBtn_Clicked;
            PlusBtn.Clicked += PlusBtn_Clicked;
            Entry = new Entry
            {
                PlaceholderColor = Color.Gray,
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                WidthRequest = 20,
                Margin = 0,
                BackgroundColor = Color.Transparent,
                FontSize = 14,
                FontFamily = "SSPRegular",
                FontAttributes = FontAttributes.None,
                TextColor = Color.FromHex("#121112")
            };
            Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
            //Entry.SetBinding(Entry.HeightProperty, new Binding(nameof(HeightRequest), BindingMode.TwoWay, source: this));
            //Entry.SetBinding(Entry.WidthProperty, new Binding(nameof(WidthRequest), BindingMode.TwoWay, source: this));
            Entry.TextChanged += Entry_TextChanged;
            Children.Add(MinusBtn);
            Children.Add(Entry);
            Children.Add(PlusBtn);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
                this.Text = int.Parse(e.NewTextValue);
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text > 1)
                Text--;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            Text++;
        }

    }
}
