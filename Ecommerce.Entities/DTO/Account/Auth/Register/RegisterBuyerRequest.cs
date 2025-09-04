using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Account.Auth.Register
{
    public class RegisterBuyerRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        // Buyer-specific fields
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }

}
