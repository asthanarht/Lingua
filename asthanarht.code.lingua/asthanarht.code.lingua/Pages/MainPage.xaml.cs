using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using asthanarht.code.lingua.ViewModel;

namespace asthanarht.code.lingua.Pages
{
    public partial class MainPage : ContentPage
    {
        MainViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            

			BindingContext = vm = new MainViewModel(Navigation);  
			ClickPhoto.GestureRecognizers.Add(new TapGestureRecognizer()
			{
				Command = vm.ClickPhotoCommand
			});

			PickPhoto.GestureRecognizers.Add(new TapGestureRecognizer()
			{
				Command = vm.PickPhotoCommand
			});

			TapGestureRecognizer tapLanguageLabel = new TapGestureRecognizer();
			tapLanguageLabel.Tapped += TapLanguageLabel_Tapped;
			captureOCR.GestureRecognizers.Add(tapLanguageLabel);

			TapGestureRecognizer tapSettingLabel = new TapGestureRecognizer();
			tapSettingLabel.Tapped +=	TapSettingLabel_Tapped;		
			setting.GestureRecognizers.Add(tapSettingLabel);
        }

		void TapLanguageLabel_Tapped(object sender, EventArgs e)
		{
			 vm.OCRTextCommand.Execute(null);
		}

		void TapSettingLabel_Tapped(object sender, EventArgs e)
		{
			vm.SettingCommand.Execute(null);
		}
}
}
