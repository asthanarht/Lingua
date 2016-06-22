using asthanarht.code.lingua.Contract;
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
        private string imageUri;

        public string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; RaisePropertyChanged(); }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(); }
        }


        private Command getOcrTextCommand;
        public Command GetOcrTextCommand
        {
            get
            {
                return getOcrTextCommand ??
                    (getOcrTextCommand = new Command(async () => await ExecuteGetOcrTextCommand(), () => { return !IsBusy; }));
            }
        }

        private async Task ExecuteGetOcrTextCommand()
        {
            try
            {
                var stream = PhotoDetails.GetStream();
                //var ocrResult = await visionService.RecognizeTextAsync(stream);
                this.ImageUri = PhotoDetails.Path;
                //this.Status = ParseOcrResults(ocrResult);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Log text from the given OCR results object.
        /// </summary>
        /// <param name="results">The OCR results.</param>
        protected string ParseOcrResults(OcrResults results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append("Text: ");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
