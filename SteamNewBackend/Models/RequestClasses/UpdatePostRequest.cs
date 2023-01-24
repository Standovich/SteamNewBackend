using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.RequestClasses
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }
        public string? Post_Title { get; set; }
        public string? Post_Content { get; set; }
        public int Game_Id { get; set; }
    }
}
