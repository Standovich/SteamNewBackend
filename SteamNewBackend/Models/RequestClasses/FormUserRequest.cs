namespace SteamNewBackend.Models.DbRequestClasses
{
    public class FormUserRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int DevTeam { get; set; }
    }
}
