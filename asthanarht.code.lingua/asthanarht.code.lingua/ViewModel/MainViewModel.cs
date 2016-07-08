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

						var navPage = new NavigationPage(new OCRDetailsPage(this.ImageUri, t));
						//navPage.BarBackgroundColor = Color.FromHex("#F9A050");
						await navigation.PushAsync(navPage);

					
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
			var navPage = new NavigationPage(new Setting());
			//navPage.BarBackgroundColor = Color.FromHex("#F9A050");
			await navigation.PushAsync(navPage);
		}

		public ICommand PickPhotoCommand { get; set; }

		public ICommand OCRTextCommand { get; set; }

		private async Task ClickPhoto()
        {
            //await CrossMedia.Current.Initialize();

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

            
            //if (file == null)
            //    return;

           

            var navPage = new NavigationPage(new OCR());
            navPage.BarBackgroundColor = Color.FromHex("#F9A050");
            await page.Navigation.PushModalAsync(navPage);
            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;
            //});

        }
    }
}
