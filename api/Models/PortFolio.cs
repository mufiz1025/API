using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class PortFolio
    {
        public string? AppUserId { get; set; }
        public int StockId { get; set; }    
        public AppUser? AppUser { get; set; }
        public Stocks? stocks{ get ; set;}

    }
}