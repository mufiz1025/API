using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.DTOs.stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;   // ORM tool to Map you objects of Database Entities.

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly  ApplicationDBContext _context ;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stocks> CreateAsync(Stocks existingStock)
        {
           await _context.Stocks.AddAsync(existingStock);
           await _context.SaveChangesAsync();
           return existingStock;
        }

        public async Task<Stocks?> DeleteAsync(int id)
        {
            var existingStock =await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            {
                return null ;
            }
             _context.Stocks.Remove(existingStock);
             await _context.SaveChangesAsync();
             return existingStock;
        }

        public async  Task<List<Stocks>> GetALLAsync(QueryObject query)
        {
            var stocks =  _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.appUser).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDesending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                if(query.SortBy.Equals("Industry" , StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDesending ? stocks.OrderByDescending(s => s.Industry) : stocks.OrderBy(s => s.Industry);
                }
            }

            var SkipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
        }

      

        public async Task<Stocks?> GetByIdAsync(int id)
        {
             return  await _context.Stocks.Include(c=> c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Stocks?> GetBySymbolAsync(string symbol)
        {
            return _context.Stocks.FirstOrDefaultAsync(i => i.Symbol == symbol);
        }

        public async Task<bool> StockExists(int id)
        {
           return await _context.Stocks.AnyAsync( s => s.Id == id);
        }

        public async Task<Stocks?> UpdateAsync(int id, UpdateStockRequest stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            { 
                return null ;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase= stockDto.Purchase;
            existingStock.LastDivdend = stockDto.LastDivdend;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
             await _context.SaveChangesAsync();

             return existingStock;

        }
    }
}