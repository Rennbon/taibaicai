using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class UserBirthday 
    {
        public int UBDID { set; get; }
        public int UserID { set; get; }

        public DateTime Birthday { set; get; }
    }
}
