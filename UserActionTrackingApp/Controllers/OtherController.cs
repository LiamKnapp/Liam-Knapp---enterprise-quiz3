using Microsoft.AspNetCore.Mvc;

using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class OtherController : BaseController
    {
        public OtherController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public IActionResult Index()
        {



            CreateSession("Page2Session");
            PageCountIncrement("page2");

            // Retrieve total page views from cookie
            ViewData["TotalPageViews"] = GetPageViewsFromCookie("page2");

            // Retrieve total page views from session
            ViewData["SessionCount"] = HttpContext.Session.GetInt32("Page2Session");
            
            return View();
        }
    }
}
