namespace SteamNewBackend.Models
{
    public class UserGame
    {
        public int Id { get; set; }
        public int Game_Id { get; set; }
        public Game Game { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
    }
}
