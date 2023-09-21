using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoAPI.Interfaces
{
    public interface IClient
    {
        Task<HttpResponseMessage> PostAsync(string BaseUrl, string url, object model, string token);

        Task<HttpResponseMessage> GetAsync(string BaseUrl, string url, string id, string token);
    }
}
