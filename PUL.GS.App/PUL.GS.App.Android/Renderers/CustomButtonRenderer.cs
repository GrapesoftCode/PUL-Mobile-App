using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PUL.GS.App.Controls;
using PUL.GS.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace PUL.GS.App.Droid.Renderers
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        GradientDrawable _gradient;
        public CustomButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                Control.Touch -= ButtonTouched;
            }

            if (Control != null)
            {
                try
                {
                    Control.Touch += ButtonTouched;
                    Control.StateListAnimator = new Android.Animation.StateListAnimator();
                    Control.SetBackground(DrawGradient(e));
                }
                catch (Exception ex)
                {
                    // handle exception
                }
            }
        }

        private void ButtonTouched(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            if (e.Event.Action == MotionEventActions.Down)
            {
                _gradient.Alpha = 200;
                Control.SetBackground(_gradient);
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                _gradient.Alpha = 255;
                Control.SetBackground(_gradient);
            }
        }


        private GradientDrawable DrawGradient(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            var button = e.NewElement as CustomButton;
            var orientation = button.GradientOrientation == CustomButton.GradientOrientationStates.Horizontal ?
                GradientDrawable.Orientation.LeftRight : GradientDrawable.Orientation.TopBottom;

            _gradient = new GradientDrawable(orientation, new[] {
                button.StartColor.ToAndroid().ToArgb(),
                button.MiddleColor.ToAndroid().ToArgb(),
                button.EndColor.ToAndroid().ToArgb(),
            });

            _gradient.SetCornerRadius(button.CornerRadius * 10);
            _gradient.SetStroke(0, button.StartColor.ToAndroid());

            return _gradient;
        }
    }
}