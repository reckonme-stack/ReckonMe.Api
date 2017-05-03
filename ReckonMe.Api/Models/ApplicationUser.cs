using MongoDB.Bson.Serialization.Attributes;

namespace ReckonMe.Api.Models
{
    [BsonIgnoreExtraElements]
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}