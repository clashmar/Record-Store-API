using System.Text.Json;

namespace RecordStoreFrontend.Client
{
    public static class HttpRequestExtensions
    {
        public static async Task<(string? Error, T? Value)> GetAsync<T>(this HttpClient client, string path)
        {
            var response = await client.GetAsync(path);

            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return (null, JsonSerializer.Deserialize<T>(responseBody)!);

            }
            else
            {
                return (responseBody, default);
            }
        }

    }
}
