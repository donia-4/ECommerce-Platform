using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Stripe
{
    public class CreateCheckoutSessionRequest
    {
        public List<string> ProductNames { get; set; } = new(); 
        public List<long> UnitAmounts { get; set; } = new(); // in cents
        public List<int> Quantities { get; set; } = new();
        public string Currency { get; set; } = "egp";
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
