using Microsoft.AspNetCore.Builder;
using NetCoreStack.Contracts;
using NetCoreStack.Localization.Interfaces;
using NetCoreStack.Localization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStack.Localization
{
    internal class DatabaseStartupTask : ILocalizationStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(IApplicationBuilder app)
        {
            using (var unitOfWork = _serviceProvider.CreateUnitOfWork())
            {
                var languageRepo = unitOfWork.Repository<Language>();
                var resourceRepo = unitOfWork.Repository<Resource>();

                if (!languageRepo.Any(k => k.CultureName == "en-US"))
                {
                    var language = new Language
                    {
                        ObjectState = ObjectState.Added,
                        Country = "English - United States",
                        CultureName = "en-US",
                        DisplayName = "English - United States",
                        Region = "United States",
                        IsDefaultLanguage = true
                    };
                    languageRepo.SaveAllChanges(language);

                    var resureceList = new List<Resource>();
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingBindRequiredValueAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingBindRequiredValueAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingBindRequiredValueAccessor });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingKeyOrValueAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingKeyOrValueAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingKeyOrValueAccessor });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueIsInvalidAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueIsInvalidAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueIsInvalidAccessor });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustBeANumberAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustBeANumberAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustBeANumberAccessor });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustNotBeNullAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustNotBeNullAccessor, Value = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustNotBeNullAccessor });

                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.InvalidEmail), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.InvalidEmail, Value = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.InvalidEmail });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex, Value = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.PasswordNoMatch), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.PasswordNoMatch, Value = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.PasswordNoMatch });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range, Value = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required, Value = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required });
                    resourceRepo.SaveAllChanges(resureceList);
                }

                if (!languageRepo.Any(k => k.CultureName == "tr-TR"))
                {
                    var language = new Language
                    {
                        ObjectState = ObjectState.Added,
                        Country = "Turkish - Turkey",
                        CultureName = "tr-TR",
                        DisplayName = "Turkish - Turkey",
                        Region = "Turkey",
                        IsDefaultLanguage = false,
                    };
                    languageRepo.SaveAllChanges(language);

                    var resureceList = new List<Resource>();
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor, Value = "'{0}' değeri için {1} geçerli değil." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingBindRequiredValueAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingBindRequiredValueAccessor, Value = "'{0}' alanı için bir değer sağlanamadı." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingKeyOrValueAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.MissingKeyOrValueAccessor, Value = "Bir değer gerekli." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor, Value = "Sağlanan değer, {0} için geçersiz." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueIsInvalidAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueIsInvalidAccessor, Value = "'{0}' değeri geçersiz." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustBeANumberAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustBeANumberAccessor, Value = "{0} alanı bir sayı olmalıdır." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustNotBeNullAccessor), Key = LocalizationGlobals.DefaultLocalizationMessage.ModelBindingMessage.ValueMustNotBeNullAccessor, Value = "Null değeri geçersiz." });

                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.InvalidEmail), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.InvalidEmail, Value = "{0} alanı geçerli bir e-posta adresi değil." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.MustMatchRegex, Value = "{0} alanı, '{1}' ifadesinin düzenli ifadesiyle eşleşmelidir." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.PasswordNoMatch), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.PasswordNoMatch, Value = "Şifre ve onay şifresi uyuşmuyor." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Range, Value = "{0} alanı, {2} ifadesinden küçük ve {1} ifadesinden büyük olmalıdır." });
                    resureceList.Add(new Resource { ObjectState = ObjectState.Added, LanguageId = language.Id, Comment = nameof(LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required), Key = LocalizationGlobals.DefaultLocalizationMessage.AttributeMessage.Required, Value = "{0} alanı boş olamaz." });
                    resourceRepo.SaveAllChanges(resureceList);
                }
            }

            await Task.CompletedTask;
        }
    }
}