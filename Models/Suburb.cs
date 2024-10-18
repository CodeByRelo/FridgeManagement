using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagement.Models
{
    public class Suburb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuburbID { get; set; }

        [Required(ErrorMessage = "Suburb Name is required")]
        public string SuburbName { get; set; }

        [Required]
        public int CityID { get; set; }

        [ForeignKey("CityID")]
        public City City { get; set; }
    }
}
