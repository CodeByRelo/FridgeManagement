using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class StockController
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockControllerID { get; set; }

        [Required(ErrorMessage = "Controller Name is required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

}
