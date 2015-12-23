using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BusinessService.Common
{
    public class EmailHelper
    {
        //回调方法
        Action<string> actionSendCompletedCallback = null;
        ///// <summary>
        ///// 使用异步发送邮件
        ///// </summary>
        ///// <param name="subject">主题</param>
        ///// <param name="body">内容</param>
        ///// <param name="to">接收者,以,分隔多个接收者</param>
        //// <param name="_actinCompletedCallback">邮件发送后的回调方法</param>
        ///// <returns></returns>
        public void SendAsync(string subject, string body, string toMail, string fromMail, string fromName,
            string password, string host, int port, bool enableSsl,
            Action<string> _actinCompletedCallback)
        {
            MailMessage mes = new MailMessage();
            mes.From = new System.Net.Mail.MailAddress(fromMail, fromName);
            mes.To.Add(toMail);
            mes.Subject = subject;
            mes.Body = body;
            mes.IsBodyHtml = true;
            mes.BodyEncoding = Encoding.UTF8;
            mes.SubjectEncoding = Encoding.UTF8;


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(fromMail, password);
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSsl;


            //异步回调
            actionSendCompletedCallback = _actinCompletedCallback;
            smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            try
            {
          
                smtpClient.SendAsync(mes, "OK");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                smtpClient = null;
                mes = null;
            }
        }
        /// <summary>
        /// 异步操作完成后执行回调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (actionSendCompletedCallback == null) return;
            string message = string.Empty;
            if (e.Cancelled)
            {
                message = "异步操作取消";
            }
            else if (e.Error != null)
            {
                message = (string.Format("UserState:{0},Message:{1}", (string)e.UserState, e.Error.ToString()));
            }
            else
            {
                message = (string)e.UserState;
            }
            //执行回调方法  可以直接写log
            actionSendCompletedCallback(message);
        }
        public bool SendMessage(string emial, string subject, string body)
        {

            MailMessage mes = new MailMessage();

            mes.From = new MailAddress(mes.From.Address, "服务器推送");
            try
            {
                mes.To.Add(new MailAddress(emial));
            }
            catch { }

            mes.Subject = subject;
            mes.Body = body;
            mes.BodyEncoding = Encoding.UTF8;
            mes.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.126.com";
            //smtp.Credentials = new NetworkCredential(mes.From.Address, "mmftaqcvukahmulc");
            smtp.Port = 25;
            //sender.EnableSsl = false;

            bool result = false;
            try
            {
                smtp.Send(mes);
                result = true;
            }
            catch
            {
            }
            return result;
        }
    }
}
