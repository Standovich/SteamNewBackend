using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.Dto
{
    public class UpdatePost
    {
        public int Id { get; set; }
        public string? Post_Title { get; set; }
        public string? Post_Content { get; set; }
        public int Game_Id { get; set; }
    }
}
