using asthanarht.code.lingua.Contract;
using asthanarht.code.lingua.Service;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        private ObservableCollection<string> _myCollection;
        public ObservableCollection<string> myCollection
        {
            get
            {
                return _myCollection;
            }
            set
            {
                _myCollection = value;
                RaisePropertyChanged();
            }
        }



        private string _myitem;
        public string myItem
        {
            get
            {
                return _myitem;
            }
            set
            {
                _myitem = value;
                RaisePropertyChanged();
            }
        }

        private string languageSet;
        public string LanguageSet
        {
            get
            {
                return languageSet;
            }
            set
            {
                languageSet = value;
                RaisePropertyChanged();
            }
        }

        public ICommand PickPhotoCommand { get; set; }

        public OCRViewModel()
        {
            this.ImageUri = DefaultPhoto;
            visionService = DependencyService.Get<IVisionService>();
            translateService = DependencyService.Get<ITranslationService>();
            this.myCollection = new ObservableCollection<string>();
            foreach(var lang in LanguageCodes.PopulateLanguageDict)
            {
                _myCollection.Add(lang.Key);
            }

            this.languageSet = "HIN";
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


        private Dictionary<string, string> languagePickerCollection;

        public Dictionary<string, string> LanguagePickerCollection
        {
            get { return languagePickerCollection; }
            set { languagePickerCollection = value; RaisePropertyChanged(); }
        }
        private string translateText;

		public string TranslateText
		{
			get { return translateText; }
			set { translateText = value; RaisePropertyChanged(); }
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
                this.LanguageSet = this.myItem;
				var status = await translateService.TranslateText(this.Status,LanguageCodes.PopulateLanguageDict[this.myItem]);
				this.TranslateText = status;
            }
            catch
            {
                throw new NotImplementedException();
            }
            
        }

        private async Task ExecuteClickPhotoCommand()
        {
            await CrossMedia.Current.Initialize();

			//if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			//{
			//    await App_old.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
			//    return;
			//}

			//var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			//{
			//    Directory = "Sample",
			//    Name = "test.jpg",
			//    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear

			//});

			var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;
            PhotoDetails = file;

            var stream = file.GetStream();
            this.ImageUri = PhotoDetails.Path;

            var ocrResult = await visionService.RecognizeTextAsync(stream);
            this.Status = ParseOcrResults(ocrResult);
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
