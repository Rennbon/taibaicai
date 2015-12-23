using BusinessService.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
          
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);
            t.AutoReset = true;
            t.Enabled = true;
            t.Start();
        }

        protected override void OnStop()
        {
        }
        public void ChkSrv(object source,System.Timers.ElapsedEventArgs e)
        {
            int hour = e.SignalTime.Hour;
            int minute = e.SignalTime.Minute;
            int second = e.SignalTime.Second;
            if (hour ==14&&minute==30&&second==00)
            {
                try 
                {
                    System.Timers.Timer tt = (System.Timers.Timer)source;
                    tt.Enabled = false;
                    new EventBLL().BirthdayRemind();
                    tt.Enabled = true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

 
    }
}
