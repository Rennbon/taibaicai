using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class MailManage:Main
    {
        public int MailID { set; get; }
        public string MailAddress { set; get; }
        public string MailName { set; get; }
        public string Password { set; get; }
        public int Port { set; get; }
        public string Host { set; get; }
    }
}
