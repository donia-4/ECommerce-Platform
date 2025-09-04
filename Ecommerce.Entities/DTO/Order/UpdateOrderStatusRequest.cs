using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.DTO.Order
{
    public class UpdateOrderStatusRequest
    {
        [Required]
        public string Status { get; set; }
    }
}
