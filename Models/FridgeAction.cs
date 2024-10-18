using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class FridgeAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionID { get; set; }

        [Required]
        public DateTime ActionDate { get; set; }

        public string ActionDescription { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [ForeignKey("FridgeID")]
        public Fridge Fridge { get; set; }

        // Constructor to set ActionDate automatically to the current date and time
        public FridgeAction()
        {
            ActionDate = DateTime.Now;  // Automatically sets to the current date and time
        }
    }
}
