using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class PurchaseRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseRequestID { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public string RequestDetails { get; set; }

        [Required]
        public int StockControllerID { get; set; }

        [ForeignKey("StockControllerID")]
        public StockController StockController { get; set; }

        // Constructor to set RequestDate automatically to the current date and time
        public PurchaseRequest()
        {
            RequestDate = DateTime.Now;  // Automatically sets to the current date and time
        }
    }
}
