using asthanarht.code.lingua.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asthanarht.code.lingua.Service
{
    public interface ITranslationService
    {
        Task<string> TranslateText( string TextToTranslate, string LanguageCode = LanguageCodes.French);

    }
}
