﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.Models
{
    public partial class tb_Serv_Info
    {
        public string ServTypeName { get; set; }

        public int TouchTimes { get; set; }
        public int InstrumentTimes { get; set; }
    }
}