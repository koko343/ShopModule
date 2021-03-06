﻿using ShopModule.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopModule.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int PictureId { get; set; }

        public string Category { get; set; }

        public IEnumerable<Category> CategoryItems { get; set; }

        public bool IsBought { get; set; }

        public Picture Picture { get; set; }
    }
}