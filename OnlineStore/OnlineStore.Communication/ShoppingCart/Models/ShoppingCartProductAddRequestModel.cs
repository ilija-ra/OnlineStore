﻿namespace OnlineStore.Communication.ShoppingCart.Models
{
    public class ShoppingCartProductAddRequestModel
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public long? Quantity { get; set; }

        public string? Category { get; set; }
    }
}
