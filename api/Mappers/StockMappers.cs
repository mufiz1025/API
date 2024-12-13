using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
         public static StockDto ToStockDto(this Stocks stockDto)
         {
            return new StockDto{
               
               Symbol = stockDto.Symbol,
               CompanyName = stockDto.CompanyName,
               Purchase = stockDto.Purchase,
               LastDivdend =stockDto.LastDivdend,
               Industry =stockDto.Industry,
               MarketCap =stockDto.MarketCap,
               Comments = stockDto.Comments.Select(c => c.ToCommentDto()).ToList()
            };
         }
         public static Stocks ToStockDtoFromCreateDto(this CreateStockRequest StockDto)
         {
            return new Stocks{
               Symbol = StockDto.Symbol,
               CompanyName = StockDto.CompanyName,
               Purchase = StockDto.Purchase,
               LastDivdend =StockDto.LastDivdend,
               Industry =StockDto.Industry,
               MarketCap =StockDto.MarketCap
            };
         }
        
    }
}