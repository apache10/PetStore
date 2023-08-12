using PetStore.Services.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Services
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [AgeRange(20, ErrorMessage = "Pet must not be older than 20 years.")]
        public DateTime DOB { get; set; } = new DateTime();
        [Required]
        [Range(0, 50)]
        public decimal Weight { get; set; }
        [Required]
        [Display(Name = "Pet Type")]
        [RegularExpression("^(Bird|Lizard|Cat|Dog|Rabbit|Ferret)$", ErrorMessage = "Invalid option.")]
        public string? Type { get; set; }
    }
}
