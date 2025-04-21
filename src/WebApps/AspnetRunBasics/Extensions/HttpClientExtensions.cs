using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            }
            else
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }
        }
        public static async Task<HttpResponseMessage> PostAsJson<TRequest>(this HttpClient httpClient, string url, TRequest data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);

            return response;
        }

        public static async Task<HttpResponseMessage> PutAsJson<TRequest, TResponse>(this HttpClient httpClient, string url, TRequest data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, content).ConfigureAwait(false);

            return await response.ReadContentAs<HttpResponseMessage>();
        }

    }
}
