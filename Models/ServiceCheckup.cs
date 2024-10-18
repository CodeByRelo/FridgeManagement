using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class ServiceCheckup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceCheckupID { get; set; }

        [Required(ErrorMessage = "Checkup Date is required")]
        public DateTime CheckupDate { get; set; }

        public string Notes { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [ForeignKey("FridgeID")]
        public Fridge Fridge { get; set; }
    }

}
