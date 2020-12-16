using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Core.UseCases.Common
{
    public interface IQuery<TQueryRequest, TQueryResponse>
    {
        Task<TQueryResponse> ExecuteAsync(TQueryRequest request);
    }
}