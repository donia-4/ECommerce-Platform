﻿namespace Ecommerce.Entities.DTO.Cart
{
    public class AddToCartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
