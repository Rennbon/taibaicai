﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService.Entities
{
    public class PushMessage : Main
    {
        public int ID { set; get; }
        public string Title { set; get; }

        public string Content { set; get; }

        public int MessageType { set; get; }

        
    }
}
