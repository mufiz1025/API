using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extentions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortFolioController :ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;

        private readonly IPortFolioRepository _PortfolioRepo;
        public PortFolioController(UserManager<AppUser> userManger,
         IStockRepository stockRepo ,
         IPortFolioRepository PortfolioRepo )
        {
            _userManager = userManger;
            _stockRepo = stockRepo;
            _PortfolioRepo = PortfolioRepo;
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _PortfolioRepo.GetUserPortfolio(appUser);

            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateUserPortfolio(String symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _PortfolioRepo.GetUserPortfolio(appUser);

            if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Cannot add same stock to the portfolio");
            }
            var portfolioModel = new PortFolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _PortfolioRepo.CreatePortFolioAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could not create portfolio!");
            }
            else 
            {
                return Created();
            }

           
        }  
         [HttpDelete]
         [Authorize]     
         public async Task<IActionResult> DeletePortFolio(string symbol)
         {
            var username = User.GetUsername() ;
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("User not found");

            var userPortfolio = await _PortfolioRepo.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(e => e.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count()==1)
            {
                await _PortfolioRepo.DeletePortFolioAsync(appUser,symbol);
            }
            else
            {
                return BadRequest("Stock dosent exists in your portfolio");
            }
            return Ok();
        } 
    }
}