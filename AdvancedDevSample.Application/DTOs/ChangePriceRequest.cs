using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdvancedDevSample.Application.DTOs
{
    public class ChangePriceRequest
    {
        /// <Summary>
        /// Nouveau prix du produit
        /// Doit etre strictement positif
        /// </Summary>>
        [Required]
        [Range(0.01, double.MaxValue)]

        public decimal NewPrice { get; set; }

    }
}
