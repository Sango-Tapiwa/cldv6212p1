﻿
// Models/ViewModels/HomeViewModel.cs
using ABCRetails.Models;

namespace ABCRetails.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; } = new();
        public int CustomerCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
    }
}