using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.DTOs.stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;

using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [Route("api/Stocks")]
    [ApiController]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class StockController : ControllerBase
    {
         private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) 
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks =  await _stockRepo.GetALLAsync(query);

            var stockdto = stocks.Select(s => s.ToStockDto()).ToList();

            return Ok(stocks);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id )
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
             var stocks =  await _stockRepo.GetByIdAsync(id);

             if (stocks == null) 
             {
                return NotFound();
             }
             return Ok(stocks.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStocks([FromBody] CreateStockRequest updateDto)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = updateDto.ToStockDtoFromCreateDto();
            await _stockRepo.CreateAsync(stockModel);
                
            return CreatedAtAction(nameof(GetById) , new {id = stockModel.Id}, stockModel.ToStockDto()); 
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> Update([FromRoute]  int id ,[FromBody] UpdateStockRequest updateDto)
        { 
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await  _stockRepo.UpdateAsync(id ,updateDto);
            if(stockModel ==null )
            {
                return NotFound();
            }
            return  Ok(stockModel.ToStockDto());
           
        }
        [HttpDelete]
         [Route("{id:int}")]
         public async Task<IActionResult> Delete([FromRoute] int id )
         {
            if(!ModelState.IsValid)
              return BadRequest(ModelState);
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }   
            return NoContent();
         }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    } 
}