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

        }
    }
}
