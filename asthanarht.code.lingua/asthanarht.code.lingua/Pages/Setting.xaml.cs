using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace asthanarht.code.lingua
{
	public partial class Setting : ContentPage
	{
		SettingViewMode vm;
		public Setting()
		{
			BindingContext = vm =  new SettingViewMode();

			InitializeComponent();

		}
	}
}

