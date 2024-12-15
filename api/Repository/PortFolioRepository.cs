using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortFolioRepository : IPortFolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortFolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PortFolio> CreatePortFolioAsync(PortFolio portFolio)
        {
            await _context.PortFolios.AddAsync(portFolio);
            await  _context.SaveChangesAsync();
            return portFolio;
        }

        public async Task<PortFolio> DeletePortFolioAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.PortFolios.FirstOrDefaultAsync(x => x.AppUserId  == appUser.Id && x.stocks.Symbol.ToLower() == symbol.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }
            _context.PortFolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();

            return portfolioModel;
        }

        public async Task<List<Stocks>> GetUserPortfolio(AppUser user)

        {

            return await _context.PortFolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stocks
            {
                Id = stock.StockId,
                Symbol = stock.stocks.Symbol,
                CompanyName = stock.stocks.CompanyName,
                Purchase = stock.stocks.Purchase,
                LastDivdend = stock.stocks.LastDivdend,
                Industry = stock.stocks.Industry,
                MarketCap = stock.stocks.MarketCap
            }).ToListAsync();


        }
    }
}