using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.Controls
{
    public class MenuStepper : StackLayout
    {
        Button PlusBtn;
        Button MinusBtn;
        Entry Entry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 0,
              defaultBindingMode: BindingMode.TwoWay);

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public MenuStepper()
        {
            MinusBtn = new Button { Text = "-", WidthRequest = 35, HeightRequest = 30, FontAttributes = FontAttributes.Bold, FontSize = 15,TextColor = Color.OrangeRed, BackgroundColor = Color.Transparent };
            PlusBtn = new Button { Text = "+", WidthRequest = 35, HeightRequest = 30, FontAttributes = FontAttributes.Bold, FontSize = 15, TextColor = Color.OrangeRed, BackgroundColor = Color.Transparent };

            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        MinusBtn.BackgroundColor = Color.Transparent;
                        MinusBtn.BorderColor = Color.OrangeRed;
                        PlusBtn.BackgroundColor = Color.Transparent;
                        PlusBtn.BorderColor = Color.OrangeRed;
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
                WidthRequest = 35,
                HeightRequest = 35,
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Transparent
            };
            Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
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
            if (Text > 0)
                Text--;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            Text++;
        }

    }
}
