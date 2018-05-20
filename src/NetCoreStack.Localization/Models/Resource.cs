using NetCoreStack.Data.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStack.Localization.Models
{
    [Table("Resource", Schema = "Localization")]
    public class Resource : EntityIdentitySql
    {
        [Required]
        public long LanguageId { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Key")]
        public string Key { get; set; }

        [Required]
        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public virtual Language Language { get; set; }
    }
}