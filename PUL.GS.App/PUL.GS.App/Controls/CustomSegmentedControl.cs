using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.Controls
{
    public class CustomSegmentedControl : View, IViewContainer<SegmentedControlOption>
	{
		public IList<SegmentedControlOption> Children { get; set; }

		public CustomSegmentedControl()
		{
			Children = new List<SegmentedControlOption>();
		}

		public event ValueChangedEventHandler ValueChanged;

		public delegate void ValueChangedEventHandler(object sender, EventArgs e);

		private string selectedValue;

		public string SelectedValue
		{
			get { return selectedValue; }
			set
			{
				selectedValue = value;
				if (ValueChanged != null)
					ValueChanged(this, EventArgs.Empty);
			}
		}
	}

	public class SegmentedControlOption : View
	{
		public static readonly BindableProperty TextProperty = BindableProperty.Create<SegmentedControlOption, string>(p => p.Text, "");

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public SegmentedControlOption()
		{
		}
	}
}
