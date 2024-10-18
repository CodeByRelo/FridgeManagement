using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class Fault
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaultID { get; set; }

        [Required(ErrorMessage = "Fault Description is required")]
        public string FaultDescription { get; set; }

        public string Status { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [ForeignKey("FridgeID")]
        public Fridge Fridge { get; set; }
    }

}
