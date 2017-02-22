using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using System;
namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            //filters.Add(new HandleErrorAttribute()
            //    {
            //        ExceptionType = typeof(DivideByZeroException),
            //        View = "DivideError"
            //    });
            //filters.Add(new HandleErrorAttribute()
            //{
            //    ExceptionType = typeof(NotFiniteNumberException),
            //    View = "NotFiniteError"
            //});
            //filters.Add(new HandleErrorAttribute());//ExceptionFilter
            filters.Add(new EmployeeExceptionFilter());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
