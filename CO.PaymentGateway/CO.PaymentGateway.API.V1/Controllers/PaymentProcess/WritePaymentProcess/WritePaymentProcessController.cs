using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Data.EFContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CO.AcessControl.AcessClient;
using CO.PaymentGateway.Business.Core.PaymentProcessException;

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
            this._processPaymentCommand = paymentProcessCommand;
        }

        [HttpPost("/v1/paymentprocess")]
        [AuthorizeCO("WriteProcessPayment")]
        public async Task<ActionResult<PaymentProcessResponse>> PostPaymentProcessEntity(CreatePaymentProcessRequestModel paymentProcessRequest)
        {
            try
            {
                var paymentProcessResponse =
                    await this._processPaymentCommand.ExecuteAsync(paymentProcessRequest.ToBusinessRequestModel());

                return Created($"{this.Request.Path.Value}/{paymentProcessResponse.PaymentProcessId}",
                    paymentProcessResponse);
            }
            
            catch (COPaymentException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }


}
