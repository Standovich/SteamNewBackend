using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models
{
    public class DevTeam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? DevTeam_name { get; set; }
    }
}