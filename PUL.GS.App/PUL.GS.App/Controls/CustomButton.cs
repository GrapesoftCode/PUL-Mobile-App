using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.Controls
{
    public class CustomButton : Button
    {
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CustomButton), typeof(CornerRadius), typeof(CustomButton));
        public CustomButton()
        {
            // MK Clearing default values (e.g. on iOS it's 5)
            base.CornerRadius = 0;
        }

        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }


        #region enum
        // specifies the orientation of the gradient color
        public enum GradientOrientationStates
        {
            Vertical,
            Horizontal
        }
        #endregion

        #region bindable properties
        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(
                nameof(StartColor),
                typeof(Color),
                typeof(CustomButton),
                default(Color));

        public static readonly BindableProperty MiddleColorProperty =
            BindableProperty.Create(
                nameof(MiddleColor),
                typeof(Color),
                typeof(CustomButton),
                default(Color));

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(
                nameof(EndColor),
                typeof(Color),
                typeof(CustomButton),
                default(Color));

        public static readonly BindableProperty GradientOrientationProperty =
            BindableProperty.Create(
                nameof(GradientOrientation),
                typeof(GradientOrientationStates),
                typeof(CustomButton),
                default(GradientOrientationStates));
        #endregion

        #region properties
        /// <summary>
        /// The start color of the gradient background
        /// </summary>
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        /// <summary>
        /// The start color of the gradient background
        /// </summary>
        public Color MiddleColor
        {
            get => (Color)GetValue(MiddleColorProperty);
            set => SetValue(MiddleColorProperty, value);
        }

        /// <summary>
        ///  The end color of the gradient background
        /// </summary>
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        /// <summary>
        ///  The gradient color orientation
        /// </summary>
        public GradientOrientationStates GradientOrientation
        {
            get => (GradientOrientationStates)GetValue(GradientOrientationProperty);
            set => SetValue(GradientOrientationProperty, value);
        }
        #endregion
    }
}
