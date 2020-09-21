using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace PUL.GS.App.Droid.Renderers
{
    public  class CustomFrameRenderer : FrameRenderer
    {
        GradientDrawable _gradient;
        public CustomFrameRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                Control.SetBackground(DrawGradient(e));
                UpdateCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(CustomFrame.CornerRadius) ||
                e.PropertyName == nameof(CustomFrame))
            {
                UpdateCornerRadius();
            }
        }

        private void UpdateCornerRadius()
        {
            if (Control.Background is GradientDrawable backgroundGradient)
            {
                var cornerRadius = (Element as CustomFrame)?.CornerRadius;
                if (!cornerRadius.HasValue)
                {
                    return;
                }

                var topLeftCorner = Context.ToPixels(cornerRadius.Value.TopLeft);
                var topRightCorner = Context.ToPixels(cornerRadius.Value.TopRight);
                var bottomLeftCorner = Context.ToPixels(cornerRadius.Value.BottomLeft);
                var bottomRightCorner = Context.ToPixels(cornerRadius.Value.BottomRight);

                var cornerRadii = new[]
                {
            topLeftCorner,
            topLeftCorner,

            topRightCorner,
            topRightCorner,

            bottomRightCorner,
            bottomRightCorner,

            bottomLeftCorner,
            bottomLeftCorner,
        };

                backgroundGradient.SetCornerRadii(cornerRadii);
            }
        }


        private GradientDrawable DrawGradient(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            var frame = e.NewElement as CustomFrame;
            var orientation = frame.GradientOrientation == CustomFrame.GradientOrientationStates.Horizontal ?
                GradientDrawable.Orientation.LeftRight : GradientDrawable.Orientation.TopBottom;

            _gradient = new GradientDrawable(orientation, new[] {
                frame.StartColor.ToAndroid().ToArgb(),
                frame.MiddleColor.ToAndroid().ToArgb(),
                frame.EndColor.ToAndroid().ToArgb(),
            });

            //_gradient.SetCornerRadius(frame.CornerRadius * 10);
            _gradient.SetStroke(0, frame.StartColor.ToAndroid());

            return _gradient;
        }
    }
}