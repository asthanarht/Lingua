using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asthanarht.code.lingua.Contract
{
    public static class LanguageCodes
    {
        /// <summary>
        /// The automatic detect.
        /// </summary>
        public const string AutoDetect = "unk";

        /// <summary>
        /// The chinese simplified.
        /// </summary>
        public const string ChineseSimplified = "zh-Hans";

        /// <summary>
        /// The chinese traditional.
        /// </summary>
        public const string ChineseTraditional = "zh-Hant";

        /// <summary>
        /// The czech.
        /// </summary>
        public const string Czech = "cs";

        /// <summary>
        /// The danish.
        /// </summary>
        public const string Danish = "da";

        /// <summary>
        /// The dutch.
        /// </summary>
        public const string Dutch = "nl";

        /// <summary>
        /// The english.
        /// </summary>
        public const string English = "en";

        /// <summary>
        /// The finnish.
        /// </summary>
        public const string Finnish = "fi";

        /// <summary>
        /// The french.
        /// </summary>
        public const string French = "fr";

        /// <summary>
        /// The german.
        /// </summary>
        public const string German = "de";

        /// <summary>
        /// The greek.
        /// </summary>
        public const string Greek = "el";

        /// <summary>
        /// The hungarian.
        /// </summary>
        public const string Hungarian = "hu";

		// <summary>
		/// The hindi.
		/// </summary>
		public const string Hindi = "hi";

		/// <summary>
		/// The italian.
		/// </summary>
		public const string Italian = "it";

        /// <summary>
        /// The japanese.
        /// </summary>
        public const string Japanese = "ja";

        /// <summary>
        /// The korean.
        /// </summary>
        public const string Korean = "ko";

        /// <summary>
        /// The norwegian.
        /// </summary>
        public const string Norwegian = "nb";

        /// <summary>
        /// The polish.
        /// </summary>
        public const string Polish = "pl";

        /// <summary>
        /// The portuguese.
        /// </summary>
        public const string Portuguese = "pt";

        /// <summary>
        /// The russian.
        /// </summary>
        public const string Russian = "ru";

        /// <summary>
        /// The spanish.
        /// </summary>
        public const string Spanish = "es";

        /// <summary>
        /// The swedish.
        /// </summary>
        public const string Swedish = "sv";

        /// <summary>
        /// The turkish.
        /// </summary>
        public const string Turkish = "tr";

		public static Dictionary<string, string> PopulateLanguageDict = new Dictionary<string, string>()
		{
			{"ENG",LanguageCodes.English},
			{"CZE",LanguageCodes.Czech},
			{"DAN",LanguageCodes.Danish},
			{"DUH",LanguageCodes.Dutch},
			{"FIN",LanguageCodes.Finnish},
			{"FRA",LanguageCodes.French},
			{"GER",LanguageCodes.German},
			{"GRK",LanguageCodes.Greek},
			{"HUN",LanguageCodes.Hungarian},
			{"HIN",LanguageCodes.Hindi},
			{"ITA",LanguageCodes.Italian},
			{"JAP",LanguageCodes.Japanese},
			{"KOR",LanguageCodes.Korean},
			{"NOR",LanguageCodes.Norwegian},
			{"POL",LanguageCodes.Polish},
			{"RUS",LanguageCodes.Russian},
			{"SPN",LanguageCodes.Spanish},
			{"SWE",LanguageCodes.Swedish},
		};
    }
}
