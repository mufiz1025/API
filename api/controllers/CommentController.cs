using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.comment;
using api.Extentions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
         private readonly ICommentRepository _commentRepo;

         private readonly IStockRepository _StockRepo;

         private readonly UserManager<AppUser> _userManager;
         public CommentController(ICommentRepository commentRepo , IStockRepository StockRepo , UserManager<AppUser> userManager)
         {
            _commentRepo = commentRepo;
            _StockRepo = StockRepo;
            _userManager = userManager;
         }
         [HttpGet]
         public async Task<IActionResult> GetAll()
         {
           if(!ModelState.IsValid)
            return BadRequest(ModelState);

           var comments = await _commentRepo.GetAllAsync();
            var CommentDto = comments.Select(S => S.ToCommentDto());
           return Ok(CommentDto);
         }
         [HttpGet]
         [Route("{id:int}")]

         public async Task<IActionResult> GetById([FromRoute] int id )
         {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
          
             var comment = await _commentRepo.GetByIdAsync(id);
             if(comment == null)
             {
                return NotFound();
             }

             return Ok(comment.ToCommentDto());
         }

         [HttpPost("{stockId:int}")]
         public async Task<IActionResult> Create([FromRoute] int stockId ,CreateCommentDto commentDto)
         {
               if(!ModelState.IsValid)
                return BadRequest(ModelState);
              
               if(!await _StockRepo.StockExists(stockId))
               {
                return BadRequest("Stock does not Exist");
               }
               var username = User.GetUsername();
               var appUser = await _userManager.FindByNameAsync(username);

               var commentModel = commentDto.ToCommentfromCreate(stockId);
               commentModel.AppUserId = appUser.Id;
               await _commentRepo.CreateAsync(commentModel);

               return  CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

         }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentDto UpdateDto)
        {
           if(!ModelState.IsValid)
            return BadRequest(ModelState);
           
           var comment = await _commentRepo.UpdateAsync(id , UpdateDto.ToCommentFromUpdate());
          if (comment == null )
          { 
            return NotFound("Comment Not Found !");
          } 
          return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
           if(!ModelState.IsValid)
            return BadRequest(ModelState);
          
           var commentModel = await _commentRepo.DeleteAsync(id);
           if(commentModel == null) 
           {
            return NotFound("comment does not Exist to delete It.");
           }
           return NoContent();
        }

    }
}