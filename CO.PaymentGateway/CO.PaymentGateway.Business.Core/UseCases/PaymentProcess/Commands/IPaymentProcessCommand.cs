using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands
{
    public interface IPaymentProcessCommand
    {
        Task<PaymentProcessResponse> ExecuteAsync(PaymentProcessRequest request);
    }
}