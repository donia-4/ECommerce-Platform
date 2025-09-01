using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;


namespace Ecommerce.Entities.Models
{
    public class Discount
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        public DiscountType Type { get; set; }
        public DiscountStatus Status { get; set; } = DiscountStatus.Active;

        [Range(0, 100)]
        public decimal Value { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Relations
        public List<DiscountProduct> Products { get; set; } = new List<DiscountProduct>();
        public List<DiscountCategory> Categories { get; set; } = new List<DiscountCategory>();
    }
}
