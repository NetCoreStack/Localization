namespace NetCoreStack.Localization
{
    public static class LocalizationGlobals
    {
        public const string NetCoreStackLocalizationControllerRoute = "NcsLocalization";

        public static class DefaultLocalizationMessage
        {
            public static class ModelBindingMessage
            {
                public const string AttemptedValueIsInvalidAccessor = "The value '{0}' is not valid for {1}.";
                public const string MissingBindRequiredValueAccessor = "A value for the '{0}' property was not provided.";
                public const string MissingKeyOrValueAccessor = "A value is required.";
                public const string UnknownValueIsInvalidAccessor = "The supplied value is invalid for {0}.";
                public const string ValueIsInvalidAccessor = "The value '{0}' is invalid.";
                public const string ValueMustBeANumberAccessor = "The field {0} must be a number.";
                public const string ValueMustNotBeNullAccessor = "Null value is invalid.";
            }

            public static class AttributeMessage
            {
                public const string InvalidEmail = "The {0} field is not a valid e-mail address.";
                public const string MustMatchRegex = "The field {0} must match the regular expression '{1}'.";
                public const string PasswordNoMatch = "The password and confirmation password do not match.";
                public const string Range = "{0} must be less than {2} and greater then {1}.";
                public const string Required = "{0} cannot be empty.";
            }
        }
    }
}