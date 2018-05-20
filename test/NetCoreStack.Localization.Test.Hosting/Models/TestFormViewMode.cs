using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreStack.Localization.Test.Hosting.Models
{
    public class TestFormViewMode
    {
        public TestFormViewMode()
        {
            Countries = new List<SelectListItem>
            {
                new SelectListItem {Text = "Turkey", Value = "1"},
                new SelectListItem {Text = "Azerbaijan", Value = "2"},
                new SelectListItem {Text = "Cyprus", Value = "3"}
            };
        }

        [Display(Name = "AboutMe")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(255, MinimumLength = 50, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string AboutMe { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(255, MinimumLength = 15, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(75, MinimumLength = 2, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string City { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string Company { get; set; }

        [Compare("Password", ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.PasswordNoMatch)]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem> Countries { set; get; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        public SelectListItem Country { get; set; }

        [Display(Name = "EMailAddress")]
        [EmailAddress(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.InvalidEmail)]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        public string EMailAddress { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex)]
        public string Password { get; set; }

        [Display(Name = "PostalCode")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [Range(10000, 99999, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public int PostalCode { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range)]
        public string Username { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [DataType(DataType.Time)]
        [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex)]
        public string Time { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [DataType(DataType.Currency)]
        public string Currency { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex)]
        public string PhoneNumber { get; set; }

        [Display(Name = "CreditCard")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [DataType(DataType.CreditCard)]
        [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex)]
        public string CreditCard { get; set; }

        [Display(Name = "IpAddress")]
        [Required(ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required)]
        [RegularExpression(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$", ErrorMessage = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex)]
        public string IpAddress { get; set; }
    }
}