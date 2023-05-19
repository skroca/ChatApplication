using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Data.DTOModels
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public List<string> Validate()
        {
            List<string> validations = new List<string>();

            try
            {
                MailAddress m = new MailAddress(Email);
            }
            catch (FormatException)
            {
                validations.Add("Provide a valid EmailAddress");
            }

            return validations;
        }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
