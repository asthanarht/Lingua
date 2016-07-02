using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using asthanarht.code.lingua.Contract;
using asthanarht.code.lingua.Service;
using Xamarin.Forms;
namespace asthanarht.code.lingua.ViewModel
{
	public class OCRDetailViewModel:BaseViewModel
	{
		readonly ITranslationService translateService;

		private ObservableCollection<string> _languageCollection;
		public ObservableCollection<string> LanguageCollection
		{
			get
			{
				return _languageCollection;
			}
			set
			{
				_languageCollection = value;
				RaisePropertyChanged();
			}
		}



		private string _selectedLanguage;
		public string SelectedLanguage
		{
			get
			{
				return _selectedLanguage;
			}
			set
			{
				_selectedLanguage = value;
				RaisePropertyChanged();
			}
		}


		public OCRDetailViewModel(ImageSource photoLocation,string OCRText)
		{
			this.PhotoUrl = photoLocation;
			this.OCRText = OCRText;
			this.TranslatedText = "Please Select Translated Language.";
			translateService = DependencyService.Get<ITranslationService>();
			this.LanguageCollection = new ObservableCollection<string>();
			foreach (var lang in LanguageCodes.PopulateLanguageDict)
			{
				LanguageCollection.Add(lang.Key);
			}
			this.SelectedLanguage = "Hin";
			this.TranslatedText = this.OCRText;
		}

		private ImageSource photoUrl;

		public ImageSource PhotoUrl
		{
			get { return photoUrl; }
			set { photoUrl = value; RaisePropertyChanged(); }
		}

		private string ocrText;

		public string OCRText
		{
			get { return ocrText; }
			set { ocrText = value; RaisePropertyChanged(); }
		}

		private string translatedText;

		public string TranslatedText
		{
			get { return translatedText; }
			set { translatedText = value; RaisePropertyChanged(); }
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
				this.TranslatedText = this.SelectedLanguage;
				var status = await translateService.TranslateText(this.OCRText, LanguageCodes.PopulateLanguageDict[this.SelectedLanguage]);
				this.TranslatedText = status;
			}
			catch
			{
				throw new NotImplementedException();
			}

		}


	}
}

