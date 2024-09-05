using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ComplaintDto
    {
        [StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string Mobile { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string HideIdentity { get; set; }

        public string TypeEmergency { get; set; }

        public string Details { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(100)]
        public string LocationDetails { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public IFormFile ImageUrl { get; set; }

        [StringLength(50)]
        public string ShareLocationMaps { get; set; }

        // Optional properties for DTO, include if needed
        public string CategoryTitle { get; set; }
        public string ComplaintTypeName { get; set; }
    }
}
