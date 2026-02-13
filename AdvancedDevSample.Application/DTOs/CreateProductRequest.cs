using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le stock ne peut pas être négatif")]
        public int StockQuantity { get; set; }
    }
}

