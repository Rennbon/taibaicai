using BusinessService.Common;
using BusinessService.DataAccess;
using BusinessService.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessService.Business
{
    public class EventBLL
    {
        private EventDAL dal = new EventDAL();
        EmailHelper emailHelper = new EmailHelper();

        public void BirthdayRemind()
        {
            List<BDRemind> list = dal.GetBDRemind();
            List<int> idlist = new List<int>();
            if (list != null)
            {

                Parallel.ForEach(list,
                    new ParallelOptions { MaxDegreeOfParallelism = (int)Math.Ceiling((double)Environment.ProcessorCount / 2d) },
                    item =>
                    {
                        if (item.Type == 1)
                        {
                            string subject = item.Receiver + "生日快乐";
                            string body = "生日真的很快乐";
                            if (emailHelper.SendMessage(item.Email, subject, body))
                            {
                                idlist.Add(item.ID);
                            }
                        }
                        else
                        {
                            string subject = "今天是您的好友" + item.BDPerson + "的生日";
                            string body = item.Receiver + "还在等什么，赶快送TA一个茶叶蛋吧！";
                            if (emailHelper.SendMessage(item.Email, subject, body))
                            {
                                idlist.Add(item.ID);
                            }
                        }
                    });
            }

        }

        public List<MailManage> GetMailManageList()
        {
            return dal.GetMailManageList();
        }

        public PushMessage GetPushMessageByID(int id)
        {
            return dal.GetPushMessageByID(id);
        }
        public void PushMessage(List<Users> list)
        {
           
            List<int> idlist = new List<int>();
            if (list != null)
            {
                long count = list.Count();
                if (count != 0)
                {
                    OrderablePartitioner<Tuple<long, long>> orderPartition = Partitioner.Create(1, count + 1, (count / (int)Math.Ceiling((double)Environment.ProcessorCount / 2d)));

                    ParallelOptions parallelOptions = new ParallelOptions();
                    parallelOptions.MaxDegreeOfParallelism = (int)Math.Ceiling((double)Environment.ProcessorCount / 2d);

                    
                }

            }
        }

       
    
    }


}



