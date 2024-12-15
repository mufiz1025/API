using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static commentDto ToCommentDto(this Comment CommentModel)
        {
            return new commentDto
            {
                 Id = CommentModel.Id,
                 Content = CommentModel.Content,
                 CreatedOn = CommentModel.CreatedOn,
                 StockId = CommentModel.StockId,
                 CreatedBy = CommentModel.appUser.UserName,
                 Title = CommentModel.Title
            };
        }
        public static Comment ToCommentfromCreate(this CreateCommentDto CommentDto , int stockId)
        {
            return new Comment
            {
                
                 Content = CommentDto.Content,
                 Title = CommentDto.Title, 
                 StockId = stockId 
            };
        }
         public static Comment ToCommentFromUpdate(this UpdateCommentDto CommentDto)
        {
            return new Comment
            {
                Content = CommentDto.Content,
                Title = CommentDto.Title
                // CreatedOn = CommentDto.CreatedOn
            };
        }
    }
}