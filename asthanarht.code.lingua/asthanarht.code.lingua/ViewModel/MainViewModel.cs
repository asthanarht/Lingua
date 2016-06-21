using asthanarht.code.lingua.View;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace asthanarht.code.lingua.ViewModel
{
    public class MainViewModel :BaseViewModel
    {
        Command clickPicCommand;
        public MainViewModel(Page page):base(page)
        {

        }


        public Command ClickPhotoCommand
        {
            get
            {
                return clickPicCommand ?? (clickPicCommand = new Command(async () => await ClickPhoto()));
            }
        }

        private async Task ClickPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
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

            var ocrviewmodel = new OCRViewModel(file);

            await page.Navigation.PushModalAsync(new OCPExtractorPage(ocrviewmodel));
            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;
            //});

        }
    }
}
