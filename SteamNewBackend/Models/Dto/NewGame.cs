namespace SteamNewBackend.Models.Dto
{
    public class NewGame
    {
        public string? Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int DevTeamId { get; set; }
    }
}
