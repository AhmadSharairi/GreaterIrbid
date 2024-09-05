using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ComplaintType : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        // Foreign key and navigation property to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigation property to Complaints
        public ICollection<Complaint> Complaints { get; set; }
    }
}
