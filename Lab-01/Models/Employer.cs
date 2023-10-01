using System.ComponentModel.DataAnnotations;

namespace Lab_01.Models
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Employer Name")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Url]
        [StringLength(100)]
        [Display(Name = "Website")]
        public string Website { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Incorporated Date")]
        public DateTime? IncorporatedDate { get; set; }
    }
}
