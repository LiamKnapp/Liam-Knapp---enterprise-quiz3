using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public IActionResult Index()
        {
            CreateSession("Page1Session");
            PageCountIncrement("page1");

            // Retrieve total page views from cookie
            ViewData["TotalPageViews"] = GetPageViewsFromCookie("page1");

            // Retrieve total page views from session
            ViewData["SessionCount"] = HttpContext.Session.GetInt32("Page1Session");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}