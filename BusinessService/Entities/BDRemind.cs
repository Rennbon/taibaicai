using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class BDRemind
    {
        public int ID { set; get; }
        /// <summary>
        /// 生日用户ID
        /// </summary>
        public int UserID { set; get; }
        /// <summary>
        /// 提醒类型 1:生日 2：好友生日
        /// </summary>
        public int Type { set; get; }
        /// <summary>
        /// 发送结果
        /// </summary>
        public bool SendResult { set; get; }
        /// <summary>
        /// 被发送者邮箱
        /// </summary>
        public string Email { set; get; }
        /// <summary>
        /// 收信者昵称
        /// </summary>
        public string Receiver { set; get; }
        /// <summary>
        /// 寿星昵称
        /// </summary>
        public string BDPerson { set; get; }

    }
}
