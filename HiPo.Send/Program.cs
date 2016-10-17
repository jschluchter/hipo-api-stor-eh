using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiPo.Send
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        async Task<JObject> PostAsync(string uri, string data)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(uri, new StringContent(data));

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JObject.Parse(content));
        }
    }
}
