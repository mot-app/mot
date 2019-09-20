using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mot.Services.Api
{
    public static class RestService
    {
        private static HttpClient client = new HttpClient();

        public static async Task Create(object data, Uri uri)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }

        public static async Task Read(object data, Uri uri)
        {
            var json = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await client.GetAsync(uri);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }

        public static async Task Update(object data, Uri uri)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(uri, content);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }

        public static async Task Delete(object data, Uri uri)
        {
            var json = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await client.DeleteAsync(uri);
            System.Diagnostics.Debug.WriteLine(response.Content);
        }
    }

}
