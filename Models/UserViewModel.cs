using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FridgeManagement.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }  // Used for Edit and Delete

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string UserRole { get; set; }

        [Required(ErrorMessage = "Password is required for creation")]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // Used for Create only
    }
}
