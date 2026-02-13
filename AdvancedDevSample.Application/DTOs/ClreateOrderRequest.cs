using System;
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs
{
    public class CreateOrderRequest
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Range(1, 1000, ErrorMessage = "La quantité doit être au moins 1")]
        public int Quantity { get; set; }
    }
}