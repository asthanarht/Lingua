using asthanarht.code.lingua.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace asthanarht.code.lingua.View
{
    public partial class OCR : ContentPage
    {
        OCRViewModel vm;
        public OCR(OCRViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = vm = viewModel;
            vm.GetOcrTextCommand.Execute(null);
        }
    }
}
