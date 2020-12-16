using System;
using System.Net.Http;
using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Entities;
using Microsoft.Extensions.Logging;

namespace CO.PaymentGateway.BankClient.Client
{
    public class BankHttpClient : IBankHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BankHttpClient> _logger;

        public BankHttpClient(HttpClient client, ILogger<BankHttpClient> logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public async Task<BankResponse> CreatePayment(BankPayment payment)
        {
            try
            {
                _logger.LogInformation("Communicating with bank client");
                var responseMessage = await _httpClient.PostAsJsonAsync("BankPayment", payment);
                _logger.LogInformation("Ensure success in bank response");
                responseMessage.EnsureSuccessStatusCode();
                _logger.LogInformation("Parsing bank response to known entity");
                var bankResponse = await responseMessage.Content.ReadAsAsync<BankResponse>();
                _logger.LogInformation("Communication Successfull, returning response");
                return bankResponse;
            }
            catch (Exception)
            {
                _logger.LogError("Bad communication with bank client. Returning empty guid as response");
                return new BankResponse {BankResponseId = Guid.Empty};
            }
        }
    }
}