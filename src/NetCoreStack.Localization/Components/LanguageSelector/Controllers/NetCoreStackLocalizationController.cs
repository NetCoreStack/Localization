using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.Data.Interfaces;
using NetCoreStack.Localization.Components.LanguageSelector.Models;
using NetCoreStack.Localization.MemoryCache;
using NetCoreStack.Localization.Models;
using System;
using System.Globalization;
using System.Linq;

namespace NetCoreStack.Localization.Components.LanguageSelector.Controllers
{
    public class NetCoreStackLocalizationController : Controller
    {
        private readonly ISqlUnitOfWork _sqlUnitOfWork;
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;
        public NetCoreStackLocalizationController(ISqlUnitOfWork sqlUnitOfWork, LocalizationInMemoryCacheProvider cacheProvider)
        {
            _sqlUnitOfWork = sqlUnitOfWork;
            _cacheProvider = cacheProvider;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult ShowMeTheCulture()
        {
            return new JsonResult($"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}");
        }

        [HttpGet]
        public JsonResult GetAllLanguage()
        {
            return Json(_cacheProvider.GetAllLanguage());
        }

        [HttpGet]
        public JsonResult GetResourceByLanguageId(long id)
        {
            var resources = _cacheProvider.GetResourceListByLanguageId(id);
            foreach (var item in resources)
            {
                item.Language.Resources = null;
            }
            return Json(resources);
        }

        [HttpGet]
        public JsonResult GetAllResource()
        {
            var resources = _cacheProvider.ResourceDictionary.Select(k => k.Value);
            foreach (var item in resources)
            {
                item.Language.Resources = null;
            }
            return Json(resources);
        }

        [HttpPost]
        public IActionResult CreateLanguage([FromBody]CreateLanguageDto input)
        {
            if (input == null)
                return new BadRequestObjectResult(ModelState);

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var languageRepo = _sqlUnitOfWork.Repository<Language>();
            var newLanguage = new Language
            {
                ObjectState = Contracts.ObjectState.Added,
                Country = input.Country,
                CultureName = input.CultureName,
                DisplayName = input.DisplayName,
                IsDefaultLanguage = input.IsDefaultLanguage,
                Region = input.Region
            };
            languageRepo.SaveAllChanges(newLanguage);
            _cacheProvider.Remove(nameof(Language));
            return new OkObjectResult(newLanguage);
        }

        [HttpPost]
        public IActionResult CreateResource([FromBody]CreateResourceDto input)
        {
            if (input == null)
                return new BadRequestObjectResult(ModelState);

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var languageRepo = _sqlUnitOfWork.Repository<Language>();
            var resourceRepo = _sqlUnitOfWork.Repository<Resource>();

            if (languageRepo.FirstOrDefault(k => k.Id == input.LanguageId) == null)
                return new NotFoundObjectResult(new { Input = input, error = $"There was no 'Language' with an id of {input.LanguageId}." });

            var newResource = new Resource
            {
                ObjectState = Contracts.ObjectState.Added,
                LanguageId = input.LanguageId,
                Key = input.Key,
                Value = input.Value,
                Comment = input.Comment
            };
            resourceRepo.SaveAllChanges(newResource);
            _cacheProvider.Remove(nameof(Resource));
            return new OkObjectResult(newResource);
        }
    }
}