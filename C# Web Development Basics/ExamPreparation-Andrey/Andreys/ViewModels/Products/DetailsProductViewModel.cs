using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels.Products
{
    public class DetailsProductViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
