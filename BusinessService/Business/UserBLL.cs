using BusinessService.DataAccess;
using BusinessService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Business
{
    public class UserBLL
    {
        private UsersDAL dal = new UsersDAL();

        public bool GetUserByEmail(string email)
        {
            if (dal.GetUserByEmail(email) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public int CreateUser(Users model)
        {
            return dal.CreateUser(model);
        }

        public Users GetUser(Users model)
        {
            return dal.GetUser(model);
        }

        public bool DeleteUser(int userID)
        {
            if (dal.DeleteUser(userID) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Users> GetUserList(int pageIndex,int pageSize, out int totalSize)
        {
            return dal.GetUserList(pageIndex, pageSize, out totalSize);
        }
        public Users GetOverAllUserInfo(int userID)
        {
            return dal.GetOverAllUserInfo(userID);
        }
    }
}
