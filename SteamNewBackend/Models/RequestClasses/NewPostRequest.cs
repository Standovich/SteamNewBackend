using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.RequestClasses
{
    public class NewPostRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int GameId { get; set; }
    }
}
