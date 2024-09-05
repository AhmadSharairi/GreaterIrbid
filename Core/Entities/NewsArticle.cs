using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class NewsArticle : BaseEntity
    {
        

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [Display(Name = "Publish Date")]
        public DateTime Date { get; set; }

        [MaxLength(255, ErrorMessage = "ImageUrl cannot exceed 255 characters")]
        public string ImageUrl { get; set; }

        [MaxLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        [Required]
        public string Description { get; set; }

        public List<NewsImage> Images { get; set; } = new List<NewsImage>();
    }
}
