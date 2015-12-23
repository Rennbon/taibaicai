using BusinessService.Business;
using BusinessService.Common;
using BusinessService.Entities;
using CPDemo.Common;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CPDemo.Controllers
{
    [RoutePrefix("API/Event")]
    public class EventController : ApiController
    {
        private UserBLL bll = new UserBLL();
        private EventBLL ebll = new EventBLL();
        private static readonly log4net.ILog Logger = LogManager.GetLogger(typeof(EventController).Name);
        const string dateFormat = "yyyy-MM-dd :HH:mm:ss:ffffff";
        [HttpPost]
        [Route("PushMessage")]
        public object PushMessage([FromBody]string json)
        {
            OperationResult op = new OperationResult(ResultCode.Error);
            try
            {
                dynamic jsonP = JValue.Parse(json);
                int id = jsonP.id;

                int totalCount = 0;
                List<Users> list = bll.GetUserList(1, 1000000, out totalCount);
                int listCount = list.Count();
                List<MailManage> mailList = ebll.GetMailManageList();
                PushMessage pm = ebll.GetPushMessageByID(id);
                if (totalCount == 0 || mailList == null || pm == null)
                {
                    return op;
                }
                
                //
                int endNum = listCount%mailList.Count();
                int partNum = listCount/mailList.Count();
                for (int i = 0; i < mailList.Count(); i++)
                {
                    List<Users> ulPart = new List<Users>();
                    if (i == mailList.Count() - 1)
                    {
                        ulPart = list;
                    }
                    else
                    {
                        ulPart = list.Take(partNum).ToList();
                        list.RemoveRange(0, partNum);
                    }
                    int index = i;
                    Task.Factory.StartNew(() =>
                    {
                        ForSend(ulPart, mailList[index], pm, mailList.Count());
                    }); 
                }
             

                return true; 
            }
            catch (Exception ex)
            {
                return op;
            }
        }



        public void ForSend(List<Users> userList,MailManage mm,PushMessage pm,int threadCount)
        {
            ParallelOptions paralleloption =new ParallelOptions();
            paralleloption.MaxDegreeOfParallelism = (int)Math.Ceiling((double)Environment.ProcessorCount / 2 / threadCount);
            Parallel.ForEach(userList, paralleloption, item =>
            {
                new EmailHelper().SendAsync(pm.Title,pm.Content,item.Email,mm.MailAddress,mm.MailName,mm.Password,
                    mm.Host, mm.Port, false, emailCompleted);


            });
      

        }


        public void emailCompleted(string message)
        {
            if (message != "OK")
            {
                Logger.ErrorFormat("邮件发送结果：{0},原因{1}", DateTime.Now.ToString(dateFormat), message);
            }
            else
            {
                Logger.Info("OK");
            }
     
        }
    }
}
