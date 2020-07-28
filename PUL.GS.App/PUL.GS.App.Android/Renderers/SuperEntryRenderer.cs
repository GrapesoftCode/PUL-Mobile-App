using System;

using Android.Graphics;
using Android.Graphics.Drawables;
using PUL.GS.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // El tipo o el miembro están obsoletos
[assembly: ExportRenderer(typeof(Entry), typeof(SuperEntryRenderer))]
#pragma warning restore CS0612 // El tipo o el miembro están obsoletos
namespace PUL.GS.App.Droid.Renderers
{
    [Obsolete]
    public class SuperEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = Xamarin.Forms.Color.Red.ToAndroid();
                shape.Paint.SetStyle(Paint.Style.Stroke);
                nativeEditText.Background = shape;
            }
        }
    }
}