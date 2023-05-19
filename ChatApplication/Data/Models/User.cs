using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
