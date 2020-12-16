using System.Collections.Generic;
using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Data.EFContext;
using Microsoft.AspNetCore.Mvc;

namespace CO.PaymentGateway.API.V1.Controllers.PaymentProcess.ReadPaymentProcess
{
    [ApiController]
    public class ReadPaymentProcessController : ControllerBase
    {
        private readonly PaymentContext _context;

        private readonly IPaymentProcessGetAllQuery _getAllQuery;

        private readonly IPaymentProcessGetByIdQuery _getByIDQuery;

        public ReadPaymentProcessController(PaymentContext context,
            IPaymentProcessGetAllQuery paymentProcessGetAllQuery,
            IPaymentProcessGetByIdQuery paymentProcessGetByIdQuery)
        {
            _context = context;
            _getAllQuery = paymentProcessGetAllQuery;
            _getByIDQuery = paymentProcessGetByIdQuery;
        }

        // GET: api/PaymentProcessEntities
        [HttpGet("/v1/paymentprocess")]
        [AuthorizeCO("ReadProcessPayment")]
        public async Task<ActionResult<IEnumerable<PaymentProcessEntity>>> GetProcessPaymentEntities()
        {
            var result = await _getAllQuery.ExecuteAsync();
            return new ActionResult<IEnumerable<PaymentProcessEntity>>(result);
        }

        // GET: api/PaymentProcessEntities/5
        [HttpGet("/v1/paymentprocess/{id}")]
        [AuthorizeCO("ReadProcessPayment")]
        public async Task<ActionResult<PaymentProcessEntity>> GetPaymentProcessEntity(int id)
        {
            var paymentProcessEntity = await _getByIDQuery.ExecuteAsync(new GetByIdQueryRequest {Id = id});

            if (paymentProcessEntity == null) return NotFound();

            return paymentProcessEntity;
        }
    }
}