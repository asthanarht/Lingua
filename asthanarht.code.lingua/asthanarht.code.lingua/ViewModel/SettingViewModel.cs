using System;
using System.Threading.Tasks;
using System.Windows.Input;
using asthanarht.code.lingua.Helpers;
using asthanarht.code.lingua.ViewModel;
using Xamarin.Forms;

namespace asthanarht.code.lingua
{
	public class SettingViewMode:BaseViewModel
	{
		public SettingViewMode()
		{
			this.VisionKey = Settings.Current.VisionApi;
			this.TranslateClientId = Settings.Current.TranslateClient;
			this.TranslateSecretId = Settings.Current.TranslateSecret;
		}

		private string visionKey;

		public string VisionKey
		{
			get { return visionKey; }
			set { visionKey = value; RaisePropertyChanged(); }
		}

		private string translateClientId;

		public string TranslateClientId
		{
			get { return translateClientId; }
			set { translateClientId = value; RaisePropertyChanged(); }
		}

		private string translateSecretId;

		public string TranslateSecretId
		{
			get { return translateSecretId; }
			set { translateSecretId = value; RaisePropertyChanged(); }
		}

		ICommand saveSettings;
		public ICommand SaveSettings =>
		saveSettings ?? (saveSettings = new Command(async () => await ExecuteSaveSettingsCommandAsync()));

		async Task ExecuteSaveSettingsCommandAsync()
		{
			 Settings.Current.VisionApi = this.VisionKey ;
			 Settings.Current.TranslateClient = this.TranslateClientId ;
			 Settings.Current.TranslateSecret = this.TranslateSecretId ;
			 //await Task.FromResult(0);
		}

}
}

