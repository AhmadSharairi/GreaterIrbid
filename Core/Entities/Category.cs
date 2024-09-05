using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } // e.g., الشكاوي الصحية والمكاره

        // Navigation property to ComplaintTypes
        [JsonIgnore]
        public ICollection<ComplaintType> ComplaintTypes { get; set; }

        [JsonIgnore]
        public ICollection<Complaint> Complaints { get; set; }

     

    }
}
