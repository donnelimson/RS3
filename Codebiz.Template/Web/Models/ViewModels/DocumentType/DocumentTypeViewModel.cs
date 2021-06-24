using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels.DocumentType
{
    public class DocumentTypeViewModel
    {
        [Key]
        public int DocumentTypeId { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(255)]
        [Display(Name = "Abbreviation?")]
        public string Abbr { get; set; }

        [MaxLength(100)]
        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Display(Name = "Is Date Required?")]
        public bool isDateRequired { get; set; }
        [Display(Name = "Is Document Number Required?")]
        public bool isDocNumberRequired { get; set; }
    }
}