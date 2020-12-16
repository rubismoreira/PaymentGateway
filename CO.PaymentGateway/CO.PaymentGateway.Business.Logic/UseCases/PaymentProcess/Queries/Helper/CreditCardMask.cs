using System.Text;
using CO.PaymentGateway.Business.Core.Entities;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries.Helper
{
    public static class CreditCardMask
    {
        public static void Mask(ref PaymentProcessEntity entity)
        {
            var sb = new StringBuilder("XXXX-XXXX-XXXX-");
            sb.Append(entity.CardNumber.Substring(12));
            entity.CardNumber = sb.ToString();
        }
    }
}