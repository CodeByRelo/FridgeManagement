using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class FaultReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportID { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        public string ReportDetails { get; set; }

        [Required]
        public int FaultID { get; set; }

        [ForeignKey("FaultID")]
        public Fault Fault { get; set; }
    }

}
