using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CO.PaymentGateway.Business.Core.UseCases.Common
{
    public class GetByIdQueryRequest
    {
        [Required]
        [FromRoute]
        public int Id { get; set; }
    }
}
