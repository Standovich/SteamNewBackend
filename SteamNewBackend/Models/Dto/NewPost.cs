using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.Dto
{
    public class NewPost
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int GameId { get; set; }
    }
}
