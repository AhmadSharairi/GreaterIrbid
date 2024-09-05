using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Complaint : BaseEntity
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
        public DateTime Date { get; set; }  = DateTime.UtcNow;

        [Url]
        public string ImageUrl { get; set; }

        [StringLength(50)]
        public string ShareLocationMaps { get; set; }

        // Foreign key and navigation property to ComplaintType
        public int ComplaintTypeId { get; set; }
        [JsonIgnore]
        public ComplaintType ComplaintType { get; set; }


         // Foreign key and navigation property to Category
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }

    }
}
