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

        public string BestBeforeHumanized => BestBefore.HumanizeDate();
    }
}
            