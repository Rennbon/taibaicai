using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayRemindService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("定时生日提醒服务启动");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000 * 3600;
            timer.Elapsed += new System.Timers.ElapsedEventHandler();
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        public void Birth
    }
}
