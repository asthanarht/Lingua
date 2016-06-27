using asthanarht.code.lingua.Contract;
using asthanarht.code.lingua.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(TranslationService))]
namespace asthanarht.code.lingua.Service
{
    public class AdmAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
    }


    public class TranslationService : ITranslationService
    {
        private async Task<string> AccessToken()
        {

            string clientID = "asthanarht";
            string clientSecret = "L+KWvEnrjvILRWy+VbJ0GGaRtccF0KO7vf7mYEDWl0E=";
            String strTranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strTranslatorAccessURI);
                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                postData.Add(new KeyValuePair<string, string>("client_id", clientID));
                postData.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
                postData.Add(new KeyValuePair<string, string>("scope", "http://api.microsofttranslator.com"));
                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                // Post to the Server and parse the response.
                try
                {
                    var response = await client.PostAsync(strTranslatorAccessURI, content);
                    response.EnsureSuccessStatusCode();
                    string jsonString = response.Content.ReadAsStringAsync().Result;

                    //object responseData = JsonConvert.DeserializeObject(jsonString);
                    var responseData = JsonConvert.DeserializeObject<AdmAccessToken>(jsonString);

                    return "Bearer " + responseData.access_token;


                    // return the Access Token.
                    //return responseData.ToString();
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public async Task<string> TranslateText(string TextToTranslate, string LanguageCode = LanguageCodes.Hindi)
        {
            var token = await AccessToken();

            string uri ="http://api.microsofttranslator.com";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization",token);

                // create the URL string.
                string url = string.Format("v2/Http.svc/Translate?text={0}&to={1}", TextToTranslate, LanguageCode);

                // make the request
                HttpResponseMessage response = await client.GetAsync(url);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();

                System.Xml.Linq.XDocument xTranslation = System.Xml.Linq.XDocument.Parse(jsonString);

                var translatedString = xTranslation.Root.Value;

                return translatedString;
            }
        

        }
    }
}
