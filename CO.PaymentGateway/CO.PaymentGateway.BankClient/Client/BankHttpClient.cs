using System;
using System.Net.Http;
using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Entities;

namespace CO.PaymentGateway.BankClient.Client
{
    public class BankHttpClient : IBankHttpClient
    {
        private readonly HttpClient _httpClient;

        public BankHttpClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<BankResponse> CreatePayment(BankPayment payment)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync("BankPayment", payment);

                responseMessage.EnsureSuccessStatusCode();

                var bankResponse = await responseMessage.Content.ReadAsAsync<BankResponse>();

                return bankResponse;
            }
            catch (Exception)
            {
                return new BankResponse {BankResponseId = Guid.Empty};
            }
        }
    }
}