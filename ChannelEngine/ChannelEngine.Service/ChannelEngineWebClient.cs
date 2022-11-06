using ChanelEngine.Service.Interfaces;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ChanelEngine.Service
{
    public class ChannelEngineWebClient : IChannelEngineWebClient
    {
        public async Task<string> PostAsync(string endpoint, string content)
        {
            var request = GetRequestObject(endpoint);
            request.Method = "POST";
            request.ContentLength = content.Length;

            await WriteApiRequestAsync(request, content);

            return await GetApiWebResponseAsync(request);
        }
        public async Task<string> GetAsync(string endpoint)
        {
            var request = GetRequestObject(endpoint);
            request.Method = "GET";

            return await GetApiWebResponseAsync(request);
        }

        private async Task WriteApiRequestAsync(HttpWebRequest request, string content)
        {
            using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                await streamWriter.WriteAsync(content);
            }
        }

        private HttpWebRequest GetRequestObject(string endpoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Headers.Add("Accept-Charset", "utf-8");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Content-Type", "application/json");
            request.CookieContainer = new CookieContainer();
            request.Expect = "application/json";

            return request;
        }

        private async Task<string> GetApiWebResponseAsync(WebRequest request)
        {
            var json = string.Empty;
            HttpWebResponse response;
            using (response = (HttpWebResponse)await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        json = await reader.ReadToEndAsync();
                    }
            return json;
        }
    }
}
