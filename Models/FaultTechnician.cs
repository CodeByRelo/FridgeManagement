using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class FaultTechnician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechnicianID { get; set; }

        [Required(ErrorMessage = "Technician Name is required")]
        public string FisrtName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

}
