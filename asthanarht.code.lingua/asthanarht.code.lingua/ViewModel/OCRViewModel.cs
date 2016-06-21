using asthanarht.code.lingua.Service;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace asthanarht.code.lingua.ViewModel
{
    public class OCRViewModel:BaseViewModel
    {
        public MediaFile PhotoDetails { get; set; }
        readonly IVisionService visionService;
        public OCRViewModel(MediaFile photoFile)
        {
            this.PhotoDetails = photoFile;
            visionService = DependencyService.Get<IVisionService>();
        }



    }
}
