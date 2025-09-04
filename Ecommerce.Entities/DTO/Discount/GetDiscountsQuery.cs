using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Discount
{
    public class GetDiscountsQuery
    {
        public string? Status { get; set; } // "active" or "expired" or null
        public string? Code { get; set; } // optional search by code
        public int PageNumber { get; set; } = 1;  
        public int PageSize { get; set; } = 10;
    }

}
