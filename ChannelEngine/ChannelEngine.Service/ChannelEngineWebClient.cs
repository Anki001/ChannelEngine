using ChanelEngine.Service.Interfaces;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ChanelEngine.Service
{
    public class ChannelEngineWebClient : IChannelEngineWebClient
    {
        public async Task<string> GetAsync(string url)
        {
            var request = GetRequestObject(url);
            request.Method = "GET";

            return await GetApiWebResponseAsync(request);
        }

        private HttpWebRequest GetRequestObject(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Accept-Charset", "utf-8");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Content-Type", "application/json");
            request.CookieContainer = new CookieContainer();
            request.Expect = "application/json";

            return request;
        }

        private static async Task<string> GetApiWebResponseAsync(WebRequest request)
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
