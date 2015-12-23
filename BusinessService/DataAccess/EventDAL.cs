using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOHelper.Common.DB;
using System.Data;
using BusinessService.Entities;
using System.Data.Common;


namespace BusinessService.DataAccess
{
    public class EventDAL:DAL
    {
        private Func<IDataRecord, BDRemind> recordExtract =
            record =>
            {
                return
                    new BDRemind()
                    {
                        ID = record.GetInt32("ID"),
                        UserID=record.GetInt32("UserID"),
                        Type = record.GetInt32("Type"),
                        SendResult = record.GetBoolean("SendResult"),
                        Email = record.GetString("Email"),
                        Receiver = record.GetString("Receiver"),
                        BDPerson = record.GetString("BDPerson")
                    };
            };
        private Func<IDataRecord, PushMessage> pushRecordExtract =
            record =>
            {
                return new PushMessage()
                {
                    Title = record.GetString("Title"),
                    Content = record.GetString("Content"),
                };
            };

        private Func<IDataRecord, MailManage> mmRecordExtract =
            record =>
            {
                return new MailManage()
                {
                    MailAddress = record.GetString("MailAddress"),
                    MailName = record.GetString("MailName"),
                    Password = record.GetString("Password"),
                    Port = record.GetInt32("Port"),
                    Host = record.GetString("Host")

                };
            };
        public List<BDRemind> GetBDRemind()
        {
            Action<DbCommand> prepare =
               cmd =>
               {
                   cmd.CommandText = "SP_GetBirthdayRemind";
                 
               };
            return ExecuteReaderForList<BDRemind>(prepare, recordExtract);

        }

        public List<MailManage> GetMailManageList()
        {
            Action<DbCommand> prepare =
               cmd =>
               {
                   cmd.CommandText = "SP_GetMailManageList";

               };
            return ExecuteReaderForList<MailManage>(prepare, mmRecordExtract);
        }
        public PushMessage GetPushMessageByID(int id)
        {
            Action<DbCommand> prepare =
              cmd =>
              {
                  cmd.CommandText = "SP_GetPushMessage";
                  cmd.DeclareParameter("@ID").Type(DbType.String).Value(id);
                

              };

            return ExecuteReaderForObject<PushMessage>(prepare, pushRecordExtract);
        }
    }
}
