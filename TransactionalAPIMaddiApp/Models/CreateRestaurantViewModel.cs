﻿using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class CreateRestaurantViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Nit { get; set; }
        [JsonIgnore]
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public bool BiActive { get; set; }
    }
}
