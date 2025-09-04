using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.Configurations
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PuplishableKey { get; set; }
        public string WebhookSecret { get; set; }

    }
}
