using asthanarht.code.lingua.Pages;
using asthanarht.code.lingua.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace asthanarht.code.lingua
{
    public partial class App : Application
    {

		public static Size ScreenSize { get; set; }
        public static App current;
        public App()
        {
            current = this;
            InitializeComponent();
			MainPage = new NavigationPage(new MainPage());

			var screen = DependencyService.Get<IDisplay>();
			if (screen != null)
			{
				ScreenSize = screen.Size;
			}
			else {
				ScreenSize = new Size(300, 600);
			}
        }
    }

	public interface IDisplay
	{
		Size Size { get; }
	}
}
