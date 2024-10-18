using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class FridgeAllocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllocationID { get; set; }

        [Required]
        public DateTime AllocationDate { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [ForeignKey("FridgeID")]
        public Fridge Fridge { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        // Constructor to set AllocationDate automatically to the current date and time
        public FridgeAllocation()
        {
            AllocationDate = DateTime.Now;  // Automatically sets the current date and time
        }
    }


}
