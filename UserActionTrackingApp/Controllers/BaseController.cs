using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace UserActionTrackingApp.Controllers
{
    public class BaseController : Controller
    {


        private readonly IHttpContextAccessor contxt;
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            contxt = httpContextAccessor;
        }

        protected void CreateSession(string sessName)
        {

            if (HttpContext.Session != null)
            {
                // Check if the session variable exists
                if (!HttpContext.Session.IsAvailable || HttpContext.Session.GetInt32(sessName) == null)
                {

                    //create the session
                    HttpContext.Session.SetInt32(sessName, 1);
                }
                else
                {
                    // Session variable exists
                    // Update or perform other operations
                    int currentCount = HttpContext.Session.GetInt32(sessName).Value;
                    HttpContext.Session.SetInt32(sessName, currentCount + 1);
                }
            }
        }


        protected void PageCountIncrement(string pageName)
        {

                // If the cookie doesn't exist, create a new dictionary
                Dictionary<string, int> pageCounts;
                if (!Request.Cookies.ContainsKey("page_views"))
                {
                    pageCounts = new Dictionary<string, int>();
                }
                else
                {
                    // Deserialize existing JSON string from the cookie
                    string json = Request.Cookies["page_views"];
                    pageCounts = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                }

                // Increment page count or add new page if not exist
                if (pageCounts.ContainsKey(pageName))
                {
                    pageCounts[pageName]++;
                }
                else
                {
                    pageCounts[pageName] = 1;
                }

                // Serialize dictionary to JSON string
                string updatedJson = JsonConvert.SerializeObject(pageCounts);

                // Store JSON string in the cookie
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(90)
                };
                Response.Cookies.Append("page_views", updatedJson, options);
        }



        protected int GetPageViewsFromCookie(string pageName)
        {
            int page1Views = 0;

            // Check if the cookie exists
            if (Request.Cookies.ContainsKey("page_views"))
            {
                // Deserialize existing JSON string from the cookie
                string json = Request.Cookies["page_views"];
                var pageCounts = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

                // Check if the dictionary contains "page1"
                if (pageCounts.ContainsKey(pageName))
                {
                    page1Views = pageCounts[pageName];
                }
            }

            return page1Views;
        }


    }
}
