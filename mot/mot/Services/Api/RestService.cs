using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        }
    }

}
