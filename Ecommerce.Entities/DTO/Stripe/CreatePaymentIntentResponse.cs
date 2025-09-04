using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Stripe
{
    public class CreatePaymentIntentResponse
    {
        public string ClientSecret { get; set; }
    }
}
