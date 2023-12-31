﻿using BrideyApp.Models;

namespace BrideyApp.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Video { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; }

    }
}
