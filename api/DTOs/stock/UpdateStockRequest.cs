using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.stock
{
    public class UpdateStockRequest
    { 
        [Required]
        [MaxLength(10, ErrorMessage ="Symbol must be maximum of 10 charcters.")]
         public string Symbol { get; set; } = string.Empty;

         [Required]
        [MaxLength(50, ErrorMessage ="CompanyName must be maximum of 50 charcters.")]
        public string CompanyName { get; set; } =string.Empty;
        [Required]
        [Range(1,10000000000)]
        public decimal Purchase { get; set; }
         [Required]
         [Range(0.001,100)]
        public decimal LastDivdend { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage ="Industry must be maximum of 25 charcters.")]
        public string Industry { get; set; } = string.Empty;
        [Range(1,50000000000)]
        public long MarketCap { get; set; }
    }
   }
