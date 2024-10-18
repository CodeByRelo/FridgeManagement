using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class Fridge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FridgeID { get; set; }

        [Required(ErrorMessage = "Fridge Model is required")]
        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Status { get; set; }
    }

}
