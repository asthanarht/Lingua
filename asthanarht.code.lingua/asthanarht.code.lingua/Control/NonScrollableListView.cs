using System;
using Xamarin.Forms;
namespace asthanarht.code.lingua
{
	public class NonScrollableListView:ListView
	{
		public NonScrollableListView(): base(ListViewCachingStrategy.RecycleElement)
		{
			if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
				BackgroundColor = Color.White;
		}
	}
}

