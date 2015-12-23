using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class Friendship
    {
        public int FSID { set; get; }

        /// <summary>
        /// 用户自身ID
        /// </summary>
        public int OwnID { set; get; }
        /// <summary>
        /// 好友ID
        /// </summary>
        public int FID { set; get; }

        public string FUserName { set; get; }
    }
}
