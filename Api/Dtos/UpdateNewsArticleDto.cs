
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class UpdateNewsArticleDto
    {

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string Title { get; set; }

        [MaxLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}