using System.Web;
using System.Web.Mvc;
using Homework.Controllers;

namespace Homework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExcutingTimeAttribute());
            filters.Add(new BasicAuthAttribute());
        }
    }
}
