using BusinessService.Business;
using BusinessService.Entities;
using CPDemo.Caching;
using CPDemo.Common;
using CPDemo.Models;
using CPDemo.UserCheck;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CPDemo.Controllers
{
    [RoutePrefix("API/User")]
    public class UserController : ApiController
    {
        //public const 
        private static readonly log4net.ILog Logger = LogManager.GetLogger(typeof(UserController).Name);
        private UserBLL bll = new UserBLL();
        [HttpGet]
        [Route("Test")]
        public object Test()
        {
            var context = HttpContext.Current;
            Users a = new Users() { Email="1234"};
            return "OK";
        }
        [HttpPost]
        [Route("Test2")]
        public object Test2(string a,string b)
        {
            string c = a;
            string d = b;
          

            return "OK";
        }
        [HttpPost]
        [Route("Register")]
        public object Register([FromBody]string json)
        {
            OperationResult op = new OperationResult(ResultCode.Error);
            try
            {
                dynamic jsonP = JValue.Parse(json);
                string email = jsonP.email;
                string password = jsonP.password;
                if (!RegexHelper.CheckEmail(email))
                {
                    op.code = ResultCode.EmailError;
                    return op;
                }
                if (bll.GetUserByEmail(email))
                {
                    op.code = ResultCode.EmailExist;
                    return op;
                }
                Users user = new Users()
                {
                    Email = email,
                    Password = password
                };
                int result = bll.CreateUser(user);
                if (result == -1)
                {
                    op.code = ResultCode.EmailExist;
                    return op;
                }
                op.code = ResultCode.Success;
                op.data = result;
                return op;
            }
            catch (Exception ex)
            {
                return op;
            }
        }

        [HttpPost]
        [Route("Login")]
        public object Login([FromBody]string json)
        {
            OperationResult op = new OperationResult(ResultCode.Error);
            try
            {
                dynamic jsonP = JValue.Parse(json);
                string email = jsonP.email;
                string password = jsonP.password;
                if (!RegexHelper.CheckEmail(email))
                {
                    op.code = ResultCode.EmailError;
                    return op;
                }
                Users user = new Users()
                {
                    Email = email,
                    Password = password
                };
                user = bll.GetUser(user);
                if (user.UserID > 0)
                {
 
                    string token =MD5.ToMD5( Guid.NewGuid().ToString()+user.UserID+DateTime.Now);

                    TimeSpan ts = new TimeSpan(0, 15, 0);
                    UserDevice ud = new UserDevice()
                    {
                        Token = token,
                        UserID = user.UserID,
                        CreateTime = DateTime.Now,
                        ExpiredTime = DateTime.Now.Add(ts)
                    };
                    string key = token;
                    RuntimeMemoryCache cache = new RuntimeMemoryCache("Token");
                    cache.Set(key, ud, ts);
                    op.code = ResultCode.Success;
                    op.data = new { User = user, Token = token };
                }
                else
                {
                    op.code = ResultCode.QueryNull;
                }
                return op;
            }
            catch (Exception ex)
            {
                return op;
            }
        }

        [HttpPost]
        [Route("RemoveUser")]
        public object RemoveUser([FromBody]string json)
        {
            OperationResult op = new OperationResult(ResultCode.Error);
            try
            {

                dynamic jsonP = JValue.Parse(json);
                int userID = jsonP.userID;
                string token = jsonP.token;


                RuntimeMemoryCache cache = new RuntimeMemoryCache("Token");
                UserDevice ud = cache.Get<UserDevice>(token);
                if (ud == null)
                {
                    op.code = ResultCode.TokenNull;
                    return op;
                }
                else
                {
                    if (ud.Token != token || ud.ExpiredTime < DateTime.Now)
                    {
                        op.code = ResultCode.TokenNull;
                        return op;
                    }
                }
                if (userID == ud.UserID)
                {
                    op.code = ResultCode.ValidError;
                    return op;
                }
                if (bll.DeleteUser(userID))
                {
                    op.code = ResultCode.Success;
                }
                else
                {
                    op.code = ResultCode.NoChanged;
                }
                return op;
            }
            catch (Exception ex)
            {
                return op;
            }

        }
        [HttpGet]
        [Route("GetUserList")]
        public object GetUserList(int pageSize = 10, int pageIndex = 1)
        {
            OperationResult op = new OperationResult(ResultCode.Error);
            try
            {
                int totalSize = 0;
                List<Users> userList = bll.GetUserList(pageIndex, pageSize, out totalSize);
                op.code = ResultCode.Success;
                op.data = new { TotalSize = totalSize, lists = userList };
                return op;
            }
            catch (Exception ex)
            {
                return op;
            }
        }
        [HttpGet]
        [Route("GetUserInfo")]
        public object GetUserInfo(int userID)
        {
            OperationResult op = new OperationResult(ResultCode.Error);
            try
            {
                string key = typeof(Users).Name + userID;
                RuntimeMemoryCache cache = new RuntimeMemoryCache(typeof(Users).Name);
                Users user = cache.Get<Users>(key);
                if (user == null)
                {
                    user = bll.GetOverAllUserInfo(userID);
                    if (user != null)
                    {
                        cache.Set(key, user, new TimeSpan(1, 0, 0));
                    }
                    else
                    {
                        op.code = ResultCode.QueryNull;
                        return op;
                    }
                }
                op.code = ResultCode.Success;
                op.data = user;
                return op;

            }
            catch (Exception ex)
            {
                return op;
            }

        }
    }
}
