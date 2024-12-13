using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
       Task<List<Stocks>> GetALLAsync(QueryObject query);

       Task<Stocks?> GetByIdAsync(int id);

       Task<Stocks> CreateAsync(Stocks stockModel);

       Task<Stocks?> UpdateAsync(int id , UpdateStockRequest stockDto);

       Task<Stocks?> DeleteAsync(int id);

       Task<bool> StockExists(int id);
     
    }
}