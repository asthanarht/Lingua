using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asthanarht.code.lingua.ViewModel;

using Xamarin.Forms;

namespace asthanarht.code.lingua.View
{
    public partial class OCR : ContentPage
    {
        OCRViewModel vm;
             
        public OCR()
        {
			
            InitializeComponent();
            BindingContext = vm = new OCRViewModel();

			ClickPhoto.GestureRecognizers.Add(new TapGestureRecognizer()
			{
				Command=vm.ClickPhotoCommand
			});
            SelectLang.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = vm.TranslateTextCommand
            });
            languagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;

            TapGestureRecognizer tapLanguageLabel = new TapGestureRecognizer();
            tapLanguageLabel.Tapped += TapLanguageLabel_Tapped;
            languageLabel.GestureRecognizers.Add(tapLanguageLabel);
        }

        private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            vm.myItem = languagePicker.Items[languagePicker.SelectedIndex];
            vm.TranslateTextCommand.Execute(vm.myItem);
        }

        private void TapLanguageLabel_Tapped(object sender, EventArgs e)
        {
            languagePicker.Focus();
        }
    }
}
