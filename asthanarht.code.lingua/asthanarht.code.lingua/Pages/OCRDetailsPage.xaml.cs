using System;
using System.Collections.Generic;
using asthanarht.code.lingua.ViewModel;
using Xamarin.Forms;

namespace asthanarht.code.lingua
{
	public partial class OCRDetailsPage : ContentPage
	{
		OCRDetailViewModel vm;
		public OCRDetailsPage(ImageSource image, string OCRText)
		{
			InitializeComponent();
			MainScroll.ParallaxView = HeaderView;
			BindingContext = vm = new OCRDetailViewModel(image, OCRText);

			languagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;
			TapGestureRecognizer tapLanguageLabel = new TapGestureRecognizer();
			tapLanguageLabel.Tapped += TapLanguageLabel_Tapped;
			translateGrid.GestureRecognizers.Add(tapLanguageLabel);
			if (Device.Idiom != TargetIdiom.Phone)
				Row1Header.Height = Row1Content.Height = 350;
		}

		void TapLanguageLabel_Tapped(object sender, EventArgs e)
		{
			languagePicker.Focus();
		}

		private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			vm.SelectedLanguage = languagePicker.Items[languagePicker.SelectedIndex];
			vm.TranslateTextCommand.Execute(vm.SelectedLanguage);
		}
	}
}

