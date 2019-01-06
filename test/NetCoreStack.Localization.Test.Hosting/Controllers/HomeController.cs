using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NetCoreStack.Localization.Test.Hosting.Exceptions;
using NetCoreStack.Localization.Test.Hosting.Models;
using System.Diagnostics;
using System.Globalization;

namespace NetCoreStack.Localization.Test.Hosting.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _stringLocalizer;
        public HomeController(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _stringLocalizer["AboutPageDescription"]; ;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Api()
        {
            return View();
        }

        public IActionResult Forms()
        {
            var vm = new TestFormViewMode();
            return View(vm);
        }

        public IActionResult ClientSideLocalization()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Forms(TestFormViewMode model)
        {
            if (!ModelState.IsValid)
            {
                return View("Forms", model);
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ShowMeTheCulture()
        {
            return new JsonResult($"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}");
        }

        public IActionResult CustomAjaxException()
        {
            throw new ItemNotFoundException();
        }

        public IActionResult CustomException()
        {
            throw new CustomErrorException();
        }

    }
}