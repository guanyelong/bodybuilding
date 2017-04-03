using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.Models
{
    public partial class tb_User_Buy_Rec
    {
        public string Hname { get; set; }
        public int TouchTimes { get; set; }
        public int InstrumentTimes { get; set; }
        public string ProductName { get; set; }
    }

    public partial class tb_Serv_Buy_temp 
    {
        public int ID { get; set; }

        public string ServName { get; set; }

        public string ServMemo { get; set; }

        public decimal ServPrice { get; set; }
        public int ServNum { get; set; }

        public int uId { get; set; }

        public int HospId { get; set; }

        public decimal PayMoney { get; set; }
    }
}
