using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.RequestClasses
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string? User_Name { get; set; }
        public string? User_Password { get; set; }
        public string? Role { get; set; }
        public int DevTeam_Id { get; set; }
    }
}
