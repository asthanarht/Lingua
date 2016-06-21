using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asthanarht.code.lingua.Contract;
using System.Net;
using Newtonsoft.Json;
using Xamarin.Forms;
using asthanarht.code.lingua.Service;

[assembly: Dependency(typeof(VisionService))]
namespace asthanarht.code.lingua.Service
{
    public class VisionService : IVisionService
    {
        /// <summary>
        /// The service host
        /// </summary>
        private const string SERVICE_HOST = "https://api.projectoxford.ai/vision/v1.0";

        /// <summary>
        /// The subscription key name
        /// </summary>
        private const string _subscriptionKeyName = "subscription-key";

        private const string _subscriptionKey="586a32413e9a4dfaabf51dfc2325e246";

        public Task<string> OCRService(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<OcrResults> RecognizeTextAsync(Stream imageStream, string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true)
        {
            string requestUrl = string.Format("{0}/ocr?language={1}&detectOrientation={2}&{3}={4}", SERVICE_HOST, languageCode, detectOrientation, _subscriptionKeyName, _subscriptionKey);
            var request = WebRequest.Create(requestUrl);

            return await this.SendAsync<Stream, OcrResults>("POST", imageStream, request);

        }

        /// <summary>
        /// Set request content type.
        /// </summary>
        /// <param name="request">Web request object.</param>
        private void SetCommonHeaders(WebRequest request)
        {
            request.ContentType = "application/json";
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="requestBody">The request body.</param>
        /// <param name="request">The request.</param>
        /// <param name="setHeadersCallback">The set headers callback.</param>
        /// <returns>The response object.</returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        private async Task<TResponse> SendAsync<TRequest, TResponse>(string method, TRequest requestBody, WebRequest request, Action<WebRequest> setHeadersCallback = null)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request");
                }

                request.Method = method;
                if (null == setHeadersCallback)
                {
                    this.SetCommonHeaders(request);
                }
                else
                {
                    setHeadersCallback(request);
                }

                if (requestBody is Stream)
                {
                    request.ContentType = "application/octet-stream";
                }

                var asyncState = new WebRequestAsyncState()
                {
                    RequestBytes = this.SerializeRequestBody(requestBody),
                    WebRequest = (HttpWebRequest)request,
                };

                var continueRequestAsyncState = await Task.Factory.FromAsync<Stream>(
                                                    asyncState.WebRequest.BeginGetRequestStream,
                                                    asyncState.WebRequest.EndGetRequestStream,
                                                    asyncState,
                                                    TaskCreationOptions.None).ContinueWith<WebRequestAsyncState>(
                                                       task =>
                                                       {
                                                           var requestAsyncState = (WebRequestAsyncState)task.AsyncState;
                                                           if (requestBody != null)
                                                           {
                                                               using (var requestStream = task.Result)
                                                               {
                                                                   if (requestBody is Stream)
                                                                   {
                                                                       (requestBody as Stream).CopyTo(requestStream);
                                                                   }
                                                                   else
                                                                   {
                                                                       requestStream.Write(requestAsyncState.RequestBytes, 0, requestAsyncState.RequestBytes.Length);
                                                                   }
                                                               }
                                                           }

                                                           return requestAsyncState;
                                                       });

                var continueWebRequest = continueRequestAsyncState.WebRequest;
                var response = await Task.Factory.FromAsync<WebResponse>(
                                            continueWebRequest.BeginGetResponse,
                                            continueWebRequest.EndGetResponse,
                                            continueRequestAsyncState);

                return this.ProcessAsyncResponse<TResponse>(response as HttpWebResponse);
            }
            catch (Exception e)
            {
                return default(TResponse);
            }
        }

        /// <summary>
        /// Serialize the request body to byte array.
        /// </summary>
        /// <typeparam name="T">Type of request object.</typeparam>
        /// <param name="requestBody">Strong typed request object.</param>
        /// <returns>Byte array.</returns>
        private byte[] SerializeRequestBody<T>(T requestBody)
        {
            if (requestBody == null || requestBody is Stream)
            {
                return null;
            }
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                return System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody, settings));
            }
        }

        /// <summary>
        /// Processes the asynchronous response.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="webResponse">The web response.</param>
        /// <returns>The response.</returns>
        private T ProcessAsyncResponse<T>(HttpWebResponse webResponse)
        {
            using (webResponse)
            {
                if (webResponse.StatusCode == HttpStatusCode.OK ||
                    webResponse.StatusCode == HttpStatusCode.Accepted ||
                    webResponse.StatusCode == HttpStatusCode.Created)
                {
                    if (webResponse.ContentLength != 0)
                    {
                        using (var stream = webResponse.GetResponseStream())
                        {
                            if (stream != null)
                            {
                                if (webResponse.ContentType == "image/jpeg" ||
                                    webResponse.ContentType == "image/png")
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        stream.CopyTo(ms);
                                        return (T)(object)ms.ToArray();
                                    }
                                }
                                else
                                {
                                    string message = string.Empty;
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        message = reader.ReadToEnd();
                                    }

                                    JsonSerializerSettings settings = new JsonSerializerSettings();
                                    settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                                    settings.NullValueHandling = NullValueHandling.Ignore;

                                    return JsonConvert.DeserializeObject<T>(message, settings);
                                }
                            }
                        }
                    }
                }
            }

            return default(T);
        }
        internal class WebRequestAsyncState
        {
            /// <summary>
            /// Gets or sets request bytes of the request parameter for http post.
            /// </summary>
            public byte[] RequestBytes { get; set; }

            /// <summary>
            /// Gets or sets the HttpWebRequest object.
            /// </summary>
            public HttpWebRequest WebRequest { get; set; }

            /// <summary>
            /// Gets or sets the request state object.
            /// </summary>
            public object State { get; set; }
        }

    }
}
