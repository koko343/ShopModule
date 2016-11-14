using ShopModule.Core.Entities;

namespace ShopModule.Models.Shopping
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Picture Picture { get; set; }
    }
}