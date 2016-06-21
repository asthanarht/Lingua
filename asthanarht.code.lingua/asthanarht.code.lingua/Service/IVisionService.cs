using asthanarht.code.lingua.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asthanarht.code.lingua.Service
{
    public interface IVisionService
    {
        Task<string> OCRService(string path);
        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="detectOrientation">if set to <c>true</c> [detect orientation].</param>
        /// <returns>The OCR object.</returns>
        Task<OcrResults> RecognizeTextAsync(Stream imageStream, string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true);

    }
}
