using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Discount
{
    public class ApplyDiscountRequest
    {
        public string Code { get; set; }
        public List<Guid> ProductIds { get; set; }
    }

}
