using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.comment;
using api.Models;

namespace api.DTOs.stock
{
    public class StockDto
    {
        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } =string.Empty;
       
        public decimal Purchase { get; set; }

        public decimal LastDivdend { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public required List<commentDto> Comments  { get; set; } 
    }
}