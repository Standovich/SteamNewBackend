using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.RequestClasses
{
    public class FormPostRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
