using asthanarht.code.lingua.Contract;
using asthanarht.code.lingua.Service;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace asthanarht.code.lingua.ViewModel
{
    public class OCRViewModel:BaseViewModel
    {
        static readonly ImageSource DefaultPhoto = ImageSource.FromFile("defaultplaceholder.png");
        public MediaFile PhotoDetails { get; set; }
        readonly IVisionService visionService;
        readonly ITranslationService translateService;

        public ICommand PickPhotoCommand { get; set; }

        public OCRViewModel()
        {
            this.ImageUri = DefaultPhoto;
            visionService = DependencyService.Get<IVisionService>();
            translateService = DependencyService.Get<ITranslationService>();
        }
        private ImageSource imageUri;

        public ImageSource ImageUri
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

        private Command clickPhotoCommand;
        public Command ClickPhotoCommand
        {
            get
            {
                return clickPhotoCommand ??
                    (clickPhotoCommand = new Command(async () => await ExecuteClickPhotoCommand(), () => { return !IsBusy; }));
            }
        }

        private Command translateTextCommand;
        public Command TranslateTextCommand
        {
            get
            {
                return translateTextCommand ??
                    (translateTextCommand = new Command(async () => await ExecuteTranslateTextCommand(), () => { return !IsBusy; }));
            }
        }

        private async Task ExecuteTranslateTextCommand()
        {
            try
            {
                var status = await translateService.TranslateText("This is my Text");
                this.Status = status;
            }
            catch
            {
                throw new NotImplementedException();
            }
            
        }

        private async Task ExecuteClickPhotoCommand()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App_old.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear

            });


            if (file == null)
                return;
            PhotoDetails = file;

            var stream = file.GetStream();
            this.ImageUri = PhotoDetails.Path;
        }


        private async Task ExecuteGetOcrTextCommand()
        {
            try
            {
                var stream = PhotoDetails.GetStream();
                //var ocrResult = await visionService.RecognizeTextAsync(stream);
                this.Status = "This is my Text";
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
