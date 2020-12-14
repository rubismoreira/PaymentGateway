using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO.AcessControl.Models
{
    public class Policies
    {
        public const string WriteProcessPayment = "WriteProcessPayment";
        public const string ReadProcessPayment = "ReadProcessPayment";

        public static AuthorizationPolicy WriteProcessPaymentPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(WriteProcessPayment)
                .Build();
        }

        public static AuthorizationPolicy ReadProcessPaymentPolicy()
        {
            return new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .RequireRole(ReadProcessPayment)
               .Build();
        }
    }
}
