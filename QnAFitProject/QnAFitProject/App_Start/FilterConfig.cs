using System.Web;
using System.Web.Mvc;

namespace QnAFitProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //Make it required to login to use the web application
            filters.Add(new AuthorizeAttribute());
        }
    }
}
