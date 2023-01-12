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
        public string? User_Passwd { get; set; }

        public int Tier { get; set; }

        public int DevTeam_Id { get; set; }
        public List<UserGame> UserGames { get; set; }
    }
}
