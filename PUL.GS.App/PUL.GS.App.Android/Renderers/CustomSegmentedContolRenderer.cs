﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PUL.GS.App.Controls;
using PUL.GS.App.Droid.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSegmentedControl), typeof(CustomSegmentedContolRenderer))]
namespace PUL.GS.App.Droid.Renderers
{
    public class CustomSegmentedContolRenderer : ViewRenderer<CustomSegmentedControl, RadioGroup>
	{
        public CustomSegmentedContolRenderer()
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<CustomSegmentedControl> e)
		{
			base.OnElementChanged(e);

			var layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);

			var g = new RadioGroup(Context);
			g.Orientation = Orientation.Horizontal;
			g.CheckedChange += (sender, eventArgs) => {
				var rg = (RadioGroup)sender;
				if (rg.CheckedRadioButtonId != -1)
				{
					var id = rg.CheckedRadioButtonId;
					var radioButton = rg.FindViewById(id);
					var radioId = rg.IndexOfChild(radioButton);
					var btn = (Android.Widget.RadioButton)rg.GetChildAt(radioId);
					var selection = (String)btn.Text;
					e.NewElement.SelectedValue = selection;
				}
			};

			for (var i = 0; i < e.NewElement.Children.Count; i++)
			{
				var o = e.NewElement.Children[i];
				var v = (SegmentedControlButton)layoutInflater.Inflate(Resource.Layout.SegmentedControl, null);
				v.Text = o.Text;
				if (i == 0)
					v.SetBackgroundResource(Resource.Drawable.segmented_control_first_background);
				else if (i == e.NewElement.Children.Count - 1)
					v.SetBackgroundResource(Resource.Drawable.segmented_control_last_background);
				g.AddView(v);
			}

			SetNativeControl(g);
		}
	}

	public class SegmentedControlButton : Xamarin.Forms.RadioButton
    {
		private int lineHeightSelected;
		private int lineHeightUnselected;

		private Paint linePaint;

		public SegmentedControlButton(Context context) : this(context, null)
		{
		}

		public SegmentedControlButton(Context context, IAttributeSet attributes) : this(context, attributes, Resource.Attribute.segmentedControlOptionStyle)
		{
		}

		public SegmentedControlButton(Context context, IAttributeSet attributes, int defStyle) : base(context, attributes, defStyle)
		{
			Initialize(attributes, defStyle);
		}

		private void Initialize(IAttributeSet attributes, int defStyle)
		{
			var a = this.Context.ObtainStyledAttributes(attributes, Resource.Styleable.SegmentedControlOption, defStyle, Resource.Style.SegmentedControlOption);

			var lineColor = a.GetColor(Resource.Styleable.SegmentedControlOption_lineColor, 0);
			linePaint = new Paint();
			linePaint.Color = lineColor;

			lineHeightUnselected = a.GetDimensionPixelSize(Resource.Styleable.SegmentedControlOption_lineHeightUnselected, 0);
			lineHeightSelected = a.GetDimensionPixelSize(Resource.Styleable.SegmentedControlOption_lineHeightSelected, 0);

			a.Recycle();
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			if (linePaint.Color != 0 && (lineHeightSelected > 0 || lineHeightUnselected > 0))
			{
				var lineHeight = Checked ? lineHeightSelected : lineHeightUnselected;

				if (lineHeight > 0)
				{
					var rect = new Rect(0, Height - lineHeight, Width, Height);
					canvas.DrawRect(rect, linePaint);
				}
			}
		}
	}
}