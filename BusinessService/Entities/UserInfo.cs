using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class UserInfo 
    {
        public int InfoID { set; get; }
        public int UserID { set; get; }

        /// <summary>
        /// 0男 1女
        /// </summary>
        public int Gender { set; get; }
        public string Hobby { set; get; }
        public string Position { set; get; }

        public decimal Height { set; get; }
        public decimal Weight { set; get; }

        public string SelfIntroduction { set; get; }

    }
}
