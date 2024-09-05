using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class CitizenSatisfactionDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comments cannot exceed 1000 characters.")]
        public string Comments { get; set; }
    }
}
