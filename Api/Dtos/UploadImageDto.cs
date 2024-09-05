
using System.ComponentModel.DataAnnotations;



namespace Api.Dtos
{
    public class UploadImageDto
    {
 

        [Required(ErrorMessage = "Main image is required.")]
        public IFormFile ImageUrl { get; set; }

    }
}