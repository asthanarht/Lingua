﻿using asthanarht.code.lingua.Pages;
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
		public MainViewModel(Xamarin.Forms.Page page):base(page)
        {

        }

		ICommand settingCommand;
		public ICommand SettingCommand =>
		settingCommand ?? (settingCommand = new Command(async () => await ExecuteSettingCommandAsync()));

		async Task ExecuteSettingCommandAsync()
		{
			var navPage = new NavigationPage(new Setting());
			//navPage.BarBackgroundColor = Color.FromHex("#F9A050");
			await page.Navigation.PushModalAsync(navPage);
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
