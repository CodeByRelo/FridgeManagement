using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string FirtsName { get; set; }

        public string lastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }

}
