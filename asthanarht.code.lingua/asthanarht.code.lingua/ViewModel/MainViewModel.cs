using asthanarht.code.lingua.Pages;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media.Abstractions;

namespace asthanarht.code.lingua.ViewModel
{
    public class MainViewModel :BaseViewModel
    {
		INavigation navigation;
		static readonly ImageSource DefaultPhoto = ImageSource.FromFile("defaultplaceholder.png");
		public MediaFile PhotoDetails { get; set; }
        Command clickPicCommand;
		public MainViewModel(INavigation navigation)
        {
			this.ImageUri = DefaultPhoto;

			this.navigation = navigation;
			PickPhotoCommand = new Command(async () =>
			{
				PhotoDetails = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
				if (PhotoDetails != null)
					ImageUri = ImageSource.FromFile(PhotoDetails.Path);
			});

			OCRTextCommand = new Command(async () =>
			{

				if (ImageUri != null)
				{
					try
					{
						var stream = PhotoDetails.GetStream();
						var ocrResult = await Task.FromResult(0);// visionService.RecognizeTextAsync(stream);

						var t = "Implementation options are multiple. I typically keep it simple, using fields in a static class. Note that you could have a message class for each type that will display messages, or you could have a central message class where you group multiple classes. You could have nested message groups. You could also add other types of constants for use in your code... As I mentioned, options and preferences abound.\n\n";


						await navigation.PushAsync(new OCRDetailsPage(this.ImageUri, t));

					
						//this.Status = ParseOcrResults(ocrResult);
					}
					catch (Exception ex)
					{
					}

				}

			});

		}

		private ImageSource imageUri;

		public ImageSource ImageUri
		{
			get { return imageUri; }
			set { imageUri = value; RaisePropertyChanged(); }
		}

		ICommand settingCommand;
		public ICommand SettingCommand =>
		settingCommand ?? (settingCommand = new Command(async () => await ExecuteSettingCommandAsync()));

		async Task ExecuteSettingCommandAsync()
		{
			
			//navPage.BarBackgroundColor = Color.FromHex("#F9A050");
			await navigation.PushAsync(new Setting());
		}

		public ICommand PickPhotoCommand { get; set; }

		public ICommand OCRTextCommand { get; set; }

		ICommand clickPhotoCommand;
		public ICommand ClickPhotoCommand =>
		clickPhotoCommand ?? (clickPhotoCommand = new Command(async () => await ClickPhoto()));

		private async Task ClickPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
				await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            PhotoDetails = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "OCR",
                Name = "OCR.jpg",
               DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear
                
            });

            
            if (PhotoDetails == null)
             return;
			if (PhotoDetails != null)
				ImageUri = ImageSource.FromFile(PhotoDetails.Path);
           


        }
    }
}
