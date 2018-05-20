using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace NetCoreStack.Localization
{
    internal class DefaultDataAnnotationsMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> _dataAnnotationLocalizationOptions;

        public DefaultDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions)
        {
            _validationAttributeAdapterProvider = validationAttributeAdapterProvider ?? throw new ArgumentNullException(nameof(validationAttributeAdapterProvider));
            _dataAnnotationLocalizationOptions = dataAnnotationLocalizationOptions ?? throw new ArgumentNullException(nameof(dataAnnotationLocalizationOptions));
        }

        public DefaultDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions,
            IStringLocalizerFactory stringLocalizerFactory)
            : this(validationAttributeAdapterProvider, dataAnnotationLocalizationOptions)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public void Configure(MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var type = typeof(NetCoreStackLocalizationEntityResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            var L = _stringLocalizerFactory.Create(nameof(NetCoreStackLocalizationEntityResource), assemblyName.Name);
            options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor, x, y]);
            options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingBindRequiredValueAccessor, x]);
            options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingKeyOrValueAccessor]);
            options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor, x]);
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueIsInvalidAccessor]);
            options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustBeANumberAccessor]);
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => L[LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustNotBeNullAccessor, x]);
        }
    }
}