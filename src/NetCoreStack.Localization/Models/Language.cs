using NetCoreStack.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStack.Localization.Models
{
    [Table("Language", Schema = "Localization")]
    public class Language : EntityIdentitySql
    {
        [MaxLength(255)]
        [Required]
        [Display(Name = "Culture Name")]
        public string CultureName { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Region")]
        public string Region { get; set; }

        public bool IsDefaultLanguage { get; set; }

        public virtual List<Resource> Resources { get; set; }
    }
}