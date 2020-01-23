using System;
using BestBeforeApp.Helpers;
using BestBeforeApp.Shared;

namespace BestBeforeApp.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public DateTime BestBefore { get; set; }

        public byte[] Photo { get; set; }

        public string BestBeforeHumanized => BestBefore.HumanizeDate();

        public Product() { }

        public Product(string name, DateTime bestBefore, int amount)
        {
            Name = name;
            BestBefore = bestBefore;
            Amount = amount;
        }

        public Product(string name, DateTime bestBefore, int amount, byte[] photo)
            : this(name, bestBefore, amount) => Photo = photo;
    }
}
            