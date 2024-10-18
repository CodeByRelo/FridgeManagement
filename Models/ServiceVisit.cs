using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class ServiceVisit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceVisitID { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public string VisitDetails { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [ForeignKey("FridgeID")]
        public Fridge Fridge { get; set; }
    }

}
