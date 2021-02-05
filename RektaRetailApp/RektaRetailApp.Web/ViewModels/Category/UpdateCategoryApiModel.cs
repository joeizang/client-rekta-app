﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ViewModel.Category
{
    public class UpdateCategoryViewModel
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = null!;

        [StringLength(450)]
        public string? CategoryDescription { get; set; }

        public int CategoryId { get; set; }
    }
}
