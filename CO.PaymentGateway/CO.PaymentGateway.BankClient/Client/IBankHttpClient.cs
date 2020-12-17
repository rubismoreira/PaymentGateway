using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Entities;

namespace CO.PaymentGateway.BankClient.Client
{
    public interface IBankHttpClient
    {
        Task<BankResponse> CreatePaymentAsync(BankPayment payment);
    }
}