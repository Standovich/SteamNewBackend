﻿using System.ComponentModel.DataAnnotations;

namespace SteamNewBackend.Models.RequestClasses
{
    public class UpdateGameRequest
    {
        public int Id { get; set; }
        public string? Game_Name { get; set; }
        public DateTime Game_RelDate { get; set; }
        public string? Game_Description { get; set; }
        public int Game_Price { get; set; }
        public int DevTeam_Id { get; set; }
    }
}
