using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.Models
{
    public partial class tb_Serv_POF
    {
        public string ServName { get; set; }
        public decimal price { get; set; }

        public string HospIds { get; set; }

        public string Hname { get; set; }
    }
}
