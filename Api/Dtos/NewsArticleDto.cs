using System.ComponentModel.DataAnnotations;


namespace Api.Dtos
{
    public class NewsArticleDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Main image is required.")]
        public IFormFile ImageUrl { get; set; }


        [MaxLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
