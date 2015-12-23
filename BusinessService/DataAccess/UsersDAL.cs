using BusinessService.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ADOHelper.Common.DB;

namespace BusinessService.DataAccess
{
    public class UsersDAL : DAL
    {
        private Func<IDataRecord, Users> recordUserExtract =
               record =>
               {
                   return
                       new Users()
                       {
                           UserID = record.GetInt32("UserID"),

                           UserName = record.GetString("UserName")

                       };
               };
        private Func<IDataReader, Friendship> fsRecordExtract =
           record =>
           {
               return
                   new Friendship()
                   {
                       

                   };
           };
        private Func<IDataReader,Users> overallRecordExtract=
            record =>
            {
                return
                    new Users()
                    {
                        UserID = record.GetInt32("AttachedID"),
                        Email = record.GetString("Email"),
                        UserName = record.GetString("UserName")

                    };
            };

        public int CreateUser(Users model)
        {
            Action<DbCommand> prepare =
                cmd =>
                {
                    cmd.CommandText = "SP_CreateUser";
                    cmd.DeclareParameter("@Email").Type(DbType.String).Value(model.Email);
                    cmd.DeclareParameter("@Password").Type(DbType.Int32).Value(model.Password);




                    cmd.DeclareParameter("@UserID").Type(DbType.Int32).Direction(ParameterDirection.Output);
                };
            Action<DbParameterCollection> extract =
                parameters =>
                {
                    model.UserID = (int)parameters["UserID"].Value;
                };
            return ExecuteStoredProcedure(prepare, extract);
        }

        public Users GetUser(Users model)
        {
            Action<DbCommand> prepare =
               cmd =>
               {
                   cmd.CommandText = "SP_GetUser";
                   cmd.DeclareParameter("@Email").Type(DbType.String).Value(model.Email);
                   cmd.DeclareParameter("@Password").Type(DbType.Int32).Value(model.Password);

               };

            return ExecuteReaderForObject<Users>(prepare, recordUserExtract);
        }
        public int DeleteUser(int userID)
        {
            Action<DbCommand> prepare =
                cmd =>
                {
                    cmd.CommandText = "SP_DeleteUser";
                    cmd.DeclareParameter("@UserID").Type(DbType.Int64).Value(userID);
                 
                };

            return ExecuteStoredProcedure(prepare);
        }
        public List<Users> GetUserList(int pageIndex, int pageSize, out int totalSize)
        {

            int a = 0;
            Action<DbCommand> prepare =
                cmd =>
                {
                    cmd.CommandText = "SP_GetUserList";
                    cmd.DeclareParameter("@pageIndex").Type(DbType.Int32).Value(pageIndex);
                    cmd.DeclareParameter("@PageSize").Type(DbType.Int32).Value(pageSize);


                };
            DataSet ds = ExecuteDataSet(prepare);
            List<Users> list = new List<Users>();
            foreach (DataRow item in ds.Tables[1].Rows)
            {
                Users index = new Users();
                index.UserName = item["UserName"].ToString();
                index.UserID = int.Parse(item["UserID"].ToString());
                index.Email =item["Email"].ToString();
                list.Add(index);
            }
            totalSize = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return list;

        }
        public Users GetOverAllUserInfo(int userID)
        {
            Action<DbCommand> prepare =
                cmd =>
                {
                    cmd.CommandText = "SP_GetUserInfo";
                    cmd.DeclareParameter("@UserID").Type(DbType.Int32).Value(userID);
                };
            DataSet ds = ExecuteDataSet(prepare);
            Users user = new Users();
            DataRowCollection drc = ds.Tables[0].Rows;
            if (drc.Count > 0)
            {
                user.UserID =int.Parse( drc[0]["UserID"].ToString());
                user.Email = drc[0]["Email"].ToString();
                user.UserName = drc[0]["UserName"].ToString();
                if(!Convert.IsDBNull(drc[0]["UBDID"])){
                    user.userBirthday = new UserBirthday()
                    {
                        UBDID =int.Parse( drc[0]["UBDID"].ToString()),
                        Birthday = DateTime.Parse(drc[0]["Birthday"].ToString())

                    };
                }
                if(!Convert.IsDBNull(drc[0]["InfoID"]))
                {
                    user.userInfo = new UserInfo()
                    {
                        InfoID = int.Parse(drc[0]["InfoID"].ToString()),
                        Gender = int.Parse(drc[0]["Gender"].ToString()),
                        Height =decimal.Parse(drc[0]["Height"].ToString()),                         
                        Position=drc[0]["Position"].ToString(),
                        SelfIntroduction =drc[0]["SelfIntroduction"].ToString(),
                        Weight=int.Parse(drc[0]["Weight"].ToString()),
                        Hobby =  drc[0]["Hobby"].ToString(),
                    };
                }
                if (!Convert.IsDBNull(drc[0]["SUID"]))
                {
                    user.userSU = new UserSU()
                    {
                        SUID =int.Parse( drc[0]["SUID"].ToString()),
                        QQ = drc[0]["QQ"].ToString(),
                        WBlog = drc[0]["WBlog"].ToString(),
                        WeChat = drc[0]["WeChat"].ToString()
                    };
                }
                user.eduList = new List<UserEdu>();
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    UserEdu index = new UserEdu()
                    {
                        EduID =int.Parse( item["EduID"].ToString()),
                        EDate = DateTime.Parse(item["EDate"].ToString()),
                        SDate = DateTime.Parse(item["SDate"].ToString()),
                        SchoolName = item["SchoolName"].ToString(),
                        Professional = item["Professional"].ToString()
                    };
                    user.eduList.Add(index);
                }
                user.fsList = new List<Friendship>();
                foreach (DataRow item in ds.Tables[2].Rows)
                {
                    Friendship index = new Friendship()
                    {
                        FSID = int.Parse(item["FSID"].ToString()),
                        FID = int.Parse(item["FID"].ToString()),
                        FUserName = item["FUserName"].ToString()

                    };
                    user.fsList.Add(index);
                }
            }
 
            return user;
        }
        public int GetUserByEmail(string email)
        {
            Action<DbCommand> prepare =
               cmd =>
               {
                   cmd.CommandText = "SP_GetUserByEmail";
                   cmd.DeclareParameter("@Email").Type(DbType.String).Value(email);
               };
            return ExecuteStoredProcedure(prepare); 
        }
    }
}
