using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.PaymentProcessException;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Data.EFContext;
using Microsoft.AspNetCore.Mvc;

namespace CO.PaymentGateway.API.V1.Controllers.PaymentProcess.WritePaymentProcess
{
    [ApiController]
    public class WritePaymentProcessController : ControllerBase
    {
        private readonly PaymentContext _context;

        private readonly IPaymentProcessCommand _processPaymentCommand;

        public WritePaymentProcessController(PaymentContext context, IPaymentProcessCommand paymentProcessCommand)
        {
            _context = context;
            _processPaymentCommand = paymentProcessCommand;
        }

        [HttpPost("/v1/paymentprocess")]
        [AuthorizeCO("WriteProcessPayment")]
        public async Task<ActionResult<PaymentProcessResponse>> PostPaymentProcessEntity(
            CreatePaymentProcessRequestModel paymentProcessRequest)
        {
            try
            {
                var paymentProcessResponse =
                    await _processPaymentCommand.ExecuteAsync(paymentProcessRequest.ToBusinessRequestModel());

                return Created($"{Request.Path.Value}/{paymentProcessResponse.PaymentProcessId}",
                    paymentProcessResponse);
            }

            catch (COPaymentException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}