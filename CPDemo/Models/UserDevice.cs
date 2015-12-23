using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDemo.Models
{
    public class UserDevice
    {
        public int UserID { set; get; }
        public string Token { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime ExpiredTime { set; get; }
    }
}