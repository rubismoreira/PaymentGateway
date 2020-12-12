using CO.PaymentGateway.Business.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CO.PaymentGateway.Business.Core.Entities
{
    public class PaymentProcessEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public string CCV { get; set; }

        public int ValidationMonth { get; set; }

        public decimal Amount { get; set; }

        public DateTime RegistrationTime { get; set; }

        public CardType CardType { get; set; }

        public Currency Currency { get; set; }
    }
}
