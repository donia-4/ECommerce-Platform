using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.DTO.Discount
{
    public class CreateDiscountRequest
    {
        public string Code { get; set; } = string.Empty; // max 50 in validator
        public string Type { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> ProductIds { get; set; } = new();
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
