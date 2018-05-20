using System.ComponentModel.DataAnnotations;

namespace NetCoreStack.Localization.Components.LanguageSelector.Models
{
    public class CreateLanguageDto
    {
        [MaxLength(255)]
        [Required]
        public string CultureName { get; set; }

        [MaxLength(255)]
        [Required]
        public string DisplayName { get; set; }

        [MaxLength(255)]
        [Required]
        public string Country { get; set; }

        [MaxLength(255)]
        [Required]
        public string Region { get; set; }

        public bool IsDefaultLanguage { get; set; }
    }
}
