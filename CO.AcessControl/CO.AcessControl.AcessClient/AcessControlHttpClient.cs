using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CO.AcessControl.AcessClient
{
    public class AcessControlHttpClient : IAcessControlHttpClient
    {
        private readonly HttpClient _httpClient;

        public AcessControlHttpClient(HttpClient client)
        {
            this._httpClient = client;
        }

        public async Task AuthorizeAsync(string policyAuth, string authenticationBearer)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", authenticationBearer);
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("accesscontrol/authorize", new {policy = policyAuth});
            
        }

    }
}