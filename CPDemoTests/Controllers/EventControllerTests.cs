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
    public class EventControllerTests
    {
        CPDemo.Controllers.EventController ec = new EventController();
        
        [TestMethod()]
        public void PushMessageTest()
        {
            string jsonContent = JsonConvert.SerializeObject(
              new
              {
                  id = 1
              }
              );
            ec.PushMessage(jsonContent);
        }

        [TestMethod()]
        public void ForSendTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void emailCompletedTest()
        {
            Assert.Fail();
        }
    }
}
