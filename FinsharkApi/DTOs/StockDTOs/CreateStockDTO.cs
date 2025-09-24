using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinsharkApi.DTOs
{
    public class CreateStockDTO
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot exceed 10 characters.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company Name  cannot exceed 10 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000000000 , ErrorMessage = "Price must be a positivevalue.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0.001, 100, ErrorMessage = "Last Dividend must be between 0 and 100.")]

        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Exchange cannot exceed 10 characters.")]
        public string Industry { get; set; } = string.Empty;
        [Range(1,50000000000000)]
        public long MarketCap { get; set; }

    }
}