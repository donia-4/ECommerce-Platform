using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.DTO.Discount
{
    public class GetDiscountResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } // stored flag
        public bool IsCurrentlyActive { get; set; } // computed (flag && range)
        public List<Guid> GuidProductIds { get; set; } = new();
        public List<Guid> GuidCategoryIds { get; set; } = new();
    }
}
