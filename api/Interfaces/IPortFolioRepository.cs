using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;

namespace api.Interfaces
{
    public interface IPortFolioRepository 
    {
        Task<List<Stocks>> GetUserPortfolio(AppUser user);
    }
}