using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.Models.Auth.Identity;

namespace Ecommerce.Entities.Models.Auth
{
    public class Buyer
    {
        [Key]
        public string Id { get; set; }   // PK = FK to User.Id

        [ForeignKey(nameof(Id))]
        public User User { get; set; }

        public DateTime BirthDate { get; set; }
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Relations
        public Cart Cart { get; set; }           // One-to-One
        public Wishlist Wishlist { get; set; }   // One-to-One
        public List<Order> Orders { get; set; } = new List<Order>(); // One-to-Many
    }
}
