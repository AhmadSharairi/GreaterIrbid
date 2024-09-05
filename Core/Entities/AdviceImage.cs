using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AdviceImage : BaseEntity
    {
        [MaxLength(255, ErrorMessage = "Url cannot exceed 255 characters")]
        public string Url { get; set; }
    }
}