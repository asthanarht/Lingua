// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace asthanarht.code.lingua.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
	public  class Settings:INotifyPropertyChanged 
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

   static Settings settings;

		/// <summary>
		/// Gets or sets the current settings. This should always be used
		/// </summary>
		/// <value>The current.</value>
		public static Settings Current
		{
			get { return settings ?? (settings = new Settings()); }
		}

		const string VisionApiKey = "vision_key";
		readonly string VisionApiKeyDefault = "586a32413e9a4dfaabf51dfc2325e246";
		public string VisionApi
		{
			get { return AppSettings.GetValueOrDefault<string>(VisionApiKey, VisionApiKeyDefault); }
			set
			{
				if (AppSettings.AddOrUpdateValue(VisionApiKey, value))
				{
					OnPropertyChanged();
				}
			}
		}

		const string TranslateClientId = "translate_clientId";
		readonly string TranslateClientIdDefault = "asthanarht";
		public string TranslateClient
		{
			get { return AppSettings.GetValueOrDefault<string>(TranslateClientId, TranslateClientIdDefault); }
			set
			{
				if (AppSettings.AddOrUpdateValue(TranslateClientId, value))
				{
					OnPropertyChanged();
				}
			}
		}

		const string TranslateSecretId = "translate_secretId";
		readonly string TranslateSecretIdDefault = "L+KWvEnrjvILRWy+VbJ0GGaRtccF0KO7vf7mYEDWl0E=";
		public string TranslateSecret
		{
			get { return AppSettings.GetValueOrDefault<string>(TranslateSecretId, TranslateSecretIdDefault); }
			set
			{
				if (AppSettings.AddOrUpdateValue(TranslateSecretId, value))
				{
					OnPropertyChanged();
				}
			}
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName]string name = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		#endregion

	}
}