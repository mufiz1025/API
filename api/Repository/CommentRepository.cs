using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.DTOs.comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;      
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
           await _context.Comment.AddAsync(commentModel);
           await _context.SaveChangesAsync();
              return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {  
            var exisitingComment = await _context.Comment.FirstOrDefaultAsync(c => c.Id ==id);
            if(exisitingComment == null)
            { 
                return null;
            }
            _context.Comment.Remove(exisitingComment);
            await _context.SaveChangesAsync();

            return exisitingComment;
        
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.ToListAsync();
        }

       

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return  await _context.Comment.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var exisitingComment = await _context.Comment.FindAsync(id);
            if (exisitingComment == null)
            {
                return null;
            }
            exisitingComment.Content = commentModel.Content;
            exisitingComment.Title = commentModel.Title;
            await _context.SaveChangesAsync();

            return exisitingComment;
        }
    }
}