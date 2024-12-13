using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must have mininmum 3 charcters")]
        [MaxLength(15 , ErrorMessage = "title must have a maximum lenght of 10 charcters.")]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [MinLength(3, ErrorMessage = "Content must have mininmum 3 charcters")]
        [MaxLength(100 , ErrorMessage = "Content must have a maximum lenght of 10 charcters.")]
        public string Content { get; set; } = string.Empty ;
        
    }
}