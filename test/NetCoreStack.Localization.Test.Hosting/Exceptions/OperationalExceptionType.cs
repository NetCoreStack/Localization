using System.ComponentModel.DataAnnotations;

namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public enum OperationalExceptionType
    {
        [Display(Name = "NotSet")]
        NotSet = 0,

        [Display(Name = "ItemNotFoundExceptionKey")]
        ItemNotFound = 1,

        [Display(Name = "CustomExceptionKey")]
        CustomException = 2,
    }
}
