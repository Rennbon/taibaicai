using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CPDemo.UserCheck
{
    public class UserAuthAttribute : AuthorizeAttribute
    {
        protected  bool CheckUser(HttpContextBase httpContext)
        {
            if (httpContext.Session["Token"] != null)
                return true;
            else
                return false;
        }
    }
}