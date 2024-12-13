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
    }
}