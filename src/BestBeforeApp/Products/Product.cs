using System;

namespace BestBeforeApp.Products
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public DateTime BestBefore { get; set; }
    }
}
