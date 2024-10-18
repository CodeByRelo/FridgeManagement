using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class ServiceHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceHistoryID { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }

        public string ServiceNotes { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [ForeignKey("FridgeID")]
        public Fridge Fridge { get; set; }
    }


}
