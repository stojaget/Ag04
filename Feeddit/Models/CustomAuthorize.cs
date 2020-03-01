
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Feeddit.Models
{
    public class CustomAuthorize : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            RouteData rd = RouteTable.Routes.GetRouteData(context);
            if (context.Session["Username"] == null || context.Session["Username"].ToString() == string.Empty)
            {

                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Login" },
                     { "action", "AccessDenied" }
                });

            }
        }
    }
}