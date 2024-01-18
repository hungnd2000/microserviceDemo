using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Product.API.Factories
{
    public class ApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet(string apiUrl)
        {
            var client = _httpClientFactory.CreateClient();

            //HttpResponseMessage response = await client.GetAsync(apiUrl);

            var stringContent = new StringContent(apiUrl);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync("http://172.116.12.172:8014/iadmin/validate", stringContent);


            if (response.IsSuccessStatusCode)
            {
                using var contentStream =
                    await response.Content.ReadAsStreamAsync();
            }

            //if (response.IsSuccessStatusCode)
            //{
            //    return await response.Content.ReadFromJsonAsync<T>();
            //}
            //else
            //{
            //    // Handle error scenarios
            //    // You might want to throw an exception or return null, depending on your requirements.
            //    return default;
            //}
        }
    }
}
