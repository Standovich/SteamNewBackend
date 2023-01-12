using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Game_Name { get; set; }

        [Required]
        public DateTime Game_RelDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Game_Description { get; set; }

        [Required]
        public int Game_Price { get; set; }

        [Required]
        public int DevTeam_Id { get; set; }
        public ICollection<Post> Posts { get; set; }
        public List<UserGame> UserGames { get; set; }
    }
}
