using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class Users 
    {
        public Users()
        {
            //this.eduList = new List<UserEdu>();
            //this.fsList = new List<Friendship>();
            //this.userBirthday = new UserBirthday();
            //this.userInfo = new UserInfo();
            //this.userSU = new UserSU();
        }
        public int UserID { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }

        public string UserName { set; get; }

        public virtual UserBirthday userBirthday { set; get; }

        public virtual UserInfo userInfo { set; get; }
        public virtual UserSU userSU { set; get; }
        public virtual List<UserEdu> eduList { set; get; }

        public virtual List<Friendship> fsList { set; get; }

        
    }
}
