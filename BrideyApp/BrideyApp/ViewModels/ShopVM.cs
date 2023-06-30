﻿using BrideyApp.Models;

namespace BrideyApp.ViewModels
{
    public class ShopVM
    {
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Color> Colors { get; set; }
        public List<Composition> Compositions { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
        public Sale Sale { get; set; }


    }
}