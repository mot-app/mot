using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            await client.PostAsync(uri, content);
        }

        public static async Task<string> Read(Uri uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public static async Task Update(object data, Uri uri)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync(uri, content);
        }

        public static async Task Delete(Uri uri)
        {
            await client.DeleteAsync(uri);
        }
    }

}
