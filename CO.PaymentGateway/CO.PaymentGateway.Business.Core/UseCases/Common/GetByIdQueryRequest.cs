using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CO.PaymentGateway.Business.Core.UseCases.Common
{
    public class GetByIdQueryRequest
    {
        [Required] [FromRoute] public int Id { get; set; }

        public override string ToString()
        {
            return $"Id-{Id}";
        }
    }
}