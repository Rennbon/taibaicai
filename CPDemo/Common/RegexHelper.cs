using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CPDemo.Common
{
    public static class RegexHelper
    {
        public static bool CheckEmail(string email)
        {
            Regex  re = new Regex("[a-zA-Z0-9\\-\\.]+@(([a-zA-Z0-9\\-]+\\.)+([a-zA-Z0-9]{2,4})+$)", RegexOptions.None);
            Match m = re.Match(email);
            if (m.Success)
                return true;
            else
                return false;
        }
    }
}