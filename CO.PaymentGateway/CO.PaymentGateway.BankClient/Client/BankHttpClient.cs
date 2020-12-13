using System;
using System.Net.Http;
using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Entities;

namespace CO.PaymentGateway.BankClient.Client
{
    public class BankHttpClient : IBankHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public BankHttpClient(HttpClient client)
        {
            this._httpClient = client;
        }

        public async Task<BankResponse> CreatePayment(BankPayment payment)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("BankPayment", payment);

                responseMessage.EnsureSuccessStatusCode();

                var bankResponse = await responseMessage.Content.ReadAsAsync<BankResponse>();

                return bankResponse;
            }
            catch (Exception ex)
            {
                return new BankResponse { BankResponseId = Guid.Empty };
            }
        }

    }
}