using System.ComponentModel.DataAnnotations;

namespace ShopModule.Core.Entities.Base
{
    public class EntityBase
    {
        [Required]
        public int Id { get; set; }
    }
}
