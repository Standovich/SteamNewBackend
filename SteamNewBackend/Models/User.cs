using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? User_Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? User_Password { get; set; }

        public string? Role { get; set; }

        public string? Token { get; set; }

        public int DevTeam_Id { get; set; }
        public List<UserGame> UserGames { get; set; }
    }
}
