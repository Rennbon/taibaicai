using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class UserSU 
    {
        public int SUID { set; get; }
        public int UserID { set; get; }

        public string QQ { set; get; }

        public string WeChat { set; get; }

        public string WBlog { set; get; }
    }
}
