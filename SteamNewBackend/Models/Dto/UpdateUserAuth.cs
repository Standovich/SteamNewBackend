namespace SteamNewBackend.Models.Dto
{
    public class UpdateUserAuth
    {
        public int Id { get; set; }
        public string? User_Name { get; set; }
        public string? User_OldPassword { get; set; }
        public string? User_NewPassword { get; set; }
        public string? Role { get; set; }
        public int DevTeam_Id { get; set; }
    }
}
