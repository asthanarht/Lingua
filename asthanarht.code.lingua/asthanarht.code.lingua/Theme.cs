using System;
using Xamarin.Forms;

namespace asthanarht.code.lingua
{
	public static class Theme
	{
		private static readonly string primaryColor = "#512DA8";
		private static readonly string primaryColorDark = "#E64A19";
		private static readonly string dividerColor = "#B6B6B6";
		private static readonly string textColor = "#727272";
		private static readonly string textColorDark = "#212121";
		private static readonly string textAndIconsColor = "#FFFFFF";
		public static Color PrimaryColor { get { return Color.FromHex(primaryColor); } }
		public static Color PrimaryColorDark { get { return Color.FromHex(primaryColorDark); } }
		public static Color DividerColor { get { return Color.FromHex(dividerColor); } }
		public static Color TextColor { get { return Color.FromHex(textColor); } }
		public static Color TextColorDark { get { return Color.FromHex(textColorDark); } }
		public static Color TextAndIconsColor { get { return Color.FromHex(textAndIconsColor); } }

	}

}

