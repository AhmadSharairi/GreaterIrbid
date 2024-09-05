using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class NewsImage 
    {
      
        public int Id { get; set; }  

        [MaxLength(255, ErrorMessage = "Url cannot exceed 255 characters")]
        public string Url { get; set; }
        public int NewsArticleId { get; set; }
        
        [JsonIgnore] 
        public NewsArticle NewsArticle { get; set; }
    }
}
