using CO.PaymentGateway.Business.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CO.PaymentGateway.Business.Core.Entities
{
    [Table("PaymentProcessEntities")]
    public class PaymentProcessEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public string CVV { get; set; }

        public int ValidationMonth { get; set; }

        public int ValidationYear { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public DateTime RegistrationTime { get; set; }

        public CardType CardType { get; set; }

        public Currency Currency { get; set; }

        public Guid ContextId { get; set; }

        public Guid UserId { get; set; }

        public Guid BankResponse { get; set; }

        public PaymentStatus BankResponseStatus { get; set; }
    }
}
