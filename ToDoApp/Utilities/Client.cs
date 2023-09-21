using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;
using Newtonsoft.Json;
using ToDoAPI.Interfaces;

namespace ToDoAPI.Utilities
{
    public class Client : IClient
    {
        public async Task<HttpResponseMessage> PostAsync(string BaseUrl, string url, object model, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    string concatUrl = BaseUrl + url;
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(10);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");


                    HttpResponseMessage response = await client.PostAsync(concatUrl, content);


                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string BaseUrl, string url, string id, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(10);

                    string aurl = string.Format(url, id);
                    string concatUrl = BaseUrl + aurl;


                    HttpResponseMessage response = await client.GetAsync(concatUrl);

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
