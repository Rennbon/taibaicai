using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class UserEdu 
    {
        public int EduID { set; get; }
        public int UserID { set; get; }

        public DateTime SDate { set; get; }

        public DateTime EDate { set; get; }

        public string SchoolName { set; get; }

        public string Professional { set; get; }
    }
}
