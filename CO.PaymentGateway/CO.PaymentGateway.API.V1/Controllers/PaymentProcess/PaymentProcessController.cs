using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Data.EFContext;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CO.PaymentGateway.API.V1.Controllers.PaymentProcess
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessController : ControllerBase
    {
        private readonly PaymentContext _context;


        private readonly IPaymentProcessGetAllQuery _getAllQuery;

        private readonly IPaymentProcessCommand _processPaymentCommand;

        private readonly IPaymentProcessGetByIdQuery _getByIDQuery;

        public PaymentProcessController(PaymentContext context, IPaymentProcessGetAllQuery paymentProcessGetAllQuery, IPaymentProcessGetByIdQuery paymentProcessGetByIdQuery, IPaymentProcessCommand paymentProcessCommand)
        {
            _context = context;
            this._getAllQuery = paymentProcessGetAllQuery;
            this._getByIDQuery = paymentProcessGetByIdQuery;
            this._processPaymentCommand = paymentProcessCommand;
        }

        // GET: api/PaymentProcessEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentProcessEntity>>> GetProcessPaymentEntities()
        {
            var result =  await _getAllQuery.ExecuteAsync();
            return new ActionResult<IEnumerable<PaymentProcessEntity>>(result);
        }

        // GET: api/PaymentProcessEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentProcessEntity>> GetPaymentProcessEntity(int id)
        {
            var paymentProcessEntity = await this._getByIDQuery.ExecuteAsync(new GetByIdQueryRequest { Id = id});

            if (paymentProcessEntity == null)
            {
                return NotFound();
            }

            return paymentProcessEntity;
        }

        // POST: api/PaymentProcessEntities
        [HttpPost]
        public async Task<ActionResult<PaymentProcessResponse>> PostPaymentProcessEntity(PaymentProcessRequest paymentProcessRequest)
        {
            var paymentProcessResponse  = await  this._processPaymentCommand.ExecuteAsync(paymentProcessRequest);

            return Created($"{this.Request.Path.Value}/{paymentProcessResponse.BankResponse}", paymentProcessResponse);
        }
    }

   
}
