using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Post_Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string? Post_Content { get; set; }
        public int Game_Id { get; set; }
        public Game Game { get; set; }
    }
}
