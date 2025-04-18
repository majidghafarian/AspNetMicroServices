using System.Text.Json;

namespace Shopping.Aggregator.Extensions
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
    }
}
