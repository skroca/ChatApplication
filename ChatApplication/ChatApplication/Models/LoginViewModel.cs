using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}