using System.ComponentModel.DataAnnotations;

namespace NetCoreStack.Localization.Components.LanguageSelector.Models
{
    public class CreateResourceDto
    {
        [Required]
        public long LanguageId { get; set; }

        [MaxLength(255)]
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public string Comment { get; set; }
    }
}
