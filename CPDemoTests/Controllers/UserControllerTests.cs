using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPDemo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
namespace CPDemo.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        CPDemo.Controllers.UserController uc = new UserController();
        [TestMethod()]
        public void TestTest()
        {
            uc.Test();
        }

        [TestMethod()]
        public void Test2Test()
        {
            uc.Test2("A","B");

        }

        [TestMethod()]
        public void RegisterTest()
        {
            string jsonContent = JsonConvert.SerializeObject(
             new
             {
                 email ="124@qq.com",
                 password="111111"
             }
             );
            uc.Register(jsonContent);
        }

        [TestMethod()]
        public void LoginTest()
        {

            string jsonContent = JsonConvert.SerializeObject(
             new
             {
                 email = "124@qq.com",
                 password = "111111"
             }
             );
            object result = uc.Login(jsonContent);
            int a = 1;
        }

        [TestMethod()]
        public void RemoveUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUserListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUserInfoTest()
        {
            Assert.Fail();
        }
    }
}
