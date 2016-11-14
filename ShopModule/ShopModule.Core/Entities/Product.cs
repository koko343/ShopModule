using ShopModule.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Core.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int PictureId { get; set; }

        [ForeignKey("PictureId")]
        public virtual Picture Picture { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
