using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Stripe
{
    public class CreatePaymentIntentRequest
    {
        public long Amount { get; set; } // in cents
        public string Currency { get; set; } = "egp";
        public string Description { get; set; }
    }
}
