using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Discount
{
    public class GetActiveDiscountsResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; } // Enum → String
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

}
